using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Respawn playerRespawn = other.gameObject.GetComponent<Respawn>();
            if (playerRespawn != null)
            {
                // Update the player's checkpoint position
                playerRespawn.lastCheckpointPosition = transform.position;
                Debug.Log("Checkpoint position updated to: " + playerRespawn.lastCheckpointPosition);
            }
        }
    }
}
