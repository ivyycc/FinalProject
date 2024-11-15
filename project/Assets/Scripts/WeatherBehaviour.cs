using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherBehaviour : MonoBehaviour
{
    public float windStrength = 1.0f;
    public float smoothness = 10f; // Controls how smooth the movement is

    private Vector3 targetPosition;
    private PlayerMovement playermove;
    void Start()
    {
        targetPosition = transform.position;
        playermove = GetComponent<PlayerMovement>();
    }

    void OnParticleCollision(GameObject other)
    {
        ParticleSystem ps = other.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            playermove.speed = 3f;
            playermove.pullSpeed = 10f;

            /* Get the wind direction from the particle system
            var forceModule = ps.forceOverLifetime;
            Vector3 windDirection = new Vector3(
                forceModule.x.constant,
                forceModule.y.constant,
                forceModule.z.constant
            ).normalized;

            
            targetPosition += windDirection * windStrength;*/
        }
    }

    void Update()
    {
        // Smoothly move towards the target position
       /* transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * smoothness
        );*/
    }
}
