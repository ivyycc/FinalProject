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

        AudioManager.instance.PlayOneShot(FMODEvents.instance.PlayGame, this.transform.position);
        SceneManager.LoadScene("CutscenesScene");
    }


    public void OnSettingsButtonMainMenuPressed()
    {

        AudioManager.instance.PlayOneShot(FMODEvents.instance.Click, this.transform.position);
        mainMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(true);
    }

    public void OnSettingsButtonPressed()
    {

        AudioManager.instance.PlayOneShot(FMODEvents.instance.Click, this.transform.position);
        pauseMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(true);
    }

    public void OnQuitButtonPressed()
    {

        AudioManager.instance.PlayOneShot(FMODEvents.instance.Back, this.transform.position);
        Application.Quit();
    }


    public void OnBackButtonMainMenuPressed()
    {

        AudioManager.instance.PlayOneShot(FMODEvents.instance.Back, this.transform.position);
        settingsMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void OnBackButtonPressed()
    {

        AudioManager.instance.PlayOneShot(FMODEvents.instance.Back, this.transform.position);
        settingsMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    public void OnContinueButtonPressed()
    {

        AudioManager.instance.PlayOneShot(FMODEvents.instance.Click, this.transform.position);
        pauseMenuPanel.SetActive(false);
    }

    public void OnControlsButtonPressed()
    {

        AudioManager.instance.PlayOneShot(FMODEvents.instance.Click, this.transform.position);
        pauseMenuPanel.SetActive(false);
        controlsMenuPanel.SetActive(true);
    }


    public void OnControlsButtonMainMenuPressed()
    {

        AudioManager.instance.PlayOneShot(FMODEvents.instance.Click, this.transform.position);
        mainMenuPanel.SetActive(false);
        controlsMenuPanel.SetActive(true);
    }
}
