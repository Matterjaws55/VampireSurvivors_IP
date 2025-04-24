using System.Collections;
using Dots;
using Dots.Player.Components;
using Unity.Entities;
using UnityEngine;

namespace Player
{
    public class PlayerEntitySetter : MonoBehaviour
    {
        private EntityReference _entityReference;
        private const int MaxRetries = 5;
        private const float RetryDelay = 0.1f;

        private IEnumerator Start()
        {
            _entityReference = GetComponent<EntityReference>();
            
            for (int i = 0; i < MaxRetries; i++)
            {
                var world = World.DefaultGameObjectInjectionWorld;
                if (world == null || !world.IsCreated)
                {
                    yield return new WaitForSeconds(RetryDelay);
                    continue;
                }

                var entityManager = world.EntityManager;
                var query = entityManager.CreateEntityQuery(typeof(PlayerTag));
                var playerEntities = query.ToEntityArray(Unity.Collections.Allocator.Temp);

                if (playerEntities.Length > 0)
                {
                    var playerEntity = playerEntities[0];
                    if (entityManager.Exists(playerEntity))
                    {
                        _entityReference.SetEntity(playerEntity);
                        playerEntities.Dispose();
                        yield break;
                    }
                }

                playerEntities.Dispose();
                yield return new WaitForSeconds(RetryDelay);
            }

            Debug.LogError("Failed to find player entity after multiple attempts");
        }
    }
}