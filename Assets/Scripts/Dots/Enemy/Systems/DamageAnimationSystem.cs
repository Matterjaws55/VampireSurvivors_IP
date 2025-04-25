using Dots.Enemy.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Dots.Enemy.Systems
{
    public partial struct DamageAnimationSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            foreach (var (damageAnimation, localTransform, entity) in
                     SystemAPI.Query<RefRW<DamageAnimation>, RefRW<LocalTransform>>().WithEntityAccess())
            {
                // Reduce the animation duration
                damageAnimation.ValueRW.Duration -= deltaTime;

                // Apply shake effect
                float shakeOffset = (float)math.sin(SystemAPI.Time.ElapsedTime * 20f) * damageAnimation.ValueRO.ShakeIntensity;
                localTransform.ValueRW.Position += new float3(shakeOffset, 0, shakeOffset);

                // Apply scale effect
                float scaleAdjustment = (float)math.sin(SystemAPI.Time.ElapsedTime * 10f) * damageAnimation.ValueRO.ScaleFactor;
                localTransform.ValueRW.Scale = math.clamp(1f + scaleAdjustment, 0.9f, 1.1f);

                // Schedule removal of the component when the animation is complete
                if (damageAnimation.ValueRO.Duration <= 0)
                {
                    ecb.RemoveComponent<DamageAnimation>(entity);
                }
            }

            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}