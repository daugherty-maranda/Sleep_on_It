using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool sprint;
    public bool jump;

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
        sprint = Input.GetButton("Sprint");

        // Mouse
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");
    }
}
