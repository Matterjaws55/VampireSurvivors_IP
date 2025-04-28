using Unity.Entities;
using UnityEngine;

namespace Dots.Enemy.Components
{
    public class DamageAmountAuthoring : MonoBehaviour
    {

        public int Damage;
        private class DamageAmountAuthoringBaker : Baker<DamageAmountAuthoring>
        {
            public override void Bake(DamageAmountAuthoring authoring)
            {
                Entity entity = GetEntity(authoring.gameObject);
                
                AddComponent(entity, new DamageAmount()
                {
                    Value = authoring.Damage
                });

            }
        }
    }
    
    public struct DamageAmount : IComponentData
    {
        public int Value; // The amount of damage the enemy deals
    }
}