using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Dots
{
    public partial class SpawnEnemySystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<SpawnEnemyConfig>();
        }

        protected override void OnUpdate()
        {
            this.Enabled = false;

            SpawnEnemyConfig enemySpawnConfig = SystemAPI.GetSingleton<SpawnEnemyConfig>();
            
            for(int i = 0; i < enemySpawnConfig.AmountToSpawn; i++)
            {
                Entity enemyEntity = EntityManager.Instantiate(enemySpawnConfig.EnemyPrefabEntity);
                
                EntityManager.SetComponentData(enemyEntity, new LocalTransform()
                {
                    Position = new float3(UnityEngine.Random.Range(-50f, 50f), 0, UnityEngine.Random.Range(-50f, 50f)),
                    Rotation = Quaternion.identity,
                    Scale = 1f,
                });
            }
        }
    }
}