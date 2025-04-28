using System;
using Dots.Components;
using TMPro;
using Unity.Entities;
using UnityEngine;

namespace UI
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        public int[] _scoreThresholds;

        private int _currentUpgrade = 0;

        public delegate void OnUpgradeSignature();
        
        public static event OnUpgradeSignature OnUpgrade;
        
        private void Update()
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (world == null) return;

            var scoreQuery = world.EntityManager.CreateEntityQuery(typeof(Score));
            if (scoreQuery.TryGetSingleton<Score>(out var score))
            {
                _scoreText.text = $"Score: {score.Value}";
            }

            if (_currentUpgrade < _scoreThresholds.Length && score.Value >= _scoreThresholds[_currentUpgrade])
            {
                // Upgrade the player
                OnUpgrade?.Invoke();
                _currentUpgrade++;
            }
        }


    }
}