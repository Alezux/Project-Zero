using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will make the grenade throwable
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

    void Start()
    {
        cam = Camera.main;
        lineVisual.positionCount = lineSegment;
        lineVisual = grenade.GetComponentInChildren<LineRenderer>();
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        LaunchProjectile();
    }

    //When player is carrying the grenade, the features of grenade will follow the mouse location where you can aim the grenade for
    void LaunchProjectile()
    {
        //Raycast features
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit, 100f, layer))
        {
            Vector3 vo = CalculateVelocty(hit.point, shootPoint.position, 1f);
            Visualize(vo);
            transform.rotation = Quaternion.LookRotation(vo);

            //When pressing the button, player will throw grenade
            if (Input.GetButtonDown("Shoot") && Time.time > nextFire)
            {
                //Animation
                playerAnim.SetTrigger("ThrowGrenade");

                //Gravity
                Rigidbody grenade;

                //Instantianting the features and giving velocity
                grenade = Instantiate(grenadePrefab, shootPoint.position, Quaternion.identity) as Rigidbody;
                grenade.velocity = vo;

                //This will count when the next grenade can be thrown
                nextFire = Time.time + fireRate;

                //Grenade loses ammos, aka grenades
                currentAmmo--;
            }

            //When grenade runs out of ammos, which in this case are multiple grenades, the grenades will disappear after time
            if (currentAmmo <= 0)
            {
                Destroy(gameObject);
                Destroy(ammoGrenadeDisplay);
                removeButton.SetActive(false);
                PickableItems.icons[1].SetActive(false);
            }
        }
    }

    //This will give the bow that appears when player carries grenade
    void Visualize(Vector3 vo)
    {
        for (int i = 0; i < lineSegment; i++)
        {
            Vector3 pos = CalculatePosInTime(vo, i / (float)lineSegment);
            lineVisual.SetPosition(i, pos);
        }
    }

    //This will give the features for the bow when carrying grenade
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
