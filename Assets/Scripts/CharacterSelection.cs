using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters;
    public int[] scoreCounts;

    public Button _startButton;
    public TMP_Text _scoreUnlockText;
    public int selectedCharacter = 0;

    private int _highScore;

    private void Start()
    {
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateUnlockText();
    }

    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
        UpdateUnlockText();
    }

    public void PreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
        UpdateUnlockText();
    }

    private void UpdateUnlockText()
    {
        if (_highScore >= scoreCounts[selectedCharacter])
        {
            _scoreUnlockText.enabled = false;
            _startButton.interactable = true;
        }
        else
        {
            _scoreUnlockText.enabled = true;
            _scoreUnlockText.text = $"Unlock progress: {_highScore}/{scoreCounts[selectedCharacter]}";
            _startButton.interactable = false;

        }
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
        SceneManager.LoadScene("dotsTest", LoadSceneMode.Single);
    }
}
