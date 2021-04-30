using UnityEngine;

//This script will give the actions to weapon
public class Weapon: MonoBehaviour
{
    public GameObject spawnPoint;
    public Rigidbody projectilePrefab;
    public float speed;
    public int damage;

    private void Update()
    {
        //When pressing button, the pistol will fire projectiles
        if (Input.GetButtonDown("Fire2"))
        {
            //These will give the direction where to shoot, which is in front of the pistol
            Rigidbody hitInfo;
            hitInfo = Instantiate(projectilePrefab, transform.position, transform.rotation) as Rigidbody;
            hitInfo.velocity = transform.TransformDirection(Vector3.up * 100);
        }

        //This will create more projectiles to shoot, as a loop
        for (var i = 0; i < Input.touchCount; ++i)
        {
            //When shooting multiple projectiles, this creates mote clone projectiles
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                Rigidbody clone;

                //This will instantiate the clone projectiles and the given directions to shoot at the certain direction and the speed
                clone = Instantiate(projectilePrefab, transform.position, transform.rotation) as Rigidbody;
                clone.velocity = transform.TransformDirection(Vector3.up * 200);
            }
        }
    }
}