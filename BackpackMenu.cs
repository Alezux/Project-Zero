using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackMenu : MonoBehaviour
{
    public GameObject backpackMenu;

    public void Open()
    {
        backpackMenu.SetActive(true);
    }

    public void Exit()
    {
        backpackMenu.SetActive(false);
    }
}
