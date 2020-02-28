using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator ScissorsAnim;
    private AudioSource snip;
    private CharacterController controller;
    private InputManager input;
    private CapsuleCollider hitBox;
    private CollisionFlags moveFlags;

    // Movement Variables
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float gravity = 10f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float sprintMult = 2f;
    [SerializeField] private float crouchMult = 0.5f;
    [SerializeField] private float crouchHeight = 1f;
    [SerializeField] private float attackCd = 0.25f;
    [SerializeField] private bool grounded = true;
    private float defaultHeight = 2f;
    private float attackTimer = 0.0f;
    private bool firing = false;
    private Vector3 moveDir;

    //Crouching
    private bool is_crouching = false;

    //Open Door
    GameObject door;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<InputManager>();
        hitBox = GetComponentInChildren<CapsuleCollider>();
        snip = GetComponent<AudioSource>();
        ScissorsAnim = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        // Check grounded
        if((moveFlags & CollisionFlags.Below) != 0)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        // Allows movement if grounded
        if (grounded == true)
        {
            var rightVector = input.horizontal * transform.right;
            var forwardVector = input.vertical * transform.forward;
            
            moveDir = (transform.right * input.horizontal) + (transform.forward * input.vertical);
            moveDir *= GetSpeed();

            // Jumping
            if (input.jump)
            {
                moveDir.y = jumpForce;
            }

            // Crouching
            if (input.crouch)
            {
                is_crouching = true;
                controller.height = crouchHeight;
            }
            else
            {
                is_crouching = false;
                controller.height = defaultHeight;
            }

        } else if (grounded == false)
        {
            // Do Some Air Movement
        }

        //This is for the door
        door = GameObject.FindWithTag("Door");

        // Fire
        if (input.fire)
        {
            if(!firing)
            {
                snip.Play();
                ScissorsAnim.SetTrigger("Snipping");
                firing = true;
                attackTimer = attackCd;
                hitBox.enabled = true;

                //This is for doors
                door.SendMessage("openDoor", SendMessageOptions.DontRequireReceiver);

            }
        }

        if(firing)
        {
            if(attackTimer > 0.0f) {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                hitBox.enabled = false;
                firing = false;
            }
        }

        // Gravity
        moveDir.y -= gravity * Time.deltaTime;

        // Move Controller
        moveFlags = controller.Move(moveDir * Time.fixedDeltaTime);
    }

    // Gets current speed
    private float GetSpeed()
    {
        var speed = moveSpeed;
   
        // If crouch, slow down. Else check if sprinting.
        if(input.crouch)
        {
            speed *= crouchMult;
        }
        else
        {
            speed *= input.sprint ? sprintMult : 1f;
        }

        return speed;
    }
}
