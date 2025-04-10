using Unity.Entities;
using UnityEngine;

namespace Dots.Enemy.Components
{
    public class EnemyTagAuthoring : MonoBehaviour
    {
        private class EnemyTagBaker : Baker<EnemyTagAuthoring>
        {
            public override void Bake(EnemyTagAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new EnemyTag());
            }
        }
    }

    public struct EnemyTag : IComponentData
    {

    }
}