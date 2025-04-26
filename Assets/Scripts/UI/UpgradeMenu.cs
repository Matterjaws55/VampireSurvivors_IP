using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private GameObject upgradeMenu;
    public GameObject healthCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        UpgradePickUp.OnUpgradePickedUp += ShowUpgradeMenu;
    }

    private void OnDisable()
    {
        UpgradePickUp.OnUpgradePickedUp -= ShowUpgradeMenu;
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
