using Dots.Player.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.Composites;


namespace Dots.Player.Systems
{
	public partial class PlayerInputSystem : SystemBase
	{
		private PlayerInputMaps _controls;

		[BurstCompile]
		protected override void OnCreate()
		{
            _controls = new PlayerInputMaps();
			_controls.Enable();

		}

		[BurstCompile]
        protected override void OnUpdate()
		{

            float2 move = _controls.TopDown.Movement.ReadValue<Vector2>();

			foreach(var inputData in SystemAPI.Query<RefRW<PlayerInput>>())
			{
				inputData.ValueRW.MoveInput = move;
			}
		}

		[BurstCompile]
        protected override void OnDestroy()
		{
			_controls.Disable();
		}
	}
}