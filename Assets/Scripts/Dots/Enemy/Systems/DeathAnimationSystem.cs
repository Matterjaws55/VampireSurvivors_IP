using Dots.Enemy.Components;
using Unity.Burst;
using Unity.Entities;

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

            foreach (var (animation, entity) in SystemAPI.Query<RefRW<DeathAnimation>>().WithEntityAccess())
            {
                animation.ValueRW.ElapsedTime += deltaTime;
            
                if (animation.ValueRW.ElapsedTime >= animation.ValueRO.Duration)
                {
                    ecb.DestroyEntity(entity);
                }
            }
        }
    }
    
}