using Dots.Components;
using Dots.Player.Components;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Dots.Systems
{
    public partial struct LifetimeSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Lifetime>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entityCommandBuffer = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);
            float deltaTime = SystemAPI.Time.DeltaTime;


            foreach (var (lifetime, entity) in SystemAPI.Query<RefRW<Lifetime>>().WithEntityAccess())
            {
                lifetime.ValueRW.Value -= deltaTime;


                if(lifetime.ValueRO.Value <= 0)
                {
                    entityCommandBuffer.DestroyEntity(entity);
                }
            }
            entityCommandBuffer.Playback(state.EntityManager);

            entityCommandBuffer.Dispose();

        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }

    }
}