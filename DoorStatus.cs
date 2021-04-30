using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tämä koodi antaa ovenlukijan valolle materiaalin vaihdon
public class DoorStatus : MonoBehaviour
{
    public GameObject[] doorStatus;
    public Material newMaterial;
    public Material originalMaterial;
    public Material[] materials;

    //Alkaessa koodi hakee asioita
    public void Start()
    {
        materials[0] = originalMaterial;
        materials = GetComponentInChildren<Renderer>().materials;
        GetComponentInChildren<Renderer>().materials = materials;
    }

    //Tätä funktiota kutsumalla saa tietyille oville materiaalin vaihdon
    public void ButtonDoor()
    {
        doorStatus[0].GetComponentInChildren<Renderer>().material = newMaterial;
        doorStatus[1].GetComponentInChildren<Renderer>().material = newMaterial;
    }

    //Tätä funktiota kutsumalla saa tietyille oville materiaalin vaihdon
    public void KeyCardDoor()
    {
        doorStatus[4].GetComponentInChildren<Renderer>().material = newMaterial;
        doorStatus[5].GetComponentInChildren<Renderer>().material = newMaterial;
    }

    //Tätä funktiota kutsumalla saa tietyille oville materiaalin vaihdon
    public void KeyCardDoor2()
    {
        doorStatus[12].GetComponentInChildren<Renderer>().material = newMaterial;
        doorStatus[15].GetComponentInChildren<Renderer>().material = newMaterial;
    }

    //Tätä funktiota kutsumalla saa tietyille oville materiaalin vaihdon
    public void OpenDoor()
    {
        doorStatus[3].GetComponentInChildren<Renderer>().material = newMaterial;
        doorStatus[6].GetComponentInChildren<Renderer>().material = newMaterial;
    }

    //Tätä funktiota kutsumalla saa tietyille oville materiaalin vaihdon
    public void CloseDoor()
    {
        doorStatus[3].GetComponentInChildren<Renderer>().material = originalMaterial;
        doorStatus[6].GetComponentInChildren<Renderer>().material = originalMaterial;
    }
}
