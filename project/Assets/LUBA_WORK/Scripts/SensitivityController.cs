using UnityEngine;
using UnityEngine.UI;

public class SensitivityController : MonoBehaviour
{
    public PlayerLook playerLook;  // Reference to the PlayerLook script
    public Slider sensitivitySlider;  // Reference to the UI Slider
    //public Text sensitivityText;  // Reference to the UI Text
    float currentValue;
    void Start()
    {

        playerLook = FindAnyObjectByType<PlayerLook>();
        // Initialize slider values
        sensitivitySlider.minValue = 5f;
        sensitivitySlider.maxValue = 15f;
        sensitivitySlider.value = 10f;  // Default value
        
        UpdateSensitivity();
    }

    
    public void UpdateSensitivity()
    {
        float sensitivity = sensitivitySlider.value;

        // Update PlayerLook sensitivity
        playerLook.UpdateSensitivity(sensitivity);

        // Update UI text
        Debug.Log( $"Sensitivity: {sensitivity:F1}");

        Debug.Log("Sensitivty Updated");
    }
}
