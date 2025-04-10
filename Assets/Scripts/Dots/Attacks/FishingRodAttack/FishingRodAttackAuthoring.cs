using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Dots.Attacks.FishingRodAttack
{

    public class FishingRodAttackAuthoring : MonoBehaviour
    {
        private class FishingRodAttackAuthoringBaker : Baker<FishingRodAttackAuthoring>
        {
            public override void Bake(FishingRodAttackAuthoring authoring)
            {

            }
        }

    }

    public struct FishingRodAttack : IComponentData
    {
    }




}