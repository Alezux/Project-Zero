using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Tämä koodi saa näyttämään pelin hudille pistoolin ammusten määrän
public class AmmoWeaponDisplay : MonoBehaviour
{
    public PlayerShooting weapon;
    public Text ammoWeaponDisplay;

    //Alkaessa koodi hakee pistoolista toiminnot, jotta saadaan pelin hudille näkymään pistoolin ammusten määrä
    void Start()
    {
        weapon = GetComponentInChildren<PlayerShooting>();
        ammoWeaponDisplay = GameObject.FindGameObjectWithTag("AmmoWeaponDisplay").GetComponent<Text>();

        //Alkaessa sulkee pistoolin ammojen määrän hudilta, avautuu vasta kerätessä pistoolin
        ammoWeaponDisplay.enabled = false;
    }

    //Päivittäessä saa hudille näkymään pistoolin ammojen määrän
    void Update()
    {
        ammoWeaponDisplay.text = weapon.currentAmmo + " / " + weapon.maxAmmo;
    }
}