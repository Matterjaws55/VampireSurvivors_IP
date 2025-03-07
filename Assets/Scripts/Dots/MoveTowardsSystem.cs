using System;
using System.Numerics;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.VisualScripting;

namespace Dots
{
    public partial struct MoveTowardsSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>(); 
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float3 playerPos = float3.zero;
            
            foreach (var transform in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<PlayerTag>())
            {
                playerPos = transform.ValueRO.Position;
                break; // Only take the first player entity found
            }
            
            foreach (RefRW<TargetPosition> targetPosition in SystemAPI.Query<RefRW<TargetPosition>>())
            {
                targetPosition.ValueRW.Value = playerPos;
            }
            
            float deltaTime = SystemAPI.Time.DeltaTime;
            foreach ((RefRW<LocalTransform> localTransform, RefRO<TargetPosition> targetPosition, RefRW<MoveSpeed> moveSpeed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<TargetPosition>, RefRW<MoveSpeed>>())
            {
                float3 curPos = localTransform.ValueRO.Position;
                float3 targPos = targetPosition.ValueRO.Value;

                float speed = moveSpeed.ValueRO.Value;
                
                float3 dir = math.normalizesafe(targPos - curPos);
                
                float3 movement = dir * speed * deltaTime;
                
                localTransform.ValueRW.Position = curPos + movement;
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}