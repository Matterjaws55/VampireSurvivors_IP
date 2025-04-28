using Unity.Entities;

namespace Dots.Player.Components
{
    public struct PlayerHealth : IComponentData
    {

        public int Value;
        public float _safeTime;
    }
}