using System;
using Dots;
using Unity.Entities;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EntityReference _enemyPrefabGameobject;

        private void Start()
        {
            SpawnEnemySystem spawnEnemySystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<SpawnEnemySystem>();
            
            spawnEnemySystem.OnSpawnEnemy += OnSpawnEnemy;
        }

        private void OnSpawnEnemy(Entity enemy)
        {
            EntityReference enemySpawned = Instantiate(_enemyPrefabGameobject);

            enemySpawned.SetEntity(enemy);
        }
    }
}