using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Dots.Enemy.Components
{
        public class EnemySpawnerAuthoring : MonoBehaviour
        {
                [System.Serializable]
                public struct EnemyTierAuthoring
                {
                        public GameObject prefab;
                        public float timeToUnlock;
                        public float spawnInterval;
                        public float spawnRadius;
                }

                public EnemyTierAuthoring[] enemyTiers;
                public Transform player;
        }

        public class EnemySpawnerBaker : Baker<EnemySpawnerAuthoring>
        {
                public override void Bake(EnemySpawnerAuthoring authoring)
                {
                        var entity = GetEntity(TransformUsageFlags.None);
        
                        // Bake enemy tiers
                        var tiers = new NativeArray<EnemyTier>(authoring.enemyTiers.Length, Allocator.Temp);
                        for (int i = 0; i < authoring.enemyTiers.Length; i++)
                        {
                                var tier = authoring.enemyTiers[i];
                                // Use GetEntity with correct TransformUsageFlags for prefabs
                                var prefabEntity = GetEntity(tier.prefab, TransformUsageFlags.Dynamic | TransformUsageFlags.Dynamic);
            
                                tiers[i] = new EnemyTier
                                {
                                        PrefabEntity = prefabEntity,
                                        TimeToUnlock = tier.timeToUnlock,
                                        SpawnInterval = tier.spawnInterval,
                                        SpawnRadius = tier.spawnRadius
                                };
                        }

                        // Add components
                        AddComponent(entity, new EnemySpawnerComponent
                        {
                                PlayerEntity = GetEntity(authoring.player, TransformUsageFlags.Dynamic),
                                GameTime = 0,
                                NextSpawnTime = 0
                        });

                        var tiersList = new FixedList128Bytes<EnemyTier>();
                        foreach (var tier in tiers)
                        {
                                tiersList.Add(tier);
                        }

                        AddComponent(entity, new EnemyTiersComponent
                        {
                                Tiers = tiersList
                        });

                        tiers.Dispose();
                }
        }
        
        public struct EnemyTier
        {
                public Entity PrefabEntity;
                public float TimeToUnlock;
                public float SpawnInterval;
                public float SpawnRadius;
        }

        public struct EnemySpawnerComponent : IComponentData
        {
                public Entity PlayerEntity;
                public float GameTime;
                public float NextSpawnTime;
        }

        public struct EnemyTiersComponent : IComponentData
        {
                public FixedList128Bytes<EnemyTier> Tiers;
        }
}