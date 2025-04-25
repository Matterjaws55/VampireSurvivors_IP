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
            public Entity ScoreEntity;
            [ReadOnly] public ComponentLookup<Score> ScoreLookup; // Added [ReadOnly]

            // Since we can't write directly to ScoreLookup, we'll use ECB to update the score
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
                            var currentScore = ScoreLookup[ScoreEntity];
                            var newScore = new Score { Value = currentScore.Value + health.ScoreOnDeath };
                            ECB.SetComponent(sortKey, ScoreEntity, newScore);

                            ECB.AddComponent(sortKey, enemyEntity, new DeathAnimation() { Duration = 1f });
                            //ECB.RemoveComponent<MoveSpeed>(sortKey, enemyEntity);
                            //ECB.RemoveComponent<Health>(sortKey, enemyEntity);
                        }

                        damagedEntities.Entities.Add(enemyEntity);
                    }
                }
            }
        }
        
        EntityQuery _enemyQuery;
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();            
            _enemyQuery = SystemAPI.QueryBuilder().WithAll<Health, LocalTransform>().Build();
        }




        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
            
            var enemies = _enemyQuery.ToEntityArray(Allocator.TempJob);

            // Get or create score entity
            Entity scoreEntity;
            if (!SystemAPI.TryGetSingletonEntity<Score>(out scoreEntity))
            {
                scoreEntity = state.EntityManager.CreateEntity();
                state.EntityManager.AddComponentData(scoreEntity, new Score { Value = 0 });
            }

            var job = new DamageJob
            {
                ECB = ecb.AsParallelWriter(),
                TransformLookup = state.GetComponentLookup<LocalTransform>(true),
                HealthLookup = state.GetComponentLookup<Health>(true),
                ScoreLookup = state.GetComponentLookup<Score>(),
                ScoreEntity = scoreEntity,
                Enemies = enemies
            };

            job.ScheduleParallel();
            state.Dependency.Complete();
            enemies.Dispose();
        }
    }
    
}