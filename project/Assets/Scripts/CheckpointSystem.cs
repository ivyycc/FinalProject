using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    public int checkpointIndex; // Index of this checkpoint, assigned in the Inspector or dynamically
                                // public bool isActivated = false; // Ensure each checkpoint can only be activated once

    public List<GameObject> TreeGroups = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Respawn playerRespawn = other.gameObject.GetComponent<Respawn>();
            if (playerRespawn != null)
            {
                if (checkpointIndex > playerRespawn.currentCheckpointIndex)
                {
                    playerRespawn.currentCheckpointIndex = checkpointIndex;
                    playerRespawn.lastCheckpointPosition = transform.position;
                   
                    Debug.Log($"Checkpoint {checkpointIndex} activated. Player respawn position updated to: {transform.position}");
                    //isActivated = true; // Prevent multiple activations
                }
                
            }

            if(this.gameObject.tag == "checkpoint0")
            {
                TreeGroups[0].SetActive(true);
            }
            if (this.gameObject.tag == "checkpoint1")
            {
                TreeGroups[1].SetActive(true);
            }
            if (this.gameObject.tag == "checkpoint2")
            {
                TreeGroups[2].SetActive(true);
            }
            if (this.gameObject.tag == "checkpoint3")
            {
                TreeGroups[3].SetActive(true);
            }
           
        }

        
    }
}
