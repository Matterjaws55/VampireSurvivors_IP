using Unity.Entities;
using Unity.Mathematics;

namespace Dots.Enemy.Components
{
    public struct DeathAnimation : IComponentData
    {
        public float Duration;
        public float ElapsedTime;
        public float3 StartPosition;
        public float3 Direction;
    }
}