using Dots.Components;
using Unity.Entities;
using UnityEngine;

namespace Dots.Attacks.Components
{
    public class HurtBoxAuthoring : MonoBehaviour
    {
        [SerializeField]
        private int _damage = 10;
        private class HurtBoxBaker : Baker<HurtBoxAuthoring>
        {

            public override void Bake(HurtBoxAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new HurtBox()
                {
                    Damage = authoring._damage
                });
            }


        }

    }

    public struct HurtBox : IComponentData
    {
        public int Damage;
    }
}