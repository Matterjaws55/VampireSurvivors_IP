using Unity.Entities;

namespace Dots.Enemy.Components
{
    public struct DeathAnimation : IComponentData
    {
        public float Duration;
        public float ElapsedTime;
    }
}