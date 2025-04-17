using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dots
{
    public partial class SpawnEnemySystem : SystemBase
    {
        public delegate void OnSpawnEnemySignature(Entity enemy);
        public OnSpawnEnemySignature OnSpawnEnemy;
        protected override void OnCreate()
        {
            RequireForUpdate<SpawnEnemyConfig>();
        }

        protected override void OnUpdate()
        {
                
            SpawnEnemyConfig enemySpawnConfig = SystemAPI.GetSingleton<SpawnEnemyConfig>();
            Entity entity = SystemAPI.GetSingletonEntity<SpawnEnemyConfig>();
            
            
            this.Enabled = false;
    
            for(int i = 0; i < enemySpawnConfig.AmountToSpawn; i++)
            {
                Entity enemyEntity = EntityManager.Instantiate(enemySpawnConfig.EnemyPrefabEntity);

                Vector3 pos = new Vector3(UnityEngine.Random.Range(-300f, 300f), 0,
                    UnityEngine.Random.Range(-300f, 300f));
                EntityManager.SetComponentData(enemyEntity, new LocalTransform()
                {
                    Position =pos,
                    Rotation = Quaternion.identity,
                    Scale = 1f,
                });

                OnSpawnEnemy?.Invoke(enemyEntity);
            }
        }
    }
}