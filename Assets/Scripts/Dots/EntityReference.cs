using System;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Dots
{
    public class EntityReference : MonoBehaviour
    {
        public Entity Entity;
        private LocalTransform _entityTransform;

        private void Update()
        {
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            
            if (entityManager.HasComponent<LocalTransform>(Entity))
            {
                _entityTransform = entityManager.GetComponentData<LocalTransform>(Entity);
            

                transform.position = _entityTransform.Position;
                transform.rotation = _entityTransform.Rotation;
            }
        }

        public void SetEntity(Entity entity)
        {
            Entity = entity;
            
        }
    }
}