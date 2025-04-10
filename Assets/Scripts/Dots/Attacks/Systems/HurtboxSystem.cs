using System.ComponentModel;
using Dots.Attacks.Components;
using Dots.Components;
using Dots.Enemy.Components;
using Dots.Player.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace Dots.Attacks.Systems
{
    public partial struct HurtboxSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<HurtBox>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }

    struct TriggerHurtboxPlayer : ITriggerEventsJob
    {
        [ReadOnly(true)] public ComponentLookup<HurtBox> hurtboxContainer;
        [ReadOnly(true)] public ComponentLookup<Health> enemyContainer;

        public void Execute(TriggerEvent triggerEvent)
        {
            Entity entityA = triggerEvent.EntityA;
            Entity entityB = triggerEvent.EntityB;

            
            
        }
    }
}