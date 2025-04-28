using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private GameObject upgradeMenu;
    public GameObject healthCanvas;



    private void OnEnable()
    {
        ScoreDisplay.OnUpgrade += ShowUpgradeMenu;
    }

    private void OnDisable()
    {
        ScoreDisplay.OnUpgrade -= ShowUpgradeMenu;
    }

    public void ShowUpgradeMenu()
    {
        upgradeMenu.SetActive(true);
        healthCanvas.SetActive(false);
        Time.timeScale = 0;
    }

    public void CloseUpgradeMenu()
    {
        upgradeMenu.SetActive(false);
        healthCanvas.SetActive(true);
        Time.timeScale = 1;
    }
}
