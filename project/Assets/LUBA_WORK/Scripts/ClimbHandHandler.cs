using UnityEngine;

public class ClimbHandHandler : MonoBehaviour
{
    public LineRenderer lineRenderer; 
    public GameObject handPrefab; // The hand object prefab to instantiate
    private GameObject instantiatedHand; // Reference to the instantiated hand object

    void Update()
    {
        if (lineRenderer != null && handPrefab != null)
        {
            // Get the last position of the LineRenderer
            int lastIndex = lineRenderer.positionCount - 1;
            Vector3 endPosition = lineRenderer.GetPosition(lastIndex);

            // Instantiate the hand if it doesn't already exist
            if (instantiatedHand == null)
            {
                instantiatedHand = Instantiate(handPrefab, endPosition, Quaternion.identity);
            }

            // Update the position of the instantiated hand
            instantiatedHand.transform.position = endPosition;

            // Optional: Adjust the hand's rotation to face the direction of the LineRenderer
            if (lineRenderer.positionCount > 1) // Ensure we have at least two points
            {
                Vector3 startPosition = lineRenderer.GetPosition(lastIndex - 1);
                Vector3 direction = (endPosition - startPosition).normalized;
                instantiatedHand.transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}
