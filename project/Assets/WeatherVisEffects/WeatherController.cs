using System.Collections;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public ParticleSystem rain;
    public Material darkSkybox;
    public Material hotSkybox;
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
      
       
     
        
 
      
    }
    public void lightning()
    {
        StartCoroutine(TriggerLightning());
    }

    public void StartRain()
    {
    
        rain.Play();
       
        RenderSettings.skybox = darkSkybox;

        Debug.Log("Is Rainy");
    }

    public void StartHotWeather()
    {

    
        RenderSettings.skybox = hotSkybox;

        Debug.Log("Is Hot");
    }
    public void StopHotWeather()
    {
        RenderSettings.skybox = originalSkybox;
    }

    public void StopRain()
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

   public void StartWind()
    {
       
        wind.Play();
        Debug.Log("Is windy");
    }

    public void StopWind()
    {
      
        wind.Stop();
        Debug.Log("Wind Stopped");
    }
}
