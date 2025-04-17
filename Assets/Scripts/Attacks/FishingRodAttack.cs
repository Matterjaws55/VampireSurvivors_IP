using Dots;
using UnityEngine;
using Unity.Entities;

namespace Attacks
{
    public class FishingRodAttack : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            // Check if the collided object is an ECS entity
            if (other.TryGetComponent(out EntityReference entityReference))
            {
                Entity collidedEntity = entityReference.Entity;
                Debug.Log($"Collided with ECS Entity: {collidedEntity}");
            
                /*
                // Perform actions on the collided entity
                var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                if (entityManager.HasComponent<Health>(collidedEntity))
                {
                    var health = entityManager.GetComponentData<Health>(collidedEntity);
                    health.Value -= 10; // Example: Reduce health
                    entityManager.SetComponentData(collidedEntity, health);
                }*/
            }
        }
    }
}