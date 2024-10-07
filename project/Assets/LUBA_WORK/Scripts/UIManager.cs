using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    public TMP_Text zoomtxt;
    private PlayerMovement playerManager;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        displayUI();
    }

    void displayUI()
    {
        if (playerManager.isZoomed)
        {
            zoomtxt.text = "Zoom Enabled";
        }
        else
        {
            zoomtxt.text = " ";
        }
    }
    
    

}

