using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This script will give the actions for the instructions menu
public class InstructionsMenu : MonoBehaviour
{
    //When calling this function, the game will go back to menu scene
    public void ReturnMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}