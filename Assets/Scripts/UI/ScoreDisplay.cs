using System;
using Dots.Components;
using TMPro;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        public int[] _scoreThresholds;

        private int _currentUpgrade = 0;

        public delegate void OnUpgradeSignature();
        
        public static event OnUpgradeSignature OnUpgrade;

        [SerializeField] private Image _scoreProgress;

        private float _scoreStart = 0;
        
        private void Update()
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (world == null) return;

            var scoreQuery = world.EntityManager.CreateEntityQuery(typeof(Score));
            if (scoreQuery.TryGetSingleton<Score>(out var score))
            {
                _scoreText.text = $"Score: {score.Value}";
                
                if (_scoreProgress)
                {
                    
                    float maxScore = 0;
                    float curScore = score.Value;
                    if(_currentUpgrade < _scoreThresholds.Length)
                        maxScore = _scoreThresholds[_currentUpgrade];
                    else
                    {
                        maxScore = _scoreStart +1;
                        curScore = 0;
                    }
                    
                    _scoreProgress.fillAmount = (curScore - _scoreStart) / (maxScore - _scoreStart);
                }
            }

            if (_currentUpgrade < _scoreThresholds.Length && score.Value >= _scoreThresholds[_currentUpgrade])
            {
                _scoreStart = _scoreThresholds[_currentUpgrade];
                // Upgrade the player
                OnUpgrade?.Invoke();
                _currentUpgrade++;
            }


        }


    }
}