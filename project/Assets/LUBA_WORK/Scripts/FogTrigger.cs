using UnityEngine;
using System.Collections;

public class FogTrigger : MonoBehaviour
{
    // Assign the GameObject you want to activate in the inspector
    public GameObject fogEffect;

    // Time the fog will stay active
    public float fogDuration = 30f;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has collided with this object
        if (other.CompareTag("Player"))
        {
            // Start the coroutine to activate the fog effect for a set duration
            StartCoroutine(ActivateFog());
        }
    }

    private IEnumerator ActivateFog()
    {
        // Activate the fog effect
        fogEffect.SetActive(true);

        // Wait for the duration specified
        yield return new WaitForSeconds(fogDuration);

        // Deactivate the fog effect after the time expires
        fogEffect.SetActive(false);
    }
}
