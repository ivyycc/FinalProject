using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;

    public float xSensitivity = 10f;
    public float ySensitivity = 10f;

    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    void Update()
    {
        // You can toggle the cursor state (for example, to show the cursor in the pause menu)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            
        }
    }
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        //cam rotation for up &down
        xRotation -= (mouseY * Time.fixedDeltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        // to camtransform
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        // make player look left 7/ right
        transform.Rotate(Vector3.up * (mouseX * Time.fixedDeltaTime) * xSensitivity);
    }

    public void UpdateSensitivity(float sensitivity)
    {

    }
}
