using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelSelection;
    public GameObject credits;
    public Toggle toggle;

    void Start()
    {
        int ring = PlayerPrefs.GetInt("Ring", 1);
        
        if (ring == 1)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
    }

    public void LevelSelectionMenu()
    {
        mainMenu.SetActive(false);
        levelSelection.SetActive(true);
    }

    public void CreditsMenu()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }

    public void ReturnBackToMenu()
    {
        mainMenu.SetActive(true);
        levelSelection.SetActive(false);
        credits.SetActive(false);
    }

    public void SelectLevel(int level)
    {
        SceneManager.LoadScene("Level_" + level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RingMovement()
    {
        if (toggle.isOn == true)
        {
            PlayerPrefs.SetInt("Ring", 1);
        }
        else if (toggle.isOn == false)
        {
            PlayerPrefs.SetInt("Ring", 0);
        }
    }
}
