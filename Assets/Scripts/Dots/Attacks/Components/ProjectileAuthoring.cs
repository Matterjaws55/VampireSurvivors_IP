using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Dots.Attacks.Components
{
    public struct Projectile : IComponentData
    {
        public int Damage;
        public float Radius;
    }

    public class ProjectileAuthoring : MonoBehaviour
    {
        public int _damage = 10;
        public float _radius = 1f;

        private class ProjectileBaker : Baker<ProjectileAuthoring>
        {
            public override void Bake(ProjectileAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new Projectile
                {
                    Damage = authoring._damage,
                    Radius = authoring._radius
                });
            }
        }
    }
}