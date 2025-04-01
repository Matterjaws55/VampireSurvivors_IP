using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.VisualScripting;
using UnityEngine;

namespace Dots.Components
{
    public class TargetPositionAuthoring : MonoBehaviour
    {

        private class TargetPositionBaker : Baker<TargetPositionAuthoring>
        {
            public override void Bake(TargetPositionAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                
                
                AddComponent(entity, new TargetPosition());
            }
        }
    }

    
    public struct TargetPosition : IComponentData
    {
        public float3 Value;
    }
}