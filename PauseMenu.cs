using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This script will give actions for the pause menu
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject uiDisabled;

    //When calling this button, the game will go to pause menu
    public void Pause()
    {
        pauseMenu.SetActive(true);
        uiDisabled.SetActive(false);
        Time.timeScale = 0;
    }

    //When calling this function, the game will exit pause menu
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        uiDisabled.SetActive(true);
    }

    //When calling this function, the game goes back to menu scene
    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
        Time.timeScale = 1;
    }

    //When calling this function, the game will close
    public void QuitGame()
    {
        Application.Quit();
    }
}
