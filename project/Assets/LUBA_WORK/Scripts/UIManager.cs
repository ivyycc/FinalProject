using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    public TMP_Text zoomtxt;
    private PlayerMovement playerManager;


    public GameObject Pause_panel;



    // Start is called before the first frame update
    void Start()
    {
        playerManager = Object.FindFirstObjectByType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        displayUI();
        Pause();

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

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause_panel.SetActive(true);
            Time.timeScale = 0f; // Freeze the game
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.Click, this.transform.position);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause_panel.SetActive(false);
            Time.timeScale = 1f; // UnFreeze the game
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.Back, this.transform.position);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {

    }

    public void SettingsMenu()
    {

    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


}

