using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI; // Required for working with UI Text
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    public GameObject messageText; // Reference to the Text UI element
    public bool canMoveToNextScene = false;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canMoveToNextScene)
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    // This function is called when another collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that enters the trigger is the player (you can use tags or names to identify the player)
        if (other.CompareTag("Player")) 
        {
            // Display the message

            messageText.SetActive(true); // Ensure the text is visible
            Debug.Log("In Trigger");

            canMoveToNextScene = true;
        }
    }

    // This function is called when the collider exits the trigger
    private void OnTriggerExit(Collider other)
    {
        // If the player exits the trigger area, hide the text
        if (other.CompareTag("Player"))
        {
            messageText.SetActive(false); // Hide the text when the player exits
            canMoveToNextScene = false;
        }
    }
}
