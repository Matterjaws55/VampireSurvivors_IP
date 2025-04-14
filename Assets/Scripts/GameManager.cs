using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("PlayerHealth")]
    public Image healthBar;
    public float healthAmount = 100f;

    public GameObject player;

    public GameObject pauseParent;
    private bool isPaused;
    public GameObject healthCanvas;

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
        
        healthCanvas.SetActive(false);
    }

    public void UnHideUI()
    {
        
        healthCanvas.SetActive(true);
    }

    public static bool LoadLvl(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1.0f;
        return true;
    }

    public void RestartScene()
    {
        try
        {
            LoadLvl(1);
            Debug.Log("Level Restarted!");
        }

        catch (System.ArgumentException exception)
        {
            LoadLvl(0);
            Debug.Log("Reverting to test scene" + exception.ToString());
        }
        
    }

    public void GoToMainMenu()
    {
        try
        {
            LoadLvl(-1);
            Debug.Log("Level Restarted!");
        }

        catch (System.ArgumentException exception)
        {
            LoadLvl(1);
            Debug.Log("Reverting to level scene" + exception.ToString());
        }

        finally

        {
            Debug.Log("Restarted Level as Main Menu does not yet exist on this branch");
        }
    }
}
