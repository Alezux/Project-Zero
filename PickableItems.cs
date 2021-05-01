using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This script is controlling the pickable items, which works as an inventory system
public class PickableItems : MonoBehaviour
{
    public GameObject[] backpackIcons;
    public GameObject[] icons;
    public GameObject[] items;
    public GameObject[] removeButtons;
    public GameObject[] selectionButtons;

    public GrenadeShoot GrenadeShoot;
    public PlayerShooting PlayerShooting;

    public Animator playerAnimation;
    public GameObject grenadeTempParent;
    public GameObject tempParent;
    public LineRenderer lineVisual;
    public Transform guide;

    public bool carrying;
    public bool weaponCarrying;
    public bool grenadeCarrying;
    public bool keyCardCarrying;
    public bool keyCard2Carrying;
    private float range = 8f;

    void Start()
    {
        GrenadeShoot = GetComponentInChildren<GrenadeShoot>();
        PlayerShooting = GetComponentInChildren<PlayerShooting>();

        items[0].GetComponent<Rigidbody>().useGravity = true;
        items[1].GetComponent<Rigidbody>().useGravity = true;
        items[2].GetComponent<Rigidbody>().useGravity = true;
        items[3].GetComponent<Rigidbody>().useGravity = true;

        removeButtons[0].SetActive(false);
        removeButtons[5].SetActive(false);
        removeButtons[6].SetActive(false);
        removeButtons[7].SetActive(false);

        //The codes below work for moving data between scenes, for example when you have key card equipped and you have moved to another scene, the keycard will come with you.
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (keyCardCarrying == true && sceneName == "middle_start")
        {
            PickKeyCard();
        }

        if (weaponCarrying == true && sceneName == "middle_start")
        {
            PickWeapon();
        }

        if (sceneName == "ProtoMapWIP")
        {
            PickWeapon();
        }

        //End of lines
    }

    void Update()
    {
        //When the interaction button is pressed close to pickable items, the player will carry the pickable item
        if (Input.GetButtonDown("Interaction"))
        {
            Pickup();
        }
    }

    //When calling this function, the player will carry weapon. A lot of actions will happen afterwards.
    public void PickWeapon()
    {
        Switch();
        carrying = true;
        weaponCarrying = true;
        grenadeCarrying = false;
        keyCardCarrying = false;
        keyCard2Carrying = false;
        PlayerShooting.ammoDisplay.GetComponent<Text>().enabled = true;
        PlayerShooting.enabled = true;

        backpackIcons[0].SetActive(false);
        icons[0].SetActive(true);
        items[0].SetActive(true);
        items[0].GetComponent<Rigidbody>().useGravity = false;
        items[0].GetComponent<Rigidbody>().isKinematic = true;
        items[0].transform.position = guide.transform.position;
        items[0].transform.rotation = guide.transform.rotation;
        items[0].transform.parent = tempParent.transform;
        removeButtons[0].SetActive(true);
        removeButtons[1].SetActive(false);
        selectionButtons[2].SetActive(false);
        selectionButtons[3].SetActive(false);
    }

    //When calling this function, the player will carry grenade. A lot of actions will happen afterwards.
    public void PickGrenade()
    {
        Switch();
        carrying = true;
        grenadeCarrying = true;
        weaponCarrying = false;
        keyCardCarrying = false;
        keyCard2Carrying = false;
        GrenadeShoot.enabled = true;
        lineVisual.enabled = true;
        GrenadeShoot.ammoGrenadeDisplay.GetComponent<Text>().enabled = true;

        backpackIcons[1].SetActive(false);
        icons[1].SetActive(true);
        items[1].SetActive(true);
        items[1].GetComponent<Rigidbody>().useGravity = false;
        items[1].GetComponent<Rigidbody>().isKinematic = true;
        items[1].transform.position = grenadeTempParent.transform.position;
        items[1].transform.rotation = grenadeTempParent.transform.rotation;
        items[1].transform.parent = grenadeTempParent.transform;
        items[1].GetComponent<LineRenderer>().enabled = true;
        items[1].GetComponent<GrenadeShoot>().enabled = true;
        removeButtons[5].SetActive(true);
        removeButtons[2].SetActive(false);
        selectionButtons[6].SetActive(false);
        selectionButtons[7].SetActive(false);
    }

    //When calling this function, the player will carry key card. A lot of actions will happen afterwards.
    public void PickKeyCard()
    {
        Switch();
        carrying = true;
        weaponCarrying = false;
        grenadeCarrying = false;
        keyCardCarrying = true;
        keyCard2Carrying = false;

        backpackIcons[2].SetActive(false);
        icons[2].SetActive(true);
        items[2].SetActive(true);
        items[2].GetComponent<Rigidbody>().useGravity = false;
        items[2].GetComponent<Rigidbody>().isKinematic = true;
        items[2].transform.position = guide.transform.position;
        items[2].transform.rotation = guide.transform.rotation;
        items[2].transform.parent = tempParent.transform;
        removeButtons[6].SetActive(true);
        removeButtons[3].SetActive(false);
        selectionButtons[10].SetActive(false);
        selectionButtons[11].SetActive(false);
    }

    //When calling this function, the player will carry second keycard. A lot of actions will happen afterwards.
    public void PickKeyCard2()
    {
        Switch();
        carrying = true;
        weaponCarrying = false;
        grenadeCarrying = false;
        keyCardCarrying = false;
        keyCard2Carrying = true;

        backpackIcons[3].SetActive(false);
        icons[3].SetActive(true);
        items[3].SetActive(true);
        items[3].GetComponent<Rigidbody>().useGravity = false;
        items[3].GetComponent<Rigidbody>().isKinematic = true;
        items[3].transform.position = guide.transform.position;
        items[3].transform.rotation = guide.transform.rotation;
        items[3].transform.parent = tempParent.transform;
        removeButtons[7].SetActive(true);
        removeButtons[4].SetActive(false);
        selectionButtons[14].SetActive(false);
        selectionButtons[15].SetActive(false);
    }

    //When calling this function, the weapon's carrying selection buttons will appear on the hud
    public void WeaponCarrySelections()
    {
        if (weaponCarrying == true)
        {
            selectionButtons[0].SetActive(true);
            selectionButtons[1].SetActive(true);
        }
    }

    //When calling this function, the grenade's carrying selection buttons will appear on the hud
    public void GrenadeCarrySelections()
    {
        if (grenadeCarrying == true)
        {
            selectionButtons[4].SetActive(true);
            selectionButtons[5].SetActive(true);
        }
    }

    //When calling this function, the keycard's carrying selection buttons will appear on the hud
    public void KeyCardCarrySelections()
    {
        if (keyCardCarrying == true)
        {
            selectionButtons[8].SetActive(true);
            selectionButtons[9].SetActive(true);
        }
    }

    //When calling this function, the second keycard's carrying selection buttons will appear on the hud
    public void KeyCard2CarrySelections()
    {
        if (keyCard2Carrying == true)
        {
            selectionButtons[12].SetActive(true);
            selectionButtons[13].SetActive(true);
        }
    }

    //When calling this function, the weapon's selection buttons will appear in the inventory menu
    public void WeaponSelections()
    {
        if (backpackIcons[0].activeInHierarchy)
        {
            selectionButtons[2].SetActive(true);
            selectionButtons[3].SetActive(true);
        }
    }

    //When calling this function, the grenade's selection buttons will appear in the inventory menu
    public void GrenadeSelections()
    {
        if (backpackIcons[1].activeInHierarchy)
        {
            selectionButtons[6].SetActive(true);
            selectionButtons[7].SetActive(true);
        }
    }

    //When calling this function, the keycard's selection buttons will appear in the inventory menu
    public void KeyCardSelections()
    {
        if (backpackIcons[2].activeInHierarchy)
        {
            selectionButtons[10].SetActive(true);
            selectionButtons[11].SetActive(true);
        }
    }

    //When calling this function, the second keycard's selection buttons will appear in the inventory menu
    public void KeyCard2Selections()
    {
        if (backpackIcons[3].activeInHierarchy)
        {
            selectionButtons[14].SetActive(true);
            selectionButtons[15].SetActive(true);
        }
    }

    //When calling this function, the weapon will be moved to inventory from carrying
    public void EquipWeapon()
    {
        carrying = false;
        weaponCarrying = false;
        PlayerShooting.ammoDisplay.GetComponent<Text>().enabled = false;

        backpackIcons[0].SetActive(true);
        items[0].SetActive(false);
        icons[0].SetActive(false);
        items[0].GetComponent<Rigidbody>().useGravity = true;
        items[0].GetComponent<Rigidbody>().isKinematic = false;
        items[0].transform.parent = null;
        items[0].transform.position = guide.transform.position;
        removeButtons[0].SetActive(false);
        removeButtons[1].SetActive(true);
        selectionButtons[0].SetActive(false);
        selectionButtons[1].SetActive(false);
    }

    //When calling this function, the grenade will be moved to inventory from carrying
    public void EquipGrenade()
    {
        carrying = false;
        grenadeCarrying = false;
        GrenadeShoot.ammoGrenadeDisplay.GetComponent<Text>().enabled = false;
        GrenadeShoot.enabled = false;
        backpackIcons[1].SetActive(true);
        items[1].SetActive(false);
        icons[1].SetActive(false);
        items[1].GetComponent<Rigidbody>().useGravity = true;
        items[1].GetComponent<Rigidbody>().isKinematic = false;
        items[1].transform.parent = null;
        items[1].transform.position = guide.transform.position;
        removeButtons[5].SetActive(false);
        removeButtons[2].SetActive(true);
        selectionButtons[4].SetActive(false);
        selectionButtons[5].SetActive(false);
    }

    //When calling this function, the keycard will be moved to inventory from carrying
    public void EquipKeyCard()
    {
        carrying = false;
        keyCardCarrying = false;

        backpackIcons[2].SetActive(true);
        items[2].SetActive(false);
        icons[2].SetActive(false);
        items[2].GetComponent<Rigidbody>().useGravity = true;
        items[2].GetComponent<Rigidbody>().isKinematic = false;
        items[2].transform.parent = null;
        items[2].transform.position = guide.transform.position;
        removeButtons[6].SetActive(false);
        removeButtons[3].SetActive(true);
        selectionButtons[8].SetActive(false);
        selectionButtons[9].SetActive(false);
    }

    //When calling this function, the second keycard will be moved to inventory from carrying
    public void EquipKeyCard2()
    {
        carrying = false;
        keyCard2Carrying = false;

        backpackIcons[3].SetActive(true);
        icons[3].SetActive(false);
        items[3].SetActive(false);
        items[3].GetComponent<Rigidbody>().useGravity = true;
        items[3].GetComponent<Rigidbody>().isKinematic = false;
        items[3].transform.parent = null;
        items[3].transform.position = guide.transform.position;
        removeButtons[7].SetActive(false);
        removeButtons[4].SetActive(true);
        selectionButtons[12].SetActive(false);
        selectionButtons[13].SetActive(false);
    }

    //When calling this function, the player will drop weapon
    public void DropWeapon()
    {
        carrying = false;
        weaponCarrying = false;
        PlayerShooting.ammoDisplay.GetComponent<Text>().enabled = false;
        PlayerShooting.enabled = false;

        backpackIcons[0].SetActive(false);
        icons[0].gameObject.SetActive(false);
        items[0].SetActive(true);
        items[0].GetComponent<Rigidbody>().useGravity = true;
        items[0].GetComponent<Rigidbody>().isKinematic = false;
        items[0].transform.parent = null;
        items[0].transform.position = guide.transform.position;
        removeButtons[0].SetActive(false);
        removeButtons[1].SetActive(false);
        selectionButtons[0].SetActive(false);
        selectionButtons[1].SetActive(false);
        selectionButtons[2].SetActive(false);
        selectionButtons[3].SetActive(false);
    }

    //When calling this function, the player will drop grenade
    public void DropGrenade()
    {
        carrying = false;
        grenadeCarrying = false;
        GrenadeShoot.ammoGrenadeDisplay.GetComponent<Text>().enabled = false;
        GrenadeShoot.enabled = false;
        backpackIcons[1].SetActive(false);
        icons[1].gameObject.SetActive(false);
        items[1].SetActive(true);
        items[1].GetComponent<LineRenderer>().enabled = false;
        items[1].GetComponent<Rigidbody>().useGravity = true;
        items[1].GetComponent<Rigidbody>().isKinematic = false;
        items[1].transform.parent = null;
        items[1].transform.position = guide.transform.position;
        removeButtons[5].SetActive(false);
        selectionButtons[4].SetActive(false);
        selectionButtons[5].SetActive(false);
        selectionButtons[6].SetActive(false);
        selectionButtons[7].SetActive(false);
    }

    //When calling this function, the player will drop keycard
    public void DropKeyCard()
    {
        carrying = false;
        keyCardCarrying = false;

        backpackIcons[2].SetActive(false);
        icons[2].gameObject.SetActive(false);
        items[2].SetActive(true);
        items[2].GetComponent<Rigidbody>().useGravity = true;
        items[2].GetComponent<Rigidbody>().isKinematic = false;
        items[2].transform.parent = null;
        items[2].transform.position = guide.transform.position;
        removeButtons[6].SetActive(false);
        removeButtons[3].SetActive(false);
        selectionButtons[8].SetActive(false);
        selectionButtons[9].SetActive(false);
        selectionButtons[10].SetActive(false);
        selectionButtons[11].SetActive(false);
    }

    //When calling this function, the player will drop second keycard
    public void DropKeyCard2()
    {
        carrying = false;
        keyCard2Carrying = false;

        backpackIcons[3].SetActive(false);
        icons[3].gameObject.SetActive(false);
        items[3].SetActive(true);
        items[3].GetComponent<Rigidbody>().useGravity = true;
        items[3].GetComponent<Rigidbody>().isKinematic = false;
        items[3].transform.parent = null;
        items[3].transform.position = guide.transform.position;
        removeButtons[7].SetActive(false);
        removeButtons[4].SetActive(false);
        selectionButtons[12].SetActive(false);
        selectionButtons[13].SetActive(false);
        selectionButtons[14].SetActive(false);
        selectionButtons[15].SetActive(false);
    }

    //When calling this function, the hud inventory and the inventory menu will switch places with items when clicked
    public void Switch()
    {
        //Moves weapon from hud inventory to inventory menu
        if (icons[0].activeInHierarchy == true)
        {
            EquipWeapon();
        }

        //Moves grenade from hud inventory to inventory menu
        if (icons[1].activeInHierarchy == true)
        {
            EquipGrenade();
        }

        //Moves keycard from hud inventory to inventory menu
        if (icons[2].activeInHierarchy == true)
        {
            EquipKeyCard();
        }

        //Moves second keycard from hud inventory to inventory menu
        if (icons[3].activeInHierarchy == true)
        {
            EquipKeyCard2();
        }
    }

    //When calling this function, the player will pick up items
    public void Pickup()
    {
        //When not carrying
        if (carrying == false)
        {
            //When player is close to weapon and presses the interaction button, the weapon will come to player's hand
            if (items[0] != null && weaponCarrying == false)
            {
                if ((guide.transform.position - items[0].transform.position).sqrMagnitude < range * range)
                {
                    PickWeapon();
                    playerAnimation.SetTrigger("Interact");
                    PlayerShooting.enabled = true;
                    removeButtons[0].SetActive(true);
                }
            }

            //When player is close to grenade and presses the interaction button, the grenade will come to player's hand
            if (items[1] != null && grenadeCarrying == false)
            {
                if ((guide.transform.position - items[1].transform.position).sqrMagnitude < range * range)
                {
                    PickGrenade();
                    playerAnimation.SetTrigger("Interact");
                    GrenadeShoot.enabled = true;
                    removeButtons[5].SetActive(true);
                }
            }

            //When player is close to keycard and presses the interaction button, the keycard will come to player's hand
            if (items[2] != null && keyCardCarrying == false)
            {
                if ((guide.transform.position - items[2].transform.position).sqrMagnitude < range * range)
                {
                    PickKeyCard();
                    playerAnimation.SetTrigger("Interact");
                    removeButtons[6].SetActive(true);
                }
            }

            //When player is close to second keycard and presses the interaction button, the second keycard will come to player's hand
            if (items[3] != null && keyCard2Carrying == false)
            {
                if ((guide.transform.position - items[3].transform.position).sqrMagnitude < range * range)
                {
                    PickKeyCard2();
                    playerAnimation.SetTrigger("Interact");
                    removeButtons[7].SetActive(true);
                }
            }
        }

        //When carrying
        if (carrying == true)
        {
            //When carrying weapon and collecting another item, another item goes to inventory
            if (weaponCarrying == true && items[0] != null)
            {
                //When carrying weapon and collecting grenade, grenade goes to inventory
                if (items[1] != null)
                {
                    if ((guide.transform.position - items[1].transform.position).sqrMagnitude < range * range)
                    {
                        backpackIcons[1].SetActive(true);
                        items[1].SetActive(false);
                        removeButtons[2].SetActive(true);
                    }
                }

                //When carrying weapon and collecting keycard, keycard goes to inventory
                if (items[2] != null)
                {
                    if ((guide.transform.position - items[2].transform.position).sqrMagnitude < range * range)
                    {
                        backpackIcons[2].SetActive(true);
                        items[2].SetActive(false);
                        removeButtons[3].SetActive(true);
                    }
                }

                //When carrying weapon and collecting second keycard, second keycard goes to inventory
                if (items[3] != null)
                {
                    if ((guide.transform.position - items[3].transform.position).sqrMagnitude < range * range)
                    {
                        backpackIcons[3].SetActive(true);
                        items[3].SetActive(false);
                        removeButtons[4].SetActive(true);
                    }
                }
            }

            //When carrying grenade and collecting another item, another item goes to inventory
            if (grenadeCarrying == true && items[1] != null)
            {
                //When carrying grenade and collecting weapon, weapon goes to inventory
                if (items[0] != null)
                {
                    if ((guide.transform.position - items[0].transform.position).sqrMagnitude < range * range)
                    {
                        backpackIcons[0].SetActive(true);
                        items[0].SetActive(false);
                        removeButtons[1].SetActive(true);
                    }
                }

                //When carrying grenade and collecting keycard, keycard goes to inventory
                if (items[2] != null)
                {
                    if ((guide.transform.position - items[2].transform.position).sqrMagnitude < range * range)
                    {
                        backpackIcons[2].SetActive(true);
                        items[2].SetActive(false);
                        removeButtons[3].SetActive(true);
                    }
                }

                //When carrying grenade and collecting second keycard, second keycard goes to inventory
                if (items[3] != null)
                {
                    if ((guide.transform.position - items[3].transform.position).sqrMagnitude < range * range)
                    {
                        backpackIcons[3].SetActive(true);
                        items[3].SetActive(false);
                        removeButtons[4].SetActive(true);
                    }
                }
            }

            //When carrying keycard and collecting another item, another item goes to inventory
            if (keyCardCarrying == true && items[2] != null)
            {
                //When carrying keycard and collecting weapon, weapon goes to inventory
                if (items[0] != null)
                {
                    if ((guide.transform.position - items[0].transform.position).sqrMagnitude < range * range)
                    {
                        backpackIcons[0].SetActive(true);
                        items[0].SetActive(false);
                        removeButtons[1].SetActive(true);
                    }
                }

                //When carrying keycard and collecting grenade, grenade goes to inventory
                if (items[1] != null)
                {
                    if ((guide.transform.position - items[1].transform.position).sqrMagnitude < range * range)
                    {
                        backpackIcons[1].SetActive(true);
                        items[1].SetActive(false);
                        removeButtons[2].SetActive(true);
                    }
                }

                //When carrying keycard and collecting second keycard, second keycard goes to inventory
                if (items[3] != null)
                {
                    if ((guide.transform.position - items[3].transform.position).sqrMagnitude < range * range)
                    {
                        backpackIcons[3].SetActive(true);
                        items[3].SetActive(false);
                        removeButtons[4].SetActive(true);
                    }
                }
            }

            //When carrying second keycard and collecting another item, another item goes to inventory
            if (keyCard2Carrying == true && items[3] != null)
            {
                //When carrying second keycard and collecting weapon, weapon goes to inventory
                if (items[0] != null)
                {
                    if ((guide.transform.position - items[0].transform.position).sqrMagnitude < range * range)
                    {
                        backpackIcons[0].SetActive(true);
                        items[0].SetActive(false);
                        removeButtons[1].SetActive(true);
                    }
                }

                //When carrying second keycard and collecting grenade, grenade goes to inventory
                if (items[1] != null)
                {
                    if ((guide.transform.position - items[1].transform.position).sqrMagnitude < range * range)
                    {
                        backpackIcons[1].SetActive(true);
                        items[1].SetActive(false);
                        removeButtons[2].SetActive(true);
                    }
                }

                //When carrying second keycard and collecting keycard, keycard goes to inventory
                if (items[2] != null)
                {
                    if ((guide.transform.position - items[2].transform.position).sqrMagnitude < range * range)
                    {
                        backpackIcons[2].SetActive(true);
                        items[2].SetActive(false);
                        removeButtons[3].SetActive(true);
                    }
                }
            }

            //When grenade is destroyed, every feature of grenade will get destroyed
            if (items[1] == null)
            {
                carrying = false;
                grenadeCarrying = false;
                backpackIcons[1].SetActive(false);
                icons[1].SetActive(false);
                removeButtons[5].SetActive(false);
                removeButtons[2].SetActive(false);
                selectionButtons[4].SetActive(false);
                selectionButtons[5].SetActive(false);
                selectionButtons[6].SetActive(false);
                selectionButtons[7].SetActive(false);
            }

            //When keycard is destroyed, every feature and action of keycard will get destroyed
            if (items[2] == null)
            {
                carrying = false;
                keyCardCarrying = false;
                backpackIcons[2].SetActive(false);
                icons[2].SetActive(false);
                removeButtons[6].SetActive(false);
                removeButtons[3].SetActive(false);
                selectionButtons[8].SetActive(false);
                selectionButtons[9].SetActive(false);
                selectionButtons[10].SetActive(false);
                selectionButtons[11].SetActive(false);
            }

            //When second keycard is destroyed, every feature and action of second keycard will get destroyed
            if (items[3] == null)
            {
                carrying = false;
                keyCard2Carrying = false;
                backpackIcons[3].SetActive(false);
                icons[3].SetActive(false);
                removeButtons[7].SetActive(false);
                removeButtons[4].SetActive(false);
                selectionButtons[12].SetActive(false);
                selectionButtons[13].SetActive(false);
                selectionButtons[14].SetActive(false);
                selectionButtons[15].SetActive(false);
            }
        }
    }
}