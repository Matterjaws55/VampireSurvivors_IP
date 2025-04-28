using Dots.Enemy.Components;
using Dots.Player.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Dots.Player.Systems
{
    public partial struct PlayerHitByEnemySystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            const float damageRadius = 1.5f;

            foreach (var (playerTransform, health, playerEntity) in
                     SystemAPI.Query<RefRO<LocalTransform>, RefRW<Player.Components.PlayerHealth>>().WithEntityAccess())
            {
                float3 playerPosition = playerTransform.ValueRO.Position;
                playerPosition.y = 0;

                if (health.ValueRO._safeTime > 0)
                {
                    health.ValueRW._safeTime -= SystemAPI.Time.DeltaTime;
                    continue;
                }
                
                foreach (var (enemyTransform, enemyDamage,enemyEntity) in
                         SystemAPI.Query<RefRO<LocalTransform>, RefRO<DamageAmount>>().WithEntityAccess())
                {
                    float3 enemyPosition = enemyTransform.ValueRO.Position;

                    if (math.distance(playerPosition, enemyPosition) <= damageRadius)
                    {
                        health.ValueRW.Value -= enemyDamage.ValueRO.Value;
                        health.ValueRW._safeTime = 0.5f;
                        break;
                    }
                    
                }
            }
        }
    }
}