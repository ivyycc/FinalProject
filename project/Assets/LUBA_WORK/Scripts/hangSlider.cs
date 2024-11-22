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

    public Slider shakyRockSlider;
    public GameObject shakyHangTime;

    PlayerMovement playerController;
    void Start()
    {
        playerController = GetComponent<PlayerMovement>();
        hangTimeCanvas.SetActive(false);
        shakyHangTime.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        hangTime.maxValue = playerController.maxHangTime;
        hangTime.minValue = 0; ;
        hangTime.value = playerController.currentHangTime;

        shakyRockSlider.maxValue = playerController.maxHangTime;
        shakyRockSlider.minValue = 0; ;
        shakyRockSlider.value = playerController.currentHangTime;
    }

    public void showSlider()
    {

        if(playerController.isShakyRock)
        {
            shakyHangTime.SetActive(true);
        }
        else
        {
            hangTimeCanvas.SetActive(true);
        }
        
    }

    public void showShakySlider()
    {
        shakyHangTime.SetActive(true);
    }

    public void hideSlider()
    {
        hangTimeCanvas.SetActive(false);
        shakyHangTime.SetActive(false);
    }
}
