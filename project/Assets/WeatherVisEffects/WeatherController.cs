using System.Collections;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public ParticleSystem rain;
    public Material darkSkybox;
    //public Material hotSkybox;
    public Light lightningLight;
    public ParticleSystem wind;
    private Material originalSkybox;

    [SerializeField] float windIntensity;
    [SerializeField] float Xdir, Ydir, Zdir;
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject camera;
    [SerializeField] public Rigidbody playerRb;
    [SerializeField] public Rigidbody camRb;
    [SerializeField] private bool playerInWindZone = false;
    void Start()
    {
        
        originalSkybox = RenderSettings.skybox;
        lightningLight.intensity = 0;
        playerRb = player.GetComponent<Rigidbody>();
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

    public void IncreaseRain(float val)
    {
        var emission = rain.emission;
        emission.rateOverTime = val;
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



    /*private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            //windzone Apply wind force to the player
            /*windIntensity = 1f;
            Xdir = 4f;
            Ydir = 0f;
            Zdir = 0f;
            Vector3 forceDirection = new Vector3(Xdir, Ydir, Zdir) * windIntensity * Time.deltaTime;
            other.transform.position += forceDirection;
            Debug.Log(other.gameObject.tag);
        }

       
    }*/


    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInWindZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInWindZone = false;
        }
    }

    private void FixedUpdate()
    {
        if (playerInWindZone)
        {
            /*Vector3 forceDirection = new Vector3(Xdir, Ydir, Zdir) * windIntensity * Time.fixedDeltaTime;
            playerRb.MovePosition(playerRb.position + forceDirection);
            Transform pT = player.transform;
            Vector3 targetPosition = pT.position + new Vector3(Xdir, Ydir, Zdir) * windIntensity;
            pT.position = Vector3.Lerp(pT.position, targetPosition, 10f); // Adjust 0.1f for speed
            
            Vector3 windVelocity = new Vector3(Xdir, Ydir, Zdir) * windIntensity * Time.fixedDeltaTime;

            // Adjust the current velocity with the wind direction
            playerRb.velocity += windVelocity;
            Debug.Log("wind");*/

            Vector3 windForce = new Vector3(Xdir,Ydir,Zdir).normalized * windIntensity * camRb.mass;

            // Apply the wind force to the camera rig
            camRb.AddForce(windForce, ForceMode.Acceleration);

            // Synchronize the player's transform with the camera rig
            player.transform.position = camera.transform.position;
            //playerTransform.rotation = transform.rotation;
        }
    }

    /*windIntensity = 0.5f;
            Xdir = 10f;
            Ydir = 0.5f;
            Zdir = 0.5f;*/

    /*public void StartHotWeather()
    {

    
        RenderSettings.skybox = hotSkybox;

        Debug.Log("Is Hot");
    }
    public void StopHotWeather()
    {
        RenderSettings.skybox = originalSkybox;
    }
    */
    /* public void StartWind()
      {

          wind.Play();
          Debug.Log("Is windy");
      }

      public void StopWind()
      {

          wind.Stop();
          Debug.Log("Wind Stopped");
      }*/
}
