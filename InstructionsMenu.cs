using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Tämä koodi antaa ohjemenulle napin palata takaisin menuskeneen
public class InstructionsMenu : MonoBehaviour
{
    //Tämä nappi vie takaisin päämenuun
    public void ReturnMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}