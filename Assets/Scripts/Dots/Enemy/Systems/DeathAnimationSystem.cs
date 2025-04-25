using Dots.Enemy.Components;
using Dots.Player.Components;
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
            state.RequireForUpdate<PlayerTag>();

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);

            var deltaTime = SystemAPI.Time.DeltaTime;
            var playerTransform = SystemAPI.GetComponent<LocalTransform>(
                SystemAPI.GetSingletonEntity<PlayerTag>()
            );

            foreach (var (transform, animation, entity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<DeathAnimation>>().WithEntityAccess())
            {
                if (animation.ValueRW.ElapsedTime == 0)
                {
                    // Store start and end positions
                    animation.ValueRW.StartPosition = transform.ValueRO.Position;
                    animation.ValueRW.Direction = playerTransform.Position - transform.ValueRO.Position;
                }

                animation.ValueRW.ElapsedTime += deltaTime;
                float t = animation.ValueRW.ElapsedTime / animation.ValueRO.Duration;

                // Height follows a parabolic arc peaking at t=0.5
                float height = math.sin(t * math.PI) * 8f; // Increased height for more dramatic arc

                // Linear interpolation from start to player position
                var position = math.lerp(
                    animation.ValueRW.StartPosition, 
                    playerTransform.Position, 
                    t
                );
                position.y += height;

                transform.ValueRW.Position = position;
                transform.ValueRW.Scale = math.lerp(1f, 0f, t);
                transform.ValueRW.Rotation = math.mul(
                    transform.ValueRW.Rotation,
                    quaternion.Euler(0, 0, 9f * deltaTime)
                );

                if (t >= 1)
                {
                    ecb.DestroyEntity(entity);
                }
            }
        }
        
    }
    
}