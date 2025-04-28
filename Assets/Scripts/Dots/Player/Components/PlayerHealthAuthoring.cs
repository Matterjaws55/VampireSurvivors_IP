using Unity.Entities;
using UnityEngine;

namespace Dots.Player.Components
{
    public class PlayerHealthAuthoring : MonoBehaviour
    {
        public int Health = 100;
        private class PlayerHealthAuthoringBaker : Baker<PlayerHealthAuthoring>
        {
            public override void Bake(PlayerHealthAuthoring authoring)
            {
                Entity entity = GetEntity(authoring.gameObject);
                
                AddComponent(entity, new PlayerHealth
                {
                    Value = authoring.Health,
                });

            }
        }
    }
}