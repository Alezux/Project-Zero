using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Tämä koodi hallitsee pelissä kerättävien esineiden toimintoja
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

    //Päivittäessä koodi hakee, avaa ja sulkee asioita
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

        //Kun astut tehtäväkentälle, ase on kädessäsi automaattisesti
        if (sceneName == "ProtoMapWIP")
        {
            PickWeapon();
        }
    }

    //Päivittäessä koodi antaa pelin esineille kantamisen
    void Update()
    {
        //Kun painat toimintanappia, koodi kutsuu funktion joka hoitaa kantamisen asiat
        if (Input.GetButtonDown("Interaction"))
        {
            Pickup();
        }
    }

    //Tätä funktiota kutsumalla pelaaja kantaa asetta
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

    //Tätä funktiota kutsumalla pelaaja kantaa kranaattia
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

    //Tätä funktiota kutsumalla pelaaja kantaa avainkorttia
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

    //Tätä funktiota kutsumalla pelaaja kantaa toista avainkorttia
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

    //Tätä funktiota kutsumalla aseen valintanapit ilmestyvät painaessa hudilla
    public void WeaponCarrySelections()
    {
        if (weaponCarrying == true)
        {
            selectionButtons[0].SetActive(true);
            selectionButtons[1].SetActive(true);
        }
    }

    //Tätä funktiota kutsumalla kranaatin valintanapit ilmestyvät painaessa hudilla
    public void GrenadeCarrySelections()
    {
        if (grenadeCarrying == true)
        {
            selectionButtons[4].SetActive(true);
            selectionButtons[5].SetActive(true);
        }
    }

    //Tätä funktiota kutsumalla avainkortin valintanapit ilmestyvät painaessa hudilla
    public void KeyCardCarrySelections()
    {
        if (keyCardCarrying == true)
        {
            selectionButtons[8].SetActive(true);
            selectionButtons[9].SetActive(true);
        }
    }

    //Tätä funktiota kutsumalla toisen avainkortin valintanapit ilmestyvät painaessa hudilla
    public void KeyCard2CarrySelections()
    {
        if (keyCard2Carrying == true)
        {
            selectionButtons[12].SetActive(true);
            selectionButtons[13].SetActive(true);
        }
    }

    //Tätä funktiota kutsumalla valintanapit ilmestyvät painaessa inventaariossa
    public void WeaponSelections()
    {
        if (backpackIcons[0].activeInHierarchy)
        {
            selectionButtons[2].SetActive(true);
            selectionButtons[3].SetActive(true);
        }
    }

    //Tätä funktiota kutsumalla valintanapit ilmestyvät painaessa inventaariossa
    public void GrenadeSelections()
    {
        if (backpackIcons[1].activeInHierarchy)
        {
            selectionButtons[6].SetActive(true);
            selectionButtons[7].SetActive(true);
        }
    }

    //Tätä funktiota kutsumalla valintanapit ilmestyvät painaessa inventaariossa
    public void KeyCardSelections()
    {
        if (backpackIcons[2].activeInHierarchy)
        {
            selectionButtons[10].SetActive(true);
            selectionButtons[11].SetActive(true);
        }
    }

    //Tätä funktiota kutsumalla valintanapit ilmestyvät painaessa inventaariossa
    public void KeyCard2Selections()
    {
        if (backpackIcons[3].activeInHierarchy)
        {
            selectionButtons[14].SetActive(true);
            selectionButtons[15].SetActive(true);
        }
    }

    //Tätä funktiota kutsumalla pelaaja siirtää aseen inventaarioon
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

    //Tätä funktiota kutsumalla pelaaja siirtää aseen inventaarioon
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

    //Tätä funktiota kutsumalla pelaaja siirtää avainkortin inventaarioon
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

    //Tätä funktiota kutsumalla pelaaja siirtää toisen avainkortin inventaarioon
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

    //Tätä funktiota kutsumalla pelaaja pudottaa aseen
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

    //Tätä funktiota kutsumalla pelaaja pudottaa kranaatin
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

    //Tätä funktiota kutsumalla pelaaja pudottaa avainkortin
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

    //Tätä funktiota kutsumalla pelaaja pudottaa toisen avainkortin
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

    public void Switch()
    {
        if (icons[0].activeInHierarchy == true)
        {
            EquipWeapon();
        }

        if (icons[1].activeInHierarchy == true)
        {
            EquipGrenade();
        }

        if (icons[2].activeInHierarchy == true)
        {
            EquipKeyCard();
        }

        if (icons[3].activeInHierarchy == true)
        {
            EquipKeyCard2();
        }
    }

    public void Pickup()
    {
        //Kun et kanna esinettä
        if (carrying == false)
        {
            //Toimintonäppäintäpainamalla aseen lähistöllä tulee ase pelaajan käteen
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

            //Toimintonäppäintäpainamalla kranaatin lähistöllä tulee kranaatti pelaajan käteen
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

            //Toimintonäppäintäpainamalla avainkortin lähistöllä tulee avainkortti pelaajan käteen
            if (items[2] != null && keyCardCarrying == false)
            {
                if ((guide.transform.position - items[2].transform.position).sqrMagnitude < range * range)
                {
                    PickKeyCard();
                    playerAnimation.SetTrigger("Interact");
                    removeButtons[6].SetActive(true);
                }
            }

            //Toimintonäppäintäpainamalla toisen avainkortin lähistöllä tulee toinen avainkortti pelaajan käteen
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

        //Kun kannat esinettä
        if (carrying == true)
        {
            //Jos kannat asetta ja keräät toisen esineen, toinen esine menee inventaarioon
            if (weaponCarrying == true && items[0] != null)
            {
                //Asetta kantaessa jos valitset kranaatin, kranaatti menee inventaarioon
                if (items[1] != null)
                {
                    if ((guide.transform.position - items[1].transform.position).sqrMagnitude < range * range)
                    {
                        backpackIcons[1].SetActive(true);
                        items[1].SetActive(false);
                        removeButtons[2].SetActive(true);
                    }
                }

                //Asetta kantaessa jos valitset avainkortin, avainkortti menee inventaarioon
                if (items[2] != null)
                {
                    if ((guide.transform.position - items[2].transform.position).sqrMagnitude < range * range)
                    {
                        backpackIcons[2].SetActive(true);
                        items[2].SetActive(false);
                        removeButtons[3].SetActive(true);
                    }
                }

                //Asetta kantaessa jos valitset toisen avainkortin, toinen avainkortti menee inventaarioon
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

            //Jos kannat kranaattia ja keräät toisen esineen, toinen esine menee inventaarioon
            if (grenadeCarrying == true && items[1] != null)
            {
                //Kranaattia kantaessa jos valitset aseen, ase menee inventaarioon
                if (items[0] != null)
                {
                    if ((guide.transform.position - items[0].transform.position).sqrMagnitude < range * range)
                    {
                        backpackIcons[0].SetActive(true);
                        items[0].SetActive(false);
                        removeButtons[1].SetActive(true);
                    }
                }

                //Kranaattia kantaessa jos valitset avainkortin, avainkortti menee inventaarioon
                if (items[2] != null)
                {
                    if ((guide.transform.position - items[2].transform.position).sqrMagnitude < range * range)
                    {
                        backpackIcons[2].SetActive(true);
                        items[2].SetActive(false);
                        removeButtons[3].SetActive(true);
                    }
                }

                //Kranaattia kantaessa jos valitset toisen avainkortin, toinen avainkortti menee inventaarioon
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

            //Jos kannat avainkorttia ja keräät toisen esineen, toinen esine menee inventaarioon
            if (keyCardCarrying == true && items[2] != null)
            {
                //Avainkorttia kantaessa jos valitset aseen, ase menee inventaarioon
                if (items[0] != null)
                {
                    if ((guide.transform.position - items[0].transform.position).sqrMagnitude < range * range)
                    {
                        backpackIcons[0].SetActive(true);
                        items[0].SetActive(false);
                        removeButtons[1].SetActive(true);
                    }
                }

                //Avainkorttia kantaessa jos valitset kranaatin, kranaatti menee inventaarioon
                if (items[1] != null)
                {
                    if ((guide.transform.position - items[1].transform.position).sqrMagnitude < range * range)
                    {
                        backpackIcons[1].SetActive(true);
                        items[1].SetActive(false);
                        removeButtons[2].SetActive(true);
                    }
                }

                //Avainkorttia kantaessa jos valitset toisen avainkortin, toinen avainkortti menee inventaarioon
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

            //Jos kannat toista avainkorttia ja keräät toisen esineen, toinen esine menee inventaarioon
            if (keyCard2Carrying == true && items[3] != null)
            {
                //Toista avainkorttia kantaessa jos valitset aseen, ase menee inventaarioon
                if (items[0] != null)
                {
                    if ((guide.transform.position - items[0].transform.position).sqrMagnitude < range * range)
                    {
                        backpackIcons[0].SetActive(true);
                        items[0].SetActive(false);
                        removeButtons[1].SetActive(true);
                    }
                }

                //Toista avainkorttia kantaessa jos valitset kranaatin, kranaatti menee inventaarioon
                if (items[1] != null)
                {
                    if ((guide.transform.position - items[1].transform.position).sqrMagnitude < range * range)
                    {
                        backpackIcons[1].SetActive(true);
                        items[1].SetActive(false);
                        removeButtons[2].SetActive(true);
                    }
                }

                //Toista avainkorttia kantaessa jos valitset avainkortin, avainkortti menee inventaarioon
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

            //Jos kranaatti on tuhoutunut, et kanna esinettä
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

            //Jos avainkortti on tuhoutunut, et kanna esinettä
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

            //Jos toinen avainkortti on tuhoutunut, et kanna esinettä
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