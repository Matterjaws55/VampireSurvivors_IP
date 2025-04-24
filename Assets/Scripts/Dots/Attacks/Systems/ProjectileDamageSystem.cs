using Dots.Attacks.Components;
using Dots.Components;
using Dots.Enemy.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Dots.Attacks.Systems
{
    public partial struct ProjectileDamageSystem : ISystem
    {
        [BurstCompile]
        private partial struct DamageJob : IJobEntity
        {
            public EntityCommandBuffer.ParallelWriter ECB;
            [ReadOnly] public ComponentLookup<LocalTransform> TransformLookup;
            [ReadOnly] public ComponentLookup<Health> HealthLookup;
            [ReadOnly] public NativeArray<Entity> Enemies;

            void Execute(
                RefRO<LocalTransform> projectileTransform,
                RefRO<Projectile> projectile,
                ref DamagedEntitites damagedEntities,
                [EntityIndexInQuery] int sortKey,
                in Entity projectileEntity)
            {
                var position = projectileTransform.ValueRO.Position;

                foreach (var enemyEntity in Enemies)
                {
                    if (damagedEntities.Entities.Length >= 511) break;
                    if (damagedEntities.Entities.Contains(enemyEntity))
                        continue;

                    var enemyTransform = TransformLookup[enemyEntity];
                    var distance = math.distance(position, enemyTransform.Position);

                    if (distance <= projectile.ValueRO.Radius)
                    {
                        var health = HealthLookup[enemyEntity];
                        var newHealth = health;
                        newHealth.Value -= (int)projectile.ValueRO.Damage;
                        ECB.SetComponent(sortKey, enemyEntity, newHealth);
                        if (newHealth.Value <= 0)
                        {
                            ECB.AddComponent(sortKey, enemyEntity, new DeathAnimation() { Duration = 2f });
                            ECB.RemoveComponent<MoveSpeed>(sortKey, enemyEntity);
                            ECB.RemoveComponent<Health>(sortKey, enemyEntity);

                        }
                        
                
                        damagedEntities.Entities.Add(enemyEntity);
                    }
                }
            }
        }

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
            var enemyQuery = SystemAPI.QueryBuilder().WithAll<Health, LocalTransform>().Build();
            var enemies = enemyQuery.ToEntityArray(Allocator.TempJob);

            var job = new DamageJob
            {
                ECB = ecb.AsParallelWriter(),
                TransformLookup = state.GetComponentLookup<LocalTransform>(true),
                HealthLookup = state.GetComponentLookup<Health>(true),
                Enemies = enemies
            };

            job.ScheduleParallel();
            state.Dependency.Complete();
            enemies.Dispose();
        }
        
        
    }
}