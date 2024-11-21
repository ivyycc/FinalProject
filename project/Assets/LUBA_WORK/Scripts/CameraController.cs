using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSensitivityController : MonoBehaviour
{
    public Transform playerBody; // Reference to the player's body for horizontal rotation
    public Transform playerCamera; // Reference to the player's camera for vertical rotation

    public float mouseSensitivity = 100f; // Base sensitivity
    private float xRotation = 0f; // To keep track of the camera's vertical rotation

    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMouseLook();
    }

    void HandleMouseLook()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate player body horizontally
        playerBody.Rotate(Vector3.up * mouseX);

        // Rotate camera vertically
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent camera from over-rotating
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    // Optional: Add a method to adjust sensitivity dynamically
    public void SetSensitivity(float newSensitivity)
    {
        mouseSensitivity = newSensitivity;
    }
}
