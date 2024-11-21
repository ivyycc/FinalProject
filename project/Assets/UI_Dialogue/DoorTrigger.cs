using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI; // Required for working with UI Text
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    public GameObject messageText;
    public bool canMoveToNextScene = false;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canMoveToNextScene)
        {
            SceneManager.LoadScene("MainScene");
        }
    }

   
    private void OnTriggerEnter(Collider other)
    {
   
        if (other.CompareTag("Player")) 
        {
           

            messageText.SetActive(true); 
            Debug.Log("In Trigger");

            canMoveToNextScene = true;
        }
    }

   
    private void OnTriggerExit(Collider other)
    {
   
        if (other.CompareTag("Player"))
        {
            messageText.SetActive(false);
            canMoveToNextScene = false;
        }
    }
}
