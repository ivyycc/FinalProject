using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // Movement
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 5f;
    private bool isGrounded;
    public float gravity = -9.8f;
    public float jumpHeight = 3;
    public bool useGravity = true;

    // Web Shooting
    public float webRange = 20f; // Maximum range for pull
    public float pullSpeed = 15f; // Speed at which player is pulled to the wall
    public string climbableTag = "Climbable";
    public LineRenderer webLine;
    private bool isWebShooting = false; // Tracks if web is being shot
    private Vector3 webTarget; // The point where the web touched the wall
    private bool isHanging = false;
    public Material webColor;
    public float maxHangDistance = 9f; // Maximum distance allowed while hanging
    public float handDistance = 1.5f;

    // Player Stats
    public int playerHealth;
    public int playerStamina;
    public int playerSwingTime;
    public int playerEnergy;

    //HangStats
    public float maxHangTime = 5f; // Maximum time the player can hang (in seconds)
    private float currentHangTime = 0f; // Timer to track hangTime

    // Camera Zoom
    public Camera playerCamera;
    public float normalFOV = 60f;  // Default field of view
    public float zoomFOV = 30f;    // Zoomed-in field of view
    public float zoomSpeed = 5f;   // Speed of zoom transition
    public bool isZoomed = false; // Tracks whether zoom is active

    void Start()
    {
        controller = GetComponent<CharacterController>();

        webLine = GetComponent<LineRenderer>();
        if (webLine != null)
        {
            webLine.positionCount = 0;
        }

        // Initialize the camera's field of view to normal
        if (playerCamera != null)
        {
            playerCamera.fieldOfView = normalFOV;
        }
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (Input.GetMouseButtonDown(1)) // Right-click to shoot web
        {
            webColor.SetColor("_Color", Color.blue);
            ShootWeb();
        }
        else if (Input.GetMouseButtonDown(0)) // Left-click to shoot web
        {
            webColor.SetColor("_Color", Color.red);
            ShootWeb();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isHanging)
        {
            Debug.Log("Space Btn Clicked");
            // Jump();
        }

        // Continue pulling or hanging as long as the right mouse button is held down
        if (Input.GetMouseButton(1) || Input.GetMouseButton(0))
        {
            if (isWebShooting)
            {
                PullPlayerToTarget();
            }
            else if (isHanging)
            {
                StickToWall();
            }
        }
        else
        {
            // If the button is released, stop web shooting or hanging
            StopWeb();
        }

        // Handle zoom input
        HandleZoom();

       
    }

    // Input method for movement
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        if (isHanging)
        {
            // Logic for movement while hanging
        }

        // Apply gravity only if useGravity is true
        if (useGravity)
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f; // Reset vertical velocity when grounded
        }

        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            Debug.Log("Jump while grounded is called");
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
        else if (isHanging)
        {
            Debug.Log("Jump while hanging is called");
            isHanging = false;
            useGravity = true; // Re-enable gravity

            // Apply upward jump velocity
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);

            // Hide the web line
            webLine.positionCount = 0;
        }
    }

    void ShootWeb()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, webRange))
        {
            if (hit.collider.CompareTag(climbableTag))
            {
                Debug.Log("Hit a climbable object!");
                webTarget = hit.point;
                isWebShooting = true;

                webLine.positionCount = 2;
                webLine.SetPosition(0, transform.position);
                webLine.SetPosition(1, webTarget);

                useGravity = false;
            }
            else
            {
                Debug.Log("Missed! Object is not climbable.");
            }
        }
        else
        {
            Debug.Log("Missed! No object hit within range.");
        }
    }

    void PullPlayerToTarget()
    {
        Vector3 directionToTarget = (webTarget - transform.position).normalized;
        controller.Move(directionToTarget * pullSpeed * Time.deltaTime);
        webLine.SetPosition(0, transform.position);

        if (Vector3.Distance(transform.position, webTarget) < handDistance)
        {
            isWebShooting = false;
            isHanging = true;
        }
    }

    void StickToWall()
    {
        webLine.SetPosition(0, transform.position);
        useGravity = false;
        playerVelocity.y = 0;

        // Limit the player's movement if they try to move too far from the web anchor
        float currentDistance = Vector3.Distance(transform.position, webTarget);
        if (currentDistance > maxHangDistance)
        {
            // Move the player back to the max distance from the web target
            Vector3 directionToTarget = (transform.position - webTarget).normalized;
            transform.position = webTarget + directionToTarget * maxHangDistance;
        }

        currentHangTime += Time.deltaTime;
        if (currentHangTime >= maxHangTime)
        {
            Debug.Log("Max hang time exceeded, falling!");
            StopWeb(); // Call StopWeb to make the player fall
        }

        // Add movement while hanging logic here
        if (Input.GetKey(KeyCode.W))
        {
            // Movement while sticking to the wall (optional)
        }
        if (Input.GetKey(KeyCode.S))
        {
            // Movement while sticking to the wall (optional)
        }
    }

    void StopWeb()
    {
        isWebShooting = false;
        isHanging = false;

        webLine.positionCount = 0;
        useGravity = true;

        currentHangTime = 0f;
    }

    // Handle zoom logic
    void HandleZoom()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isZoomed = !isZoomed;
        }

        float targetFOV = isZoomed ? zoomFOV : normalFOV;
        if (playerCamera != null)
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
        }
    }
}
