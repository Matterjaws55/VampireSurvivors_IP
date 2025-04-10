using Unity.Entities;
using UnityEngine;

namespace Dots.Player.Components
{
    public class PlayerTagAuthoring : MonoBehaviour
    {
        private class Baker : Baker<PlayerTagAuthoring>
        {
            public override void Bake(PlayerTagAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new PlayerTag());
            }        
        }
    }
    
    public struct PlayerTag : IComponentData
    {
        
    }
}