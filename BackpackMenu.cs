using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script works with the backpack menu system that is part of the inventory system
public class BackpackMenu : MonoBehaviour
{
    public GameObject backpackMenu;

    //When calling this function, the backpack menu will open
    public void Open()
    {
        backpackMenu.SetActive(true);
    }

    //When calling this function, the backpack menu will close
    public void Exit()
    {
        backpackMenu.SetActive(false);
    }
}
