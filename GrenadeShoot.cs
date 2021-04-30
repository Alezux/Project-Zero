using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tämä koodi antaa kranaatin heitolle ominaisuudet
public class GrenadeShoot : MonoBehaviour
{
    private Camera cam;
    public GameObject ammoGrenadeDisplay;
    public GameObject cursor;
    public GameObject grenade;
    public GameObject removeButton;
    public LayerMask layer;
    public LineRenderer lineVisual;
    public Rigidbody grenadePrefab;
    public Transform shootPoint;
    public float fireRate = 0.5F;
    private float nextFire = 0.0F;
    public int currentAmmo;
    public int lineSegment = 10;
    public int maxAmmo;
    public Animator playerAnim;
    public PickableItems PickableItems;

    //Alkaessa koodi antaa seuraavat arvot ja hakee komponentin
    void Start()
    {
        cam = Camera.main;
        lineVisual.positionCount = lineSegment;
        lineVisual = grenade.GetComponentInChildren<LineRenderer>();
        currentAmmo = maxAmmo;
    }

    //Päivittäessä tämä hakee toista funktiota
    void Update()
    {
        LaunchProjectile();
    }

    //Kranaatinheiton aikana kaari ja kohde seuraavat hiirtä, mihin voit heittää kranaatin
    void LaunchProjectile()
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit, 100f, layer))
        {
            //cursor.SetActive(true);
            //cursor.transform.position = hit.point + Vector3.up * 0.1f;
            Vector3 vo = CalculateVelocty(hit.point, shootPoint.position, 1f);
            Visualize(vo);
            transform.rotation = Quaternion.LookRotation(vo);

            //Näppäintä painaessa heittää kranaatin
            if (Input.GetButtonDown("Shoot") && Time.time > nextFire)
            {
                playerAnim.SetTrigger("ThrowGrenade");
                Rigidbody grenade;
                grenade = Instantiate(grenadePrefab, shootPoint.position, Quaternion.identity) as Rigidbody;
                grenade.velocity = vo;
                nextFire = Time.time + fireRate;
                currentAmmo--;
            }

            //Jos kranaatin ammukset loppuu, kranaatti tuhoutuu
            if (currentAmmo <= 0)
            {
                Destroy(gameObject);
                Destroy(ammoGrenadeDisplay);
                removeButton.SetActive(false);
                PickableItems.icons[1].SetActive(false);
            }
        }
    }

    //Tekee kranaatinheiton viivasta kaaren
    void Visualize(Vector3 vo)
    {
        for (int i = 0; i < lineSegment; i++)
        {
            Vector3 pos = CalculatePosInTime(vo, i / (float)lineSegment);
            lineVisual.SetPosition(i, pos);
        }
    }

    //Antaa kranaatinheiton kaarelle toiminnot ja laskee etäisyydet
    Vector3 CalculateVelocty(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXz = distance;
        distanceXz.y = 0f;
        float sY = distance.y;
        float sXz = distanceXz.magnitude;
        float Vxz = sXz * time;
        float Vy = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);
        Vector3 result = distanceXz.normalized;
        result *= Vxz;
        result.y = Vy;
        return result;
    }

    //Antaa kranaatinheitolle toiminnot ja laskee etäisyydet
    Vector3 CalculatePosInTime(Vector3 vo, float time)
    {
        Vector3 Vxz = vo;
        Vxz.y = 0f;
        Vector3 result = shootPoint.position + vo * time;
        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + shootPoint.position.y;
        result.y = sY;
        return result;
    }
}
