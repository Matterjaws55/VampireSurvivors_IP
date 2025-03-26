using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("PlayerHealth")]
    public Image healthBar;
    public float healthAmount = 100f;

    public GameObject player;

    private bool isPaused;

    [Header("UI & Menu Parents")]
    public GameObject healthCanvas;
    public GameObject pauseParent;
    public GameObject scoreParent;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
                PauseGame();
            else
                UnPauseGame();
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        healthBar.fillAmount = healthAmount / 100f;
    }

    public void PauseGame()
    {
        isPaused = true;
        HideUI();
        pauseParent.SetActive(true);
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        isPaused = false;
        pauseParent.SetActive(false);
        UnHideUI();
        Time.timeScale = 1;
    }

    public void HideUI()
    {
        scoreParent.SetActive(false);
        healthCanvas.SetActive(false);
    }

    public void UnHideUI()
    {
        scoreParent.SetActive(true);
        healthCanvas.SetActive(true);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
