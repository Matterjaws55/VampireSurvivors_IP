using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Dots.Player.Components
{
    public class PlayerInputAuthoring : MonoBehaviour
    {
        private static Entity GetEntity(TransformUsageFlags dynamic)
        {
            throw new System.NotImplementedException();
        }

        private class Baker : Baker<PlayerTagAuthoring>
        {
            public override void Bake(PlayerTagAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new PlayerInput());
            }
        }
    }

    public struct PlayerInput : IComponentData
    {
        public float2 MoveInput;
    }
}