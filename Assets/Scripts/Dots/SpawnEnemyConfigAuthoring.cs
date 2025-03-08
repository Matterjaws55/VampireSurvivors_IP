using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Dots
{
    public class SpawnEnemyConfigAuthoring : MonoBehaviour
    {
        [FormerlySerializedAs("_cubePrefab")] public GameObject _enemyPrefab;
        public int _amountToSpawn;
        
        private class SpawnEnemyConfigAuthoringBaker : Baker<SpawnEnemyConfigAuthoring>
        {
            public override void Bake(SpawnEnemyConfigAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.None);
                
                Entity cubePrefabEntity = GetEntity(authoring._enemyPrefab, TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new SpawnEnemyConfig()
                {
                    EnemyPrefabEntity = cubePrefabEntity,
                    AmountToSpawn = authoring._amountToSpawn,
                });
            }
        }
    }
    
    public struct SpawnEnemyConfig : IComponentData
    {
        public Entity EnemyPrefabEntity;
        public int AmountToSpawn;
    }
}