using Unity.Entities;
using UnityEngine;

namespace Dots.Components
{
    public class HealthAuthoring : MonoBehaviour
    {
        public int Health = 10;
        public int ScoreOnDeath = 100;

        private class HealthBaker : Baker<HealthAuthoring>
        {
            public override void Bake(HealthAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new Health()
                {
                    Value = authoring.Health,
                    ScoreOnDeath = authoring.ScoreOnDeath
                });
            }
        }
    }

    public struct Health : IComponentData
    {
        public int Value;
        public int ScoreOnDeath;
    }

}