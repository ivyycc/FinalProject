using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hangSlider : MonoBehaviour
{
    /*
     * This script controlls the slider that displays the hangtime circular progress bar
     
     */
    // Start is called before the first frame update

    public GameObject hangTimeCanvas;
    public Slider hangTime;
    PlayerMovement playerController;
    void Start()
    {
        playerController = GetComponent<PlayerMovement>();
        hangTimeCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        hangTime.maxValue = playerController.maxHangTime;
        hangTime.minValue = 0; ;
        hangTime.value = playerController.currentHangTime;
        
    }

    public void showSlider()
    {
        hangTimeCanvas.SetActive(true);
    }

    public void hideSlider()
    {
        hangTimeCanvas.SetActive(false);
    }
}
