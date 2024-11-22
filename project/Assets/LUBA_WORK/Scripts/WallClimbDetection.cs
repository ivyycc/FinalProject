using UnityEngine;
using UnityEngine.UI;

public class WallClimbDetection : MonoBehaviour
{
    public float maxClimbDistance = 3.8f; // Maximum distance to detect a climbable wall
    private PlayerMovement playerMovement;
    public Transform cameraTransform;   // Reference to the player's camera
    public LayerMask climbableLayer;    // Define the layer for climbable walls (optional if you want to use Layers instead of Tags)
    public Image aimingDot;             // The aiming dot UI element
    public RectTransform crossHairSpinningPart;
    public float crossHairSpinSpeed = 200.0f;
    private RaycastHit hitInfo;

        private void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }

    void Update()
    {
        CheckForClimbableWall();
    }

    void CheckForClimbableWall()
    {
        // Create a ray from the camera to where the player is looking
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        // Check if the ray hits something within the climbable distance
        if (Physics.Raycast(ray, out hitInfo, maxClimbDistance))
        {
            // Check if the object hit is tagged as "Climbable"
            if (hitInfo.collider.CompareTag("Climbable") || hitInfo.collider.CompareTag("ShakyRock")) //Added Another tag here for red dot
            {
              
                // Show aiming dot if we hit a climbable wall within the specified distance
                if(playerMovement.isHanging)
                {
                    aimingDot.gameObject.SetActive(false);
                }
                else
                {
                    aimingDot.gameObject.SetActive(true);
                }
               // aimingDot.gameObject.SetActive(true);
            }
            else
            {
                // If not looking at a climbable wall, hide the aiming dot
                aimingDot.gameObject.SetActive(false);
               
               
            }
        }
        else
        {
            // If the ray doesn't hit anything, hide the aiming dot
            aimingDot.gameObject.SetActive(false);
        }
    }
}
