using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This script will give the actions for the main menu scene
public class MainMenu : MonoBehaviour
{
    public GameObject instructionsMenu;
    
    //When calling this function, the game will start
    public void PlayGame()
    {
        SceneManager.LoadScene("middle_start");
    }

    //When calling this function, the instructions menu goes off
    public void BackToMainMenu()
    {
        instructionsMenu.SetActive(false);
    }

    //When calling this function, the instructions menu goes on
    public void Instructions()
    {
        instructionsMenu.SetActive(true);
    }

    //When calling this function, the game application wil close
    public void QuitGame()
    {
        Application.Quit();
    }
}
