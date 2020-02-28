using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
    private float defaultHeight = 2f;
    private Vector3 moveDir;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<InputManager>();
        hitBox = GetComponentInChildren<CapsuleCollider>();
    }

    void FixedUpdate()
    {
        // Check grounded
        if((moveFlags & CollisionFlags.Below) != 0)
        {
            var rightVector = input.horizontal * transform.right;
            var forwardVector = input.vertical * transform.forward;

            // If player is grounded, allow movement
            moveDir = (transform.right * input.horizontal) + (transform.forward * input.vertical);
            moveDir *= GetSpeed();

            // Jumping
            if(input.jump)
            {
                moveDir.y = jumpForce;
            }

            // Crouching
            if(input.crouch)
            {
                controller.height = crouchHeight;
            }
            else
            {
                controller.height = defaultHeight;
            }
        }

        // Fire
        if(input.fire)
        {
            hitBox.enabled = true;
        }
        else
        {
            hitBox.enabled = false;
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
