using System.Collections;
using TMPro;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public bool isFog;

    //Weather Spirtes
    public GameObject fogSprite;
    public GameObject HotSprite;
    public bool isRain;
    public bool isHot;
    public bool isWindy;
    public bool isNormal;

    private PlayerMovement playerManager;
    // Brayden Script 
    private WeatherController weatherController;
    public TMP_Text weatherText; // TextMeshPro Text component for weather updates

    // Start is called before the first frame update
    void Start()
    {
        playerManager = Object.FindFirstObjectByType<PlayerMovement>();
        weatherController = Object.FindFirstObjectByType<WeatherController>();
        StartCoroutine(CycleWeather());
        fogSprite.SetActive(false);
    }

    private void Update()
    {
        PerformWeatherBehavior();
    }

    // Coroutine to cycle through weather conditions
    IEnumerator CycleWeather()
    {
        while (true)
        {
            // Normal weather for 1 minute
            yield return new WaitForSeconds(60);
            SetWeatherCondition("Foggy", true, false, false, false);  // Set fog
            PerformWeatherBehavior();
            yield return new WaitForSeconds(60);                      // Wait for 1 minute
            ResetWeather();
            yield return new WaitForSeconds(30);                      // 30 second rest

            SetWeatherCondition("Rainy", false, true, false, false);  // Set rain
            PerformWeatherBehavior();
            yield return new WaitForSeconds(60);                      // Wait for 1 minute
            ResetWeather();
            yield return new WaitForSeconds(30);                      // 30 second rest

            SetWeatherCondition("Hot", false, false, true, false);    // Set heat
            PerformWeatherBehavior();
            yield return new WaitForSeconds(60);                      // Wait for 1 minute
            ResetWeather();
            yield return new WaitForSeconds(30);                      // 30 second rest

            SetWeatherCondition("Windy", false, false, false, true);  // Set wind
            PerformWeatherBehavior();
            yield return new WaitForSeconds(60);                      // Wait for 1 minute
            ResetWeather();
            yield return new WaitForSeconds(30);                      // 30 second rest
        }
    }

    // Method to set weather conditions and update the weather text
    void SetWeatherCondition(string condition, bool fog, bool rain, bool hot, bool windy)
    {
        isFog = fog;
        isRain = rain;
        isHot = hot;
        isWindy = windy;
        isNormal = !fog && !rain && !hot && !windy;  // Weather is normal if none of the other conditions are active

        // Update the TextMeshPro text with the current condition
        weatherText.text = "Current Weather: " + condition;
    }

    // Method to reset all weather conditions to false and return to normal weather
    void ResetWeather()
    {
        isFog = false;
        isRain = false;
        isHot = false;
        isWindy = false;
        isNormal = true;  // Weather is normal when no other conditions are active

        // Reset the weather text when no weather is active
        weatherText.text = "Current Weather: Clear";
    }

    // Method to perform behavior based on the active weather condition
    void PerformWeatherBehavior()
    {
        if (isFog)
        {
            Debug.Log("Foggy weather: Reduce visibility for the player.");
            fogSprite.SetActive(true);
            // Add behavior like reducing visibility
        }
        else if (isRain)
        {
            Debug.Log("Rainy weather: Increase player slip or reduce movement speed.");
            playerManager.maxHangTime = 3f; // Normal Hang Time
            weatherController.StartRain();
        }
        else if (isHot)
        {
            Debug.Log("Hot weather: Drain player stamina faster.");
            weatherController.StartHotWeather();
            playerManager.maxHangTime = 7f; // Normal Hang Time
        }
        else if (isWindy)
        {
            Debug.Log("Windy weather: Make objects move or affect player movement.");
            weatherController.StartWind();
            // Add behavior like pushing the player or moving environmental objects
        }
        else if (isNormal)
        {
            Debug.Log("Normal weather: No special conditions.");
            playerManager.maxHangTime = 10f; // Normal Hang Time
            fogSprite.SetActive(false);
            weatherController.StopRain();
            weatherController.StopWind();
            weatherController.StopHotWeather();

        }
    }
}
