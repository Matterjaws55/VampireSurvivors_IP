using Dots.Attacks.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;


namespace Dots.Attacks
{
    public class ProjectileSpawner : MonoBehaviour
    {
        private Entity _spawnedEntity;
        private World _world;
        private EntityManager _entityManager;

        [SerializeField] private int _damage = 10;
        [SerializeField] private float _radius = 1f;

        private void Start()
        {
            _world = World.DefaultGameObjectInjectionWorld;
            _entityManager = _world.EntityManager;

            // Create entity and add components directly
            _spawnedEntity = _entityManager.CreateEntity();
            
            _entityManager.SetName(_spawnedEntity, gameObject.name);

            // Add transform component
            _entityManager.AddComponentData(_spawnedEntity, LocalTransform.FromPosition(transform.position));
            
            // Add projectile component with values from the authoring component
            _entityManager.AddComponentData(_spawnedEntity, new Projectile 
            { 
                Damage = _damage,
                Radius = _radius
            });

            // In ProjectileSpawner.cs, add this line after adding the Projectile component
            _entityManager.AddComponentData(_spawnedEntity, new DamagedEntitites
            {
                Entities = new FixedList128Bytes<Entity>()
            });
        }

        private void Update()
        {
            if (_entityManager != null && _entityManager.Exists(_spawnedEntity))
            {
                _entityManager.SetComponentData(_spawnedEntity, LocalTransform.FromPosition(transform.position));
            }
        }

        private void OnDestroy()
        {
           /* if (_entityManager != null && _entityManager.Exists(_spawnedEntity))
            {
                _entityManager.DestroyEntity(_spawnedEntity);
            }*/
        }
    }
}
