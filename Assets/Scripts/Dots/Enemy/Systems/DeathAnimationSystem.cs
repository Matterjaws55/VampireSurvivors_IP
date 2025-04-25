using Dots.Enemy.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Dots.Enemy.Systems
{


    public partial struct DeathAnimationSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);
        
            var deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (transform, animation, entity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<DeathAnimation>>().WithEntityAccess())
            {
                animation.ValueRW.ElapsedTime += deltaTime;
                
                float t = animation.ValueRW.ElapsedTime / animation.ValueRO.Duration;
                
                transform.ValueRW.Scale = math.lerp(1f, 0f, t); // Scale down to 0
                
                // Float upward
                transform.ValueRW.Position.y += 20.5f * deltaTime;

                // Slowly rotate
                transform.ValueRW.Rotation = math.mul(
                    transform.ValueRW.Rotation,
                    quaternion.Euler(0, 0, 4f * deltaTime) // z-axis spin
                );
                
                if (animation.ValueRW.ElapsedTime >= animation.ValueRO.Duration)
                {
                    ecb.DestroyEntity(entity);
                }
            }
        }
    }
    
}