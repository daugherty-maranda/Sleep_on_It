using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool sprint;
    public bool jump;
    public bool crouch;
    public bool fire;

    public float horizontal;
    public float vertical;

    public float mouseX;
    public float mouseY;

    public void Update()
    {
        // Keys
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        jump = Input.GetButtonDown("Jump");
        fire = Input.GetButtonDown("Fire");
        sprint = Input.GetButton("Sprint");
        crouch = Input.GetButton("Crouch");

        // Mouse
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");
    }
}
