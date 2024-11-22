using UnityEngine;

public class AdvancedMouseSensitivityController : MonoBehaviour
{
    [Header("Mouse Sensitivity")]
    public float mouseSensitivityX = 100f; // Horizontal sensitivity
    public float mouseSensitivityY = 100f; // Vertical sensitivity

    [Header("Vertical Angle Limits")]
    public float minVerticalAngle = -90f; // Minimum vertical angle (looking down)
    public float maxVerticalAngle = 90f;  // Maximum vertical angle (looking up)

    [Header("Player Body Reference")]
    public Transform playerBody;         // Reference to the player's body for horizontal rotation

    private float xRotation = 0f;        // Track camera's vertical rotation

    void Start()
    {
        // Lock the cursor to the center of the screen and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse input and scale it by sensitivity and delta time
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        // Adjust vertical rotation (camera pitch) and clamp to min/max angles
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle);

        // Apply rotation to the camera (local) and player body (global)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    // Methods to dynamically change sensitivity and angles
    public void SetSensitivityX(float sensitivityX)
    {
        mouseSensitivityX = sensitivityX;
    }

    public void SetSensitivityY(float sensitivityY)
    {
        mouseSensitivityY = sensitivityY;
    }

    public void SetVerticalAngleLimits(float minAngle, float maxAngle)
    {
        minVerticalAngle = minAngle;
        maxVerticalAngle = maxAngle;
    }
}
