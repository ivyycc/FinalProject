using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Sensitivity multiplier
    public Transform playerBody; // Reference to the player's body for horizontal rotation

    private float xRotation = 0f; // Track the vertical rotation of the camera

    void Start()
    {
        
    }

    void Update()
    {
        // Get mouse input and scale it with sensitivity and frame time
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Adjust the vertical rotation and clamp it
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply rotations
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    
    }
}
