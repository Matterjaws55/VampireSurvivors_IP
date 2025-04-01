using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.VisualScripting;
using UnityEngine;

namespace Dots.Components
{
    public class MoveSpeedAuthoring : MonoBehaviour
    {


        public float MoveSpeed = 5f;
        private class MoveSpeedBaker : Baker<MoveSpeedAuthoring>
        {
            public override void Bake(MoveSpeedAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new MoveSpeed()
                {
                    Value = authoring.MoveSpeed,
                });

            }
        }
    }

    public struct MoveSpeed : IComponentData
    {
        public float Value;
    }

}