using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This script will give player movement, rotation and crouch
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

    void Start()
    {
        playerCollider = GetComponent<CapsuleCollider>();
        originalHeight = playerCollider.height;
        anim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        InvokeRepeating("PlayStepSound", 0f, 0.5f);
        isMoving = false;
    }

    void FixedUpdate()
    {
        CharacterController controller = GetComponent<CharacterController>();
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;

        //When holding ctrl button, player will crouch
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Crouch();
            isCrouching = true;
        }

        //When letting off ctrl button, player will stand again
        else
        {
            GoUp();
            isCrouching = false;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void Update()
    {
        //Player will move, get gravity and rotation towards mouse
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

        //All codes below are working within the direction where player is moving and where the player is pointing at.
        //For example when player is moving backwards and is directing to straight. These actions applies to crouching as well.
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

        //End of lines
    }

    //When calling this function, player will crouch
    void Crouch()
    {
        playerCollider.height = reducedHeight;
        anim.SetBool("isCrouching", true);
    }

    //When calling this function, player will stand
    void GoUp()
    {
        playerCollider.height = originalHeight;
        anim.SetBool("isCrouching", false);
        speed = 20f;
    }

    //When calling this function, player will get movement
    public void Move()
    {
        movementDirection = Vector3.zero;
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");
        movementDirection.x += horizontalMove;
        movementDirection.z += verticalMove;

        //When player is not idle, player will get movement
        if (verticalMove != 0 || horizontalMove != 0)
        {
            isMoving = true;
        }

        //When player is idle, player wont move
        else
        {
            isMoving = false;
        }

        //When player is crouching and moving, the player will move while crouching
        if (isCrouching && isMoving)
        {
            anim.SetBool("isCrouching", true);
        }
    }

    //When calling this function, player will get step sounds when moving
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
