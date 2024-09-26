using System.Collections;
using UnityEngine;

public class RockFallHazard : MonoBehaviour
{
    public GameObject rockPrefab;
    public Transform player;
    public float fallHeight = 5f;
    public bool simulatePlayerEnter = false;
    public Transform birdSpawnPosition; 

    private Vector3 playerInitialPosition;

    private void Update()
    {
        if (simulatePlayerEnter)
        {
            SimulatePlayerEnter();
            simulatePlayerEnter = false;
        }
    }

    private void SimulatePlayerEnter()
    {
        playerInitialPosition = player.position;

        Vector3 spawnPosition = playerInitialPosition + Vector3.up * fallHeight;

        GameObject bird = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);

     
        StartCoroutine(BirdAttack(bird));
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
}
