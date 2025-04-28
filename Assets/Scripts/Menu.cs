using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject mainMenuParent;
    public GameObject mainCreditsParent;

    // Start is called before the first frame update
    void Start()
    {
        if (mainMenuParent != null)
        {
            mainMenuParent.SetActive(true);
            mainCreditsParent.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        GameManager.LoadLvl(1);
    }

    public void OpenCreditsMain()
    {
        mainMenuParent.SetActive(false);
        mainCreditsParent.SetActive(true);
    }

    public void CloseCreditsMain()
    {
        mainMenuParent.SetActive(true);
        mainCreditsParent.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToCharSelect()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("CharacterSelect");
    }
}
