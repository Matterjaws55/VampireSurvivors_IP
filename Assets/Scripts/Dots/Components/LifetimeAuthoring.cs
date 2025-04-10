using Unity.Entities;
using UnityEngine;

namespace Dots.Components
{
	public class LifetimeAuthoring : MonoBehaviour
	{


		public float Lifetime = 1f;
		private class LifeTimeBaker : Baker<LifetimeAuthoring>
		{
			public override void Bake(LifetimeAuthoring authoring)
			{
				Entity entity = GetEntity(TransformUsageFlags.Dynamic);

				AddComponent(entity, new Lifetime()
				{
					Value = authoring.Lifetime,
				});

			}
		}
	}

	public struct Lifetime : IComponentData
	{
		public float Value;
	}

}