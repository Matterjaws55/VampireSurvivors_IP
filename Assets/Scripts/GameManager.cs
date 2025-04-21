using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("PlayerHealth")]
    public Image healthBar;
    public float currentHealth = 100f;

    public GameObject player;

    public delegate void HealthPoints(GameManager source, float oldHealth, float newHealth);
    public event HealthPoints OnHealthChanged;

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
        float oldHealth = currentHealth;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);

        OnHealthChanged?.Invoke(this, oldHealth, currentHealth);
        healthBar.fillAmount = currentHealth / 100;
    }

    public void Heal(int healingAmount)
    {
        currentHealth += healingAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
        healthBar.fillAmount = currentHealth / 100;
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

        if (sceneIndex < 0 || sceneIndex >= SceneManager.sceneCountInBuildSettings)
            throw new System.ArgumentOutOfRangeException("Invalid scene index: " + sceneIndex);

        return true;
    }

    //this should work
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

    //this method results in an exception
    public void GoToMainMenu()
    {
        try
        {
            LoadLvl(3);
            Debug.Log("Went to Main Menu");
        }

        catch (System.ArgumentException exception)
        {
            RestartScene();
            Debug.Log("Restarting scene" + exception.ToString());
        }

        finally

        {
            Debug.Log("Restarted Level as Main Menu does not yet exist on this branch");
            if (pauseParent.activeInHierarchy)
            {
                UnHideUI();
                pauseParent.SetActive(false);
            }
        }
    }
}
