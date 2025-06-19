using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public GameObject player;
    public GameObject winUI;
    public GameObject gameoverUI;
    public GameObject superMeter;
    public GameObject pauseButton;
    public GameObject pauseMenu;
    public float chargeupRate = 30;
    public float duration = 5;
    public int misses = 0;
    public int chancesBeforeComboBreak = 2;
    private int combo = 0;
    private bool superMode = false;
    private float originalTimescale = 1;

    private IEnumerator SuperMode()
    {
        superMode = true;
        player.GetComponent<PlayerScript>().doubleSpeedSet = false;
        player.GetComponent<PlayerScript>().onSuper = true;
        
        yield return new WaitForSeconds(duration);
        
        player.GetComponent<PlayerScript>().doubleSpeedSet = false;
        player.GetComponent<PlayerScript>().onSuper = false;
        superMeter.GetComponent<UnityEngine.UI.Image>().color = new Color(255, 255, 0, 255);
        superMeter.transform.localScale = new Vector3(0, 1, 1);
        superMode = false;
    }

    public void WinGame()
    {
        AudioListener.pause = true;
        winUI.SetActive(true);
        gameoverUI.SetActive(false);
        pauseMenu.SetActive(false);
        pauseButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void Pause()
    {
        if (Time.timeScale > 0)
        {
            AudioListener.pause = true;
            pauseButton.SetActive(false);
            pauseMenu.SetActive(true);
            originalTimescale = Time.timeScale;
            Time.timeScale = 0f;
        }
        else
        {
            AudioListener.pause = false;
            pauseButton.SetActive(true);
            pauseMenu.SetActive(false);
            Time.timeScale = originalTimescale;
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene("MainMenu");
    }
    
    public void IncreaseSuperMeter()
    {
        if (superMode == false) 
        {
            misses = 0;
            combo += 1;
            superMeter.transform.localScale += new Vector3((1 / chargeupRate), 0, 0);

            if (combo == chargeupRate)
            {
                StartCoroutine(SuperMode());
                combo = 0;
                superMeter.GetComponent<UnityEngine.UI.Image>().color = new Color(255, 0, 0, 255);
            }
        }
    }

    public void LoseCombo()
    {
        if (combo > 0)
        {
            misses += 1;
        }

        if (misses >= chancesBeforeComboBreak)
        {
            misses = 0;
            combo = 0;
            superMeter.transform.localScale = new Vector3(0, 1, 1);
        }
    }

    public void GameOver()
    {
        AudioListener.pause = true;
        gameoverUI.SetActive(true);
        pauseMenu.SetActive(false);
        pauseButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void ResetGame()
    {
        winUI.SetActive(false);
        combo = 0;
        superMeter.transform.localScale = new Vector3(0, 1, 1);
        AudioListener.pause = false;
        gameoverUI.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    [ContextMenu ("Next Level")]
    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            winUI.SetActive(false);
            combo = 0;
            superMeter.transform.localScale = new Vector3(0, 1, 1);
            AudioListener.pause = false;
            gameoverUI.SetActive(false);
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
