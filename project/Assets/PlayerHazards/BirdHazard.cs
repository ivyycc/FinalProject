using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BirdHoverAttack : MonoBehaviour
{
    public GameObject birdPrefab;
    public Transform player;
    public float hoverHeight = 10f;
    public float hoverDuration = 3f;
    public float birdSpeed = 5f;
    public float spawnInterval = 5f;
    public bool triggerBirdAttack = false;
    public int maxBirdSpawnCount = 5;
    public int currentBirdSpawnCount = 0;

    public float birdHealth = 1f;
    public float birdDamageTaken = 0f;

    public List<GameObject> spawnedBirds = new List<GameObject>();
    private bool isSpawning = false;

    private void Update()
    {
        if (triggerBirdAttack && !isSpawning)
        {
            triggerBirdAttack = false;
                
            StartCoroutine(SpawnBirdsContinuously());
            Debug.Log("Bird Spawn Started");
            
            
        }

        if (birdDamageTaken >= birdHealth)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator SpawnBirdsContinuously()
    {
        
            isSpawning = true;


            while (true)
            {
               //if (currentBirdSpawnCount < maxBirdSpawnCount)
               //{

                Vector3 spawnPosition = player.position + Vector3.up * hoverHeight;
                GameObject birdInstance = Instantiate(birdPrefab, spawnPosition, Quaternion.identity);
                spawnedBirds.Add(birdInstance);
                currentBirdSpawnCount++;
                Debug.Log("Spawned Bird in loop");


                StartCoroutine(HoverAndAttack(birdInstance));


                yield return new WaitForSeconds(spawnInterval);
               //}
               
        }
            
            

        
    }

    private IEnumerator HoverAndAttack(GameObject birdInstance)
    {

        yield return new WaitForSeconds(hoverDuration);


        if (birdInstance != null)
        {
            Rigidbody birdRb = birdInstance.GetComponent<Rigidbody>();
            birdRb.velocity = (player.position - birdInstance.transform.position).normalized * birdSpeed;
        }
    }
}
