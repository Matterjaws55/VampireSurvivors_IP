using Dots.Components;
using TMPro;
using Unity.Entities;
using UnityEngine;

namespace UI
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        private void Update()
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (world == null) return;

            var scoreQuery = world.EntityManager.CreateEntityQuery(typeof(Score));
            if (scoreQuery.TryGetSingleton<Score>(out var score))
            {
                _scoreText.text = $"Score: {score.Value}";
            }
        }
    }
}