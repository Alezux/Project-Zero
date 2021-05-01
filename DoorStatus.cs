using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script works for the door statuses, the door status will make action when door is opened
public class DoorStatus : MonoBehaviour
{
    public GameObject[] doorStatus;
    public Material newMaterial;
    public Material originalMaterial;
    public Material[] materials;

    public void Start()
    {
        materials[0] = originalMaterial;
        materials = GetComponentInChildren<Renderer>().materials;
        GetComponentInChildren<Renderer>().materials = materials;
    }

    //When calling certain of all the functions below, the door statuses will get activated when they are opened through certain different options
    public void ButtonDoor()
    {
        doorStatus[0].GetComponentInChildren<Renderer>().material = newMaterial;
        doorStatus[1].GetComponentInChildren<Renderer>().material = newMaterial;
    }

    public void KeyCardDoor()
    {
        doorStatus[4].GetComponentInChildren<Renderer>().material = newMaterial;
        doorStatus[5].GetComponentInChildren<Renderer>().material = newMaterial;
    }

    public void KeyCardDoor2()
    {
        doorStatus[12].GetComponentInChildren<Renderer>().material = newMaterial;
        doorStatus[15].GetComponentInChildren<Renderer>().material = newMaterial;
    }

    public void OpenDoor()
    {
        doorStatus[3].GetComponentInChildren<Renderer>().material = newMaterial;
        doorStatus[6].GetComponentInChildren<Renderer>().material = newMaterial;
    }

    public void CloseDoor()
    {
        doorStatus[3].GetComponentInChildren<Renderer>().material = originalMaterial;
        doorStatus[6].GetComponentInChildren<Renderer>().material = originalMaterial;
    }

    //End of lines
}
