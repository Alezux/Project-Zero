using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Tämä koodi saa näyttämään pelin hudille kranaatin ammusten määrän
public class AmmoGrenadeDisplay : MonoBehaviour
{
    public GrenadeShoot grenade;
    public Text ammoGrenadeDisplay;

    //Alkaessa koodi hakee muista koodeista toimintoja tähän koodiin
    void Start()
    {
        grenade = GetComponentInChildren<GrenadeShoot>();
        ammoGrenadeDisplay = GameObject.FindGameObjectWithTag("AmmoGrenadeDisplay").GetComponent<Text>();

        //Alkaessa sulkee kranaatin ammusten määrän hudilta, avautuu vasta kerätessä kranaatin
        ammoGrenadeDisplay.enabled = false;
    }

    //Päivittäessä saa hudille näkymään kranaatin ammusten määrän
    void Update()
    {
        ammoGrenadeDisplay.text = grenade.currentAmmo + " / " + grenade.maxAmmo;
    }
}
