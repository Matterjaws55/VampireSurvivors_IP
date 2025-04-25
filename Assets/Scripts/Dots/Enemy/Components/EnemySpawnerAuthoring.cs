using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Dots.Enemy.Components
{
        public class EnemySpawnerAuthoring : MonoBehaviour
        {
                
                public GameObject prefab;
                public float timeToUnlock;
                public float spawnInterval;
                public float spawnRadius;
                public Transform player;
        }

        public class EnemySpawnerBaker : Baker<EnemySpawnerAuthoring>
        {
                public override void Bake(EnemySpawnerAuthoring authoring)
                {
                        var entity = GetEntity(TransformUsageFlags.None);
                        
                        Entity enemyPrefabEntity = GetEntity( authoring.prefab, TransformUsageFlags.Dynamic);

                        AddComponent(entity,new EnemyTier
                        {
                                PrefabEntity =enemyPrefabEntity,
                                TimeToUnlock = authoring.timeToUnlock,
                                SpawnInterval = authoring.spawnInterval,
                                SpawnRadius = authoring.spawnRadius
                        });
                        
                        // Add components
                        AddComponent(entity, new EnemySpawnerComponent
                        {
                                PlayerEntity = GetEntity(authoring.player, TransformUsageFlags.Dynamic),
                                GameTime = 0,
                                NextSpawnTime = 0
                        });
                }
        }
        
        public struct EnemyTier : IComponentData
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
        
}