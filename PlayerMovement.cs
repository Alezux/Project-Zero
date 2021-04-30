using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Tämä koodi antaa pelaajalle liikkumisen, pyörimisen ja kyykyn
public class PlayerMovement : MonoBehaviour
{
    CapsuleCollider playerCollider;
    float originalHeight;
    float reducedHeight = 2f;
    private Vector3 moveDirection = Vector3.zero;
    public float speed = 20f;
    public float gravity = 1000f;
    private Animator anim;
    private Rigidbody playerRb;
    public AudioSource randomSound;
    public AudioClip[] stepSounds;
    public bool isMoving;
    public bool isCrouching;

    public Vector3 worldPosition;
    public Vector3 movementDirection;


    //Alkaessa koodi hakee komponentteja
    void Start()
    {
        playerCollider = GetComponent<CapsuleCollider>();
        originalHeight = playerCollider.height;
        anim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        InvokeRepeating("PlayStepSound", 0f, 0.5f);
        isMoving = false;
    }

    //Päivittäessä pelaajaa voi liikua
    void FixedUpdate()
    {
        CharacterController controller = GetComponent<CharacterController>();
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;

        //Toimintonäppäintä painamalla pohjassa pelaaja menee kyykkyyn
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Crouch();
            isCrouching = true;
        }

        //Toimintonäppäimen päästäminen kyykystä palauttaa pelaajan seisomaan
        else
        {
            GoUp();
            isCrouching = false;
        }

        //Liikkumiseen liittyviä toimintoja
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    //Päivittäessä koodi hakee funktion
    private void Update()
    { 
        Move();
        Plane plane = new Plane(Vector3.up, 0);
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);
        }

        Vector3 vectorTomousepoint = worldPosition - transform.position;
        float angle = Vector3.SignedAngle(vectorTomousepoint, movementDirection, Vector3.up);

        if (angle >= -70f && angle <= 70f && isMoving)
        {
            anim.SetBool("isRunningForward", true);
            anim.SetBool("isRunningBackward", false);
            anim.SetBool("isStrafingLeft", false);
            anim.SetBool("isStrafingRight", false);
        }

        if (!isMoving)
        {
            anim.SetBool("isRunningForward", false);
            anim.SetBool("isRunningBackward", false);
            anim.SetBool("isStrafingLeft", false);
            anim.SetBool("isStrafingRight", false);
            anim.SetBool("isCrouchWalking", false);
        }

        if (angle >= -180f && angle <= -110f && isMoving)
        {
            anim.SetBool("isRunningBackward", true);
            anim.SetBool("isStrafingLeft", false);
            anim.SetBool("isStrafingRight", false);
            anim.SetBool("isRunningForward", false);
        }
        if (angle >= 110f && angle <= 180f && isMoving)
        {
            anim.SetBool("isRunningBackward", true);
            anim.SetBool("isStrafingLeft", false);
            anim.SetBool("isStrafingRight", false);
            anim.SetBool("isRunningForward", false);
        }

        if (angle >= 70f && angle <= 110f && isMoving)
        {
            anim.SetBool("isStrafingLeft", true);
            anim.SetBool("isRunningForward", false);
            anim.SetBool("isRunningBackward", false);
            anim.SetBool("isStrafingRight", false);
        }

        if (angle >= -110f && angle <= -70f && isMoving)
        {
            anim.SetBool("isStrafingRight", true);
            anim.SetBool("isStrafingLeft", false);
            anim.SetBool("isRunningForward", false);
            anim.SetBool("isRunningBackward", false);
        }

        if (isCrouching && isMoving)
        {
            anim.SetBool("isCrouchWalking", true);
            speed = 10f;
            anim.SetBool("isStrafingLeft", false);
            anim.SetBool("isStrafingRight", false);
            anim.SetBool("isRunningForward", false);
            anim.SetBool("isRunningBackward", false);
        }

        transform.rotation = Quaternion.identity;
    }

    //Tätä funktiota kutsumalla saa pelaajan kyykkyyn, laskee pelaajan pituuden
    void Crouch()
    {
        playerCollider.height = reducedHeight;
        anim.SetBool("isCrouching", true);

    }

    //Tätä funktiota kutsumalla saa pelaajan pituuden ennalleen
    void GoUp()
    {
        playerCollider.height = originalHeight;
        anim.SetBool("isCrouching", false);
        speed = 20f;
    }

    //Tätä funktiota kutsumalla saa pelaajan liikkumisen ja animaation toimimaan
    public void Move()
    {
        movementDirection = Vector3.zero;
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");
        movementDirection.x += horizontalMove;
        movementDirection.z += verticalMove;

        if (verticalMove != 0 || horizontalMove != 0)
        {
            isMoving = true;
        }

        else
        {
            isMoving = false;
        }

        if (isCrouching && isMoving)
        {
            anim.SetBool("isCrouching", true);
        }
    }

    void PlayStepSound()
    {
        if (isMoving)
        {
            randomSound.clip = stepSounds[Random.Range(0, stepSounds.Length)];
            randomSound.Play();
        }

        if (!isMoving)
        {
            randomSound.Stop();
        }
    }
}
