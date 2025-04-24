using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Dots
{
    public class SpawnEnemyConfigAuthoring : MonoBehaviour
    {
        [FormerlySerializedAs("_cubePrefab")] public GameObject _enemyPrefab;
        public int _amountToSpawn;
        public float _sizeX = 100;
        public float _sizeZ = 100;
        private class SpawnEnemyConfigAuthoringBaker : Baker<SpawnEnemyConfigAuthoring>
        {
            public override void Bake(SpawnEnemyConfigAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.None);
                Entity enemyPrefabEntity = GetEntity(authoring._enemyPrefab, TransformUsageFlags.Dynamic);

                
                AddComponent(entity, new SpawnEnemyConfig
                {
                    EnemyPrefabEntity = enemyPrefabEntity,
                    AmountToSpawn = authoring._amountToSpawn,
                    SizeX = authoring._sizeX,
                    SizeZ = authoring._sizeZ
                });
            }
            
        }
    }
    
    public struct SpawnEnemyConfig : IComponentData
    {
        public Entity EnemyPrefabEntity;
        public int AmountToSpawn;
        public float SizeX;
        public float SizeZ;
    }
}