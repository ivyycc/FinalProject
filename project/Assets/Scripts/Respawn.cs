using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public CharacterController characterController;
    public Vector3 lastCheckpointPosition;
    public bool isFalling;
    private bool isRespawning;
    public int currentCheckpointIndex = -1; // Track the last reached checkpoint (-1 means no checkpoint yet)

    [SerializeField] public List<GameObject> checkpoints = new List<GameObject>();
    void Start()
    {
        // Store initial position as first checkpoint
        currentCheckpointIndex = 0;
        lastCheckpointPosition = checkpoints[0].transform.position;
        characterController = this.GetComponent<CharacterController>();
        Debug.Log("Zone1: " + checkpoints[0].transform.position + ", Zone2:  " + checkpoints[1].transform.position + ", Zone3: " + checkpoints[2].transform.position);
    }

    void Update()
    {
        if (!isRespawning)
        {
            isFalling = !characterController.isGrounded && characterController.velocity.y < -35f;
            if (isFalling && currentCheckpointIndex >= 0)
            {
                RespawnPlayer();
            }
        }
    }

    public void RespawnPlayer()
    {
        Debug.Log("RESPAWN at position: " + lastCheckpointPosition);
        characterController.enabled = false; // Disable controller before teleporting
        transform.position = lastCheckpointPosition;
        characterController.enabled = true; // Re-enable controller
        isRespawning = true;
        isFalling = false;
        /*if(lastCheckpointPosition == checkpoints[0].transform.position)
        {
            lastCheckpointPosition = checkpoints[1].transform.position;
        }
        else if(lastCheckpointPosition == checkpoints[1].transform.position)
        {
            lastCheckpointPosition = checkpoints[2].transform.position;
        }*/

    }

    void FixedUpdate()
    {
        if (isRespawning && characterController.isGrounded)
        {
            isRespawning = false;
            Debug.Log("Player grounded after respawn");
        }
    }


    /*private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "checkpoint0")
        {
            passedCheckpoint = true;
        }
        if (collision.gameObject.tag == "checkpoint1")
        {
            passedCheckpoint = true;
        }
        if (collision.gameObject.tag == "checkpoint2")
        {
            passedCheckpoint = true;
        }
        if (collision.gameObject.tag == "checkpoint3")
        {
            passedCheckpoint = true;
        }
       


    }*/
}
