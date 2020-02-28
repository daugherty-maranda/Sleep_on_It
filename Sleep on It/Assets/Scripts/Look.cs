using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    [SerializeField] private float mouseSens = 0.5f;

    private float mouseX;
    private float mouseY;

    private InputManager input;

    void OnEnable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        input = GetComponentInParent<InputManager>();
    }

    void OnDisable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void LateUpdate()
    {
        mouseX += input.mouseX * mouseSens;
        //50
        mouseY = Mathf.Min(90, Mathf.Max(-90, mouseY + (input.mouseY * mouseSens)));

        Quaternion cameraRotation = Quaternion.Euler(-mouseY, 0f, 0f);
        Quaternion playerRotation = Quaternion.Euler(0f, mouseX, 0f);

        transform.localRotation = cameraRotation;
        transform.parent.localRotation = playerRotation;
    }
}
