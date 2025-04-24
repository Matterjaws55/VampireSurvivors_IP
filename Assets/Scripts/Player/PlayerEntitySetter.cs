using Dots;
using Dots.Player.Components;
using Unity.Entities;
using UnityEngine;

namespace Player
{
    public class PlayerEntitySetter : MonoBehaviour
    {
        private EntityReference _entityReference;

        private void Start()
        {
            _entityReference = GetComponent<EntityReference>();
            var world = World.DefaultGameObjectInjectionWorld;
            var entityManager = world.EntityManager;
            
            // Find player entity with PlayerTag component
            var query = entityManager.CreateEntityQuery(typeof(PlayerTag));
            if (query.TryGetSingletonEntity<PlayerTag>(out var playerEntity))
            {
                _entityReference.SetEntity(playerEntity);
            }
        }
    }
}