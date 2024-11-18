using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObject : MonoBehaviour
{
    //[SerializeField] private List<GameObject> objects = new List<GameObject>();
    //[SerializeField] private bool[] isCollected;
    //[SerializeField] private bool done;

    public float interactDistance = 5f; // Maximum interaction distance
    public GameObject playerCamera;        // Camera from which the raycast will originate
    public string interactableTag = "Interactable"; // Tag to identify interactable objects

    void Update()
    {
        
        InteractWithObject();
        
    }

    void InteractWithObject()
    {
        // Cast a ray from the camera's position and direction
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactDistance, Color.green);
            // Check if the object hit has the specific tag
            if (hit.collider.CompareTag(interactableTag))
            {
                Debug.Log($"You interacted with {hit.collider.gameObject.name}");
                hit.collider.GetComponent<Renderer>().material.color = Color.green;



                // Check for interaction input (e.g., pressing "E")
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.SetActive(false);
                }
                // Add interaction logic here
                // Example: Change color, play animation, etc.
                
            }
            else
            {
                Debug.Log("not interactable: " + hit.collider.tag);
            }
            
        }
    }

    /* voidOnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("object1"))
        {
            isCollected[0] = true;
            objects[0].SetActive(false);
        }
        if (other.gameObject.CompareTag("object2"))
        {
            isCollected[1] = true;
            objects[1].SetActive(false);
        }
        if (other.gameObject.CompareTag("object3"))
        {
            isCollected[2] = true;
            objects[2].SetActive(false);
        }
    }

    private void AllCollected()
    {
        if(isCollected[0] == true && isCollected[1] == true && isCollected[2] == true)
        {
            done = true;
        }
    }*/
}
