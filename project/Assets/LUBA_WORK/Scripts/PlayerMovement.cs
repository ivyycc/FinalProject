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
    public bool useGravity = true; // Flag to control gravity

    // Web Shooting
    public float webRange = 20f; // Maximum range for web shot
    public float pullSpeed = 15f; // Speed at which player is pulled to the wall
    public string climbableTag = "Climbable";  // Layer for climbable walls
    public LineRenderer webLine; // Optional visual web effect

    private bool isWebShooting = false; // Tracks if the player is currently being pulled
    private Vector3 webTarget; // The point where the web attached on the wall
    private bool isHanging = false; // Tracks if the player is "hanging" on the wall

    void Start()
    {
        controller = GetComponent<CharacterController>();

        webLine = GetComponent<LineRenderer>();
        if (webLine != null)
        {
            webLine.positionCount = 0;
        }
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (Input.GetMouseButtonDown(1)) // Right-click to shoot web
        {
            ShootWeb();
        }

        // Continue pulling or hanging as long as the right mouse button is held down
        if (Input.GetMouseButton(1))
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

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("B Button pressed");
        }
        else
        {
            Debug.Log("Bomb is not ready/bomb is in cool down");
        }
    }

    // Input method for movement
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

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
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    void ShootWeb()
    {
        // Cast a ray from the player's camera forward
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        // Check if the ray hits any object within the webRange
        if (Physics.Raycast(ray, out hit, webRange))
        {
            // Check if the object hit has the "Climbable" tag
            if (hit.collider.CompareTag(climbableTag))
            {
                Debug.Log("Hit a climbable object!");

                // Store the hit point as the target position for web shooting
                webTarget = hit.point;
                isWebShooting = true;

                // Set up the LineRenderer to visualize the web
                webLine.positionCount = 2;
                webLine.SetPosition(0, transform.position); // Start of the web (player's position)
                webLine.SetPosition(1, webTarget);          // End of the web (wall point)

                // Disable gravity while pulling
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

    // Method to pull the player towards the web target
    void PullPlayerToTarget()
    {
        // Calculate direction from player to web target
        Vector3 directionToTarget = (webTarget - transform.position).normalized;

        // Move the player towards the web target
        controller.Move(directionToTarget * pullSpeed * Time.deltaTime);

        // Update the start position of the LineRenderer to follow the player's position
        webLine.SetPosition(0, transform.position);

        // Check if the player is close enough to the target
        if (Vector3.Distance(transform.position, webTarget) < 1f)
        {
            // If the player is close enough to the wall, allow them to hang
            isWebShooting = false;
            isHanging = true;

            // Stop moving the player, but keep the web line active for hanging
        }
    }

    // Method to "stick" to the wall
    void StickToWall()
    {
        // While hanging, the player's movement is restricted, but the web stays attached
        // This is where you can add logic to allow for wall-climbing or other interactions.
        // For now, we just maintain the LineRenderer and prevent the player from falling.

        // Update the start position of the LineRenderer to follow the player's position
        webLine.SetPosition(0, transform.position);

        // Prevent the player from falling (disable gravity while hanging)
        useGravity = false;
        playerVelocity.y = 0;
    }

    // Method to stop web shooting or hanging
    void StopWeb()
    {
        isWebShooting = false;
        isHanging = false;

        // Hide the LineRenderer
        webLine.positionCount = 0;

        // Re-enable gravity when the player stops hanging or shooting the web
        useGravity = true;
    }
}
