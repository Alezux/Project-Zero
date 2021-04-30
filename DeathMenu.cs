using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Tämä koodi antaa kuoleman välimenun napille toiminnat
public class DeathMenu : MonoBehaviour
{
    //Tämä nappi aloittaa pelin alusta
    public void PlayGame()
    {
        SceneManager.LoadScene("ProtoMapWIP");
    }

    //Tämä nappi vie takaisin päämenuun
    public void ReturnMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    //Tämä nappi lopettaa pelin
    public void QuitGame()
    {
        Application.Quit();
    }
}
