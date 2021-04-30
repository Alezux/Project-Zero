using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Tämä koodi antaa menuskenelle toimivat napit
public class MainMenu : MonoBehaviour
{
    public GameObject instructionsMenu;
    
    //Tämä nappi aloittaa pelin
    public void PlayGame()
    {
        SceneManager.LoadScene("middle_start");
    }

    public void BackToMainMenu()
    {
        instructionsMenu.SetActive(false);
    }

    //Tämä nappi avaa ohjemenun
    public void Instructions()
    {
        instructionsMenu.SetActive(true);
    }

    //Tämä nappi lopettaa pelin
    public void QuitGame()
    {
        Application.Quit();
    }
}
