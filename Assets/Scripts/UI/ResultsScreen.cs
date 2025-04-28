using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ResultsScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;


        private void Start()
        {
            int lastScore = PlayerPrefs.GetInt("LastScore", 0);
            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            
            if (lastScore > highScore)
            {
                PlayerPrefs.SetInt("HighScore", lastScore);
                highScore = lastScore;
            }
            
            _scoreText.text = $"SCORE: {lastScore}\nHIGH SCORE: {highScore}";
        }
    }
}