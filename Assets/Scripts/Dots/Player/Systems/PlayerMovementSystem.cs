using System;
using System.Numerics;
using Dots.Components;
using Dots.Player.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Dots.Player.Systems
{
    public partial struct PlayerMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerInput>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach(var (input, transform, moveSpeed) in SystemAPI.Query<RefRO<PlayerInput>, RefRW<LocalTransform>, RefRO<MoveSpeed>>())
            {
                float speed = moveSpeed.ValueRO.Value;

                float3 moveDelta = new float3(input.ValueRO.MoveInput.x, 0, input.ValueRO.MoveInput.y);
                moveDelta *= speed;
                moveDelta *= SystemAPI.Time.DeltaTime;

                
                float3 pos = transform.ValueRO.Position + moveDelta;

                pos.x = Mathf.Clamp(pos.x, -160, 160);
                pos.z = Mathf.Clamp(pos.z, -160, 160);
                
                transform.ValueRW.Position = pos;

            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}