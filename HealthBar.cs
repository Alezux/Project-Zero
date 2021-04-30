using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Tämä koodi saa pelaajalle elinajan linjan
public class HealthBar : MonoBehaviour
{
    public Slider slider;

    //Pelaajan elinmäärä alkaa täydestä
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    //Tämä asettaa linjalle pelaajan elämän
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
