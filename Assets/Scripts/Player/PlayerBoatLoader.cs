using System;
using System.Collections;
using Dots.Components;
using Dots.Player.Components;
using UI;
using Unity.Entities;
using UnityEngine;

namespace Player
{
    public class PlayerBoatLoader : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _boats;
        
        [SerializeField]
        private float[] _moveSpeeds;
        
        [SerializeField]
        private int[] _healths;
        private void Start()
        {
            int selectedBoat = PlayerPrefs.GetInt("SelectedCharacter", 0);
            
            for(int i = 0; i < _boats.Length; i++)
            {
                if (i == selectedBoat)
                {
                    _boats[i].SetActive(true);
                }
                else
                {
                    _boats[i].SetActive(false);
                }
            }

            StartCoroutine(WaitToSetupStats(selectedBoat));

        }

        IEnumerator WaitToSetupStats(int selectedBoat)
        {
            yield return new WaitForSeconds(0.2f);
            
            var world = World.DefaultGameObjectInjectionWorld;
            if (world != null)
            {
                

                var playerQuery = world.EntityManager.CreateEntityQuery(typeof(PlayerTag));

                if (playerQuery.TryGetSingletonEntity<PlayerTag>(out var playerEntity))
                {
                    if (world.EntityManager.HasComponent<PlayerHealth>(playerEntity))
                    {
                        var health = world.EntityManager.GetComponentData<PlayerHealth>(playerEntity);
                        health.Value = _healths[selectedBoat];
                        world.EntityManager.SetComponentData(playerEntity, health);

                        HealthDisplay._maxHealth = _healths[selectedBoat];
                    }

                    if (world.EntityManager.HasComponent<MoveSpeed>(playerEntity))
                    {
                        var moveSpeed = world.EntityManager.GetComponentData<MoveSpeed>(playerEntity);
                        moveSpeed.Value = _moveSpeeds[selectedBoat];
                        world.EntityManager.SetComponentData(playerEntity, moveSpeed);
                    }
                }
            }
        }
    }
}