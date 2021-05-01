using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This script will give the actions to the death menu, aka game over screen
public class DeathMenu : MonoBehaviour
{
    //When calling this function, the game will restart
    public void PlayGame()
    {
        SceneManager.LoadScene("ProtoMapWIP");
    }

    //When calling this function, the game will go to menu scene
    public void ReturnMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    //When calling this function, the game application will close
    public void QuitGame()
    {
        Application.Quit();
    }
}
