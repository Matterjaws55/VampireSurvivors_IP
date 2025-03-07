using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.VisualScripting;
using UnityEngine;

namespace Dots
{
    public class MoveTowardsAuthoring : MonoBehaviour
    {


        public float MoveSpeed = 5f;
        private class MoveTowardsBaker : Baker<MoveTowardsAuthoring>
        {
            public override void Bake(MoveTowardsAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new MoveSpeed()
                {
                    Value = authoring.MoveSpeed,
                });
                
                AddComponent(entity, new TargetPosition());
            }
        }
    }

    public struct MoveSpeed  : IComponentData
    {
        public float Value;
    }
    
    public struct TargetPosition : IComponentData
    {
        public float3 Value;
    }
}