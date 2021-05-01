using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script will give a health bar for everything that is losing health from taking damage
public class HealthBar : MonoBehaviour
{
    public Slider slider;

    //When calling this function, the health bar goes full
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    //This puts the health of the object to the health bar
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
