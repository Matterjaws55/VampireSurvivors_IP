using Cinemachine;
using Dots.Components;
using Dots.Player.Components;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class HealthDisplay : MonoBehaviour
    {
        public delegate void OnDeadSignature();

        [SerializeField] private CinemachineImpulseSource _cinemachineImpulse;
        private bool _dead = false;
        [SerializeField] private Image _healthBar;

        public static float _maxHealth = 100;
        private void Update()
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (world == null) return;

            var healthQuery = world.EntityManager.CreateEntityQuery(typeof(PlayerHealth));
            if (healthQuery.TryGetSingleton<PlayerHealth>(out var health))
            {
                float newHealth = (float)health.Value / _maxHealth;

                if (newHealth != _healthBar.fillAmount && _cinemachineImpulse)
                {
                    _cinemachineImpulse.GenerateImpulseWithForce(1);
                }
                _healthBar.fillAmount = newHealth;
                
                if(!_dead && health.Value <= 0)
                {
                    _dead = true;
                    var scoreQuery = world.EntityManager.CreateEntityQuery(typeof(Score));
                    if (scoreQuery.TryGetSingleton<Score>(out var score))
                    {
                        PlayerPrefs.SetInt("LastScore", score.Value);

                        SceneManager.LoadScene("Results");
                    }
                }

            }


        }
    }
}