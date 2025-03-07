using Unity.Entities;
using UnityEngine;

namespace Dots
{
    public class PlayerTagAuthoring : MonoBehaviour
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
                AddComponent(entity, new PlayerTag());
            }        
        }
    }
    
    public struct PlayerTag : IComponentData
    {
        
    }
}