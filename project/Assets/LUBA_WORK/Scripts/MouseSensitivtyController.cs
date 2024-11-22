using UnityEngine;

public class MouseSensitivtyController : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Default sensitivity
    public Transform playerBody;         // Reference to the player's body for rotation
    private float xRotation = 0f;        // Track camera's vertical rotation

    void Start()
    {
        // Lock the cursor to the center of the screen and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse input and scale it by sensitivity and delta time
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the camera vertically (inverted by default for natural feel)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limit vertical rotation

        // Apply rotation to the camera and player body
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    // Method to dynamically change sensitivity
    public void SetSensitivity(float newSensitivity)
    {
        mouseSensitivity = newSensitivity;
    }
}
