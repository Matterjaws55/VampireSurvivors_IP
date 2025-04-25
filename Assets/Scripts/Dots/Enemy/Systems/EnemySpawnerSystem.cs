using Dots.Enemy.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Dots.Enemy.Systems
{
    public partial class EnemySpawnerSystem : SystemBase
    {
        private BeginSimulationEntityCommandBufferSystem _beginSimEcbSystem;

        protected override void OnCreate()
        {
            _beginSimEcbSystem = World.GetOrCreateSystemManaged<BeginSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var ecb = _beginSimEcbSystem.CreateCommandBuffer();
            var deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (spawner, tiers) in
                     SystemAPI.Query<RefRW<EnemySpawnerComponent>, RefRO<EnemyTiersComponent>>())
            {
                spawner.ValueRW.GameTime += deltaTime;

                if (spawner.ValueRO.GameTime < spawner.ValueRO.NextSpawnTime)
                    continue;

                var currentTier = GetCurrentTier(spawner.ValueRO.GameTime, tiers.ValueRO.Tiers);
                if (!EntityManager.Exists(currentTier.PrefabEntity)) continue;
                if (!EntityManager.Exists(spawner.ValueRO.PlayerEntity)) continue;

                var playerTransform = SystemAPI.GetComponent<LocalTransform>(spawner.ValueRO.PlayerEntity);
                int spawnCount = math.clamp((int)(spawner.ValueRO.GameTime / 60f) + 1, 1, 8);

                for (int i = 0; i < spawnCount; i++)
                {
                    float angle = UnityEngine.Random.Range(0, math.PI * 2);
                    float3 spawnPos = new float3(
                        playerTransform.Position.x + math.cos(angle) * currentTier.SpawnRadius,
                        playerTransform.Position.y,
                        playerTransform.Position.z + math.sin(angle) * currentTier.SpawnRadius
                    );

                    var enemyEntity = ecb.Instantiate(currentTier.PrefabEntity);
                    ecb.SetComponent(enemyEntity, LocalTransform.FromPositionRotationScale(
                        spawnPos,
                        quaternion.identity,
                        1f
                    ));
                }

                spawner.ValueRW.NextSpawnTime = spawner.ValueRO.GameTime + currentTier.SpawnInterval;
            }

            _beginSimEcbSystem.AddJobHandleForProducer(Dependency);
        }

        private static EnemyTier GetCurrentTier(float gameTime, FixedList128Bytes<EnemyTier> tiers)
        {
            for (int i = tiers.Length - 1; i >= 0; i--)
            {
                if (gameTime >= tiers[i].TimeToUnlock)
                {
                    return tiers[i];
                }
            }
            return tiers[0];
        }
    }
}