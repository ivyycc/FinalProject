using System.Collections;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public ParticleSystem rain;
    public Material darkSkybox;
    public Light lightningLight;
    public ParticleSystem wind;
    private Material originalSkybox;

    void Start()
    {
        
        originalSkybox = RenderSettings.skybox;
   
        lightningLight.intensity = 0;
    }

    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartRain();                    // Starts Rain
        }
       
        if (Input.GetKeyDown(KeyCode.S))
        {
            StopRain();                     // Stops Rain
        }
     
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(TriggerLightning());    // Triggers Lightning
        }
 
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartWind();                            // Starts Wind
        }
      
        if (Input.GetKeyDown(KeyCode.E))
        {
            StopWind();                             // Stops Wind
        }
    }

    void StartRain()
    {
    
        rain.Play();
       
        RenderSettings.skybox = darkSkybox;

        Debug.Log("Is Rainy");
    }

    void StopRain()
    {
       
        rain.Stop();
        
        RenderSettings.skybox = originalSkybox;
        Debug.Log("Stopped Rain");
    }

    IEnumerator TriggerLightning()
    {
       
        lightningLight.intensity = 5; 
        yield return new WaitForSeconds(0.1f);
        lightningLight.intensity = 0;
        yield return new WaitForSeconds(0.1f);
        lightningLight.intensity = 5;
        yield return new WaitForSeconds(0.1f);
        lightningLight.intensity = 0;

        Debug.Log("Lightning triggered");
    }

    void StartWind()
    {
       
        wind.Play();
        Debug.Log("Is windy");
    }

    void StopWind()
    {
      
        wind.Stop();
        Debug.Log("Wind Stopped");
    }
}
