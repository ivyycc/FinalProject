using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    public int checkpointIndex; // Index of this checkpoint, assigned in the Inspector or dynamically
   // public bool isActivated = false; // Ensure each checkpoint can only be activated once

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
        }
    }
}
