using System.Collections;
using UnityEngine;

public class RockFallHazard : MonoBehaviour
{
    public GameObject rockPrefab;
    public Transform player;
    public float fallHeight = 20f;
    public bool simulatePlayerEnter = false;
    public Transform birdSpawnPosition; 

    private Vector3 playerInitialPosition;
    public bool triggered = false;

    private void Update()
    { 
        if (simulatePlayerEnter == true)
        {
            SimulatePlayerEnter();
            simulatePlayerEnter = false;
        }
    }

    private void SimulatePlayerEnter()
    {
        //if player hits trigger object position, then instantiate....
        if(triggered == true)
        {
            

            playerInitialPosition = player.position;

            Vector3 spawnPosition = playerInitialPosition + Vector3.up * fallHeight;

            GameObject bird = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("rock instantiated");
            //if right click/left click, push away rock instantiation....?

            StartCoroutine(BirdAttack(bird));
            
        }
       
    }

    private IEnumerator BirdAttack(GameObject bird)
    {
       
        while (Vector3.Distance(bird.transform.position, player.position) > 0.1f)
        {
            bird.transform.position = Vector3.MoveTowards(bird.transform.position, player.position, Time.deltaTime * 5f);
            yield return null;
        }

     
        yield return new WaitForSeconds(1f);

     
        while (Vector3.Distance(bird.transform.position, birdSpawnPosition.position) > 0.1f)
        {
            bird.transform.position = Vector3.MoveTowards(bird.transform.position, birdSpawnPosition.position, Time.deltaTime * 5f);
            yield return null;
        }

       
        Destroy(bird);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            simulatePlayerEnter = true;
            triggered = true;
        }
        
    }
}
