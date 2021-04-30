using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Tämä koodi antaa paussimenulle toimivat napit
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject uiDisabled;

    //Tämä nappi avaa paussimenun ja pysäyttää pelin
    public void Pause()
    {
        pauseMenu.SetActive(true);
        uiDisabled.SetActive(false);
        Time.timeScale = 0;
    }

    //Tämä nappi palaa peliin
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        uiDisabled.SetActive(true);
    }

    //Tämä nappi vie päämenuun
    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
        Time.timeScale = 1;
    }

    //Tämä nappi lopettaa pelin
    public void QuitGame()
    {
        Application.Quit();
    }
}
