using Unity.Entities;

namespace Dots.Enemy.Components
{
    public struct DamageAnimation : IComponentData
    {
        public float Duration; // Remaining time for the animation
        public float ShakeIntensity; // Intensity of the shake
        public float ScaleFactor; // Scale adjustment factor
    }
}