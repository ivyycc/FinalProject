using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuUIManager : MonoBehaviour
{
    public GameObject mainMenuPanel; 
    public GameObject settingsMenuPanel; 
    public GameObject pauseMenuPanel;
    public GameObject controlsMenuPanel;

   
    void Start()
    {
       
        mainMenuPanel.SetActive(true);
        settingsMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
    }

   
    void Update()
    {

    }

    
    public void OnPlayButtonPressed()
    {
    
        SceneManager.LoadScene("CutscenesScene");
    }

   
    public void OnSettingsButtonMainMenuPressed()
    {
     
        mainMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(true);
    }

    public void OnSettingsButtonPressed()
    {

        pauseMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(true);
    }



    public void OnQuitButtonPressed()
    {
        
        Application.Quit();
    }

  
    public void OnBackButtonMainMenuPressed()
    {
     
        settingsMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void OnBackButtonPressed()
    {

        settingsMenuPanel.SetActive(false);
        controlsMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    public void OnContinueButtonPressed()
    {

        pauseMenuPanel.SetActive(false);
    }

    public void OnControlsButtonPressed()
    {

        pauseMenuPanel.SetActive(false);
        controlsMenuPanel.SetActive(true);
    }


    public void OnControlsButtonMainMenuPressed()
    {

        mainMenuPanel.SetActive(false);
        controlsMenuPanel.SetActive(true);
    }
}
