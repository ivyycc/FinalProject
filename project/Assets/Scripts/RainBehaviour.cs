using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainBehaviour : MonoBehaviour
{
    public ParticleSystem rain,wind;
    public Material darkSkybox;
    public Light lightningLight;
    public Light generalLight;
    private Material originalSkybox;
    public float rain_intensity, wind_intensity, generalLight_intensity, waitTime1, waitTime2, playerSpeed, playerPullSpeed;
    public bool canTriggerLightning;
    public PlayerMovement playerMove;
    private Coroutine lightningCoroutine;

     
    void Start()
    {

        originalSkybox = RenderSettings.skybox;
        lightningLight.intensity = 0;
        generalLight.intensity = 1;
        canTriggerLightning = true;
    }


    public void lightning(float t1, float t2)
    {
        if (canTriggerLightning)
        {
            lightningCoroutine = StartCoroutine(TriggerLightning(t1, t2));
        }
        
    }

    public void StopLightning()
    {
        if (lightningCoroutine != null)
        {
            StopCoroutine(lightningCoroutine);
            lightningCoroutine = null;
            lightningLight.intensity = 0; // Reset light intensity
            canTriggerLightning = true;   // Reset the flag
        }
    }

    public void StartRain()
    {

        rain.Play();

        //RenderSettings.skybox = darkSkybox;

        Debug.Log("Is Rainy");
    }

    public void IncreaseRain(float val, float val2)
    {
        var emission = rain.emission;
        emission.rateOverTime = val;
        generalLight.intensity = val2;
    }

    public void IncreaseWind(float val,float val2, float val3)
    {
        var emission = wind.emission;
        emission.rateOverTime = val;
        playerMove.speed = val2;
        playerMove.pullSpeed = val3;

    }
    IEnumerator TriggerLightning(float time1, float time2)
    {
        canTriggerLightning = false;

        lightningLight.intensity = 15;
        yield return new WaitForSeconds(time1);
        lightningLight.intensity = 0;

        yield return new WaitForSeconds(time1);
        

        lightningLight.intensity = 15;
        yield return new WaitForSeconds(time1);
        lightningLight.intensity = 0;

        yield return new WaitForSeconds(time2);
        Debug.Log("cooldown");
        canTriggerLightning = true;
        
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            IncreaseRain(rain_intensity, generalLight_intensity);
            IncreaseWind(wind_intensity, playerSpeed, playerPullSpeed);
            lightning(waitTime1, waitTime2);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopLightning();
    }
}
