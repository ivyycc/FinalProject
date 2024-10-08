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
            
            float randomXOffset = Random.Range(-5f, 5f);
            Vector3 spawnPosition = player.position + Vector3.up * hoverHeight + Vector3.right * randomXOffset;
            GameObject birdInstance = Instantiate(birdPrefab, spawnPosition, Quaternion.identity);
            spawnedBirds.Add(birdInstance);
            currentBirdSpawnCount++;
            Debug.Log("Spawned Bird in loop");

            StartCoroutine(HoverAndAttack(birdInstance, spawnPosition));

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator HoverAndAttack(GameObject birdInstance, Vector3 originalPosition)
    {
        while (true)
        {
           
            yield return new WaitForSeconds(hoverDuration);

            if (birdInstance != null)
            {
                Rigidbody birdRb = birdInstance.GetComponent<Rigidbody>();
                birdRb.velocity = (player.position - birdInstance.transform.position).normalized * birdSpeed;

               
                yield return new WaitForSeconds(Vector3.Distance(player.position, birdInstance.transform.position) / birdSpeed);

     
                birdRb.velocity = (originalPosition - birdInstance.transform.position).normalized * birdSpeed;

           
                yield return new WaitForSeconds(Vector3.Distance(originalPosition, birdInstance.transform.position) / 10f);
            }
        }
    }
}
