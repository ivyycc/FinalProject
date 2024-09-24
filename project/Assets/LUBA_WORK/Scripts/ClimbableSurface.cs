using UnityEngine;

public class ClimbableSurface : MonoBehaviour
{
    public GameObject cylinderPrefab;  // Drag and drop your cylinder prefab here
    public float raycastDistance = 10f; // The maximum distance for the raycast

    void Update()
    {
        // Detect right-click (Mouse button 1 is for right-click)
        if (Input.GetMouseButtonDown(1))
        {
            DeployCylinder();
        }
    }

    void DeployCylinder()
    {
        // Cast a ray from the center of the screen (camera forward direction)
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            // Check if the object hit by the ray has the tag "ClimbableSurface"
            if (hit.collider.CompareTag("ClimbableSurface"))
            {
                // Get the position and normal of the surface hit by the ray
                Vector3 hitPoint = hit.point;
                Vector3 surfaceNormal = hit.normal;

                // Instantiate the cylinder at the hit point, with the orientation based on the surface normal
                GameObject cylinder = Instantiate(cylinderPrefab, hitPoint, Quaternion.LookRotation(surfaceNormal));

                // Adjust the cylinder position so it sticks properly to the surface (slightly offset from the hit point)
                cylinder.transform.position += surfaceNormal * 0.5f;  // Move it slightly outward to avoid embedding in the surface
            }
        }
    }
}
