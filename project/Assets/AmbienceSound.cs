using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AmbienceSound : MonoBehaviour
{
    private void Start()
    {
        //AudioManager.instance.InitializeWind(FMODEvents.instance.WindAmbience, this.transform.position);
        CheckIfEventIs3D(FMODEvents.instance.Rain);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.name == "Zone1")
        {
            AudioManager.instance.InitializeWind(FMODEvents.instance.WindAmbience, this.transform.position);
            AudioManager.instance.InitializeSound(FMODEvents.instance.Music, this.transform.position);
        }
        else if(this.gameObject.name == "Zone2")
        {
            AudioManager.instance.InitializeWind(FMODEvents.instance.Rain, this.transform.position);
            AudioManager.instance.InitializeSound(FMODEvents.instance.Music2, this.transform.position);
        }
        else if(this.gameObject.name == "Zone3")
        {
            AudioManager.instance.InitializeWind(FMODEvents.instance.Rain, this.transform.position);
            AudioManager.instance.InitializeSound(FMODEvents.instance.Music3, this.transform.position);
        }
        
        Debug.Log("wind playing");
        Debug.Log(other.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        AudioManager.instance.StopSound2(FMODEvents.instance.WindAmbience);
        AudioManager.instance.StopSound2(FMODEvents.instance.Rain);
        AudioManager.instance.StopSound3(FMODEvents.instance.Music); 
        AudioManager.instance.StopSound3(FMODEvents.instance.Music2); 
        AudioManager.instance.StopSound3(FMODEvents.instance.Music3);
        Debug.Log("Wind stopped");
    }



    void CheckIfEventIs3D(EventReference eventRef)
    {
        // Create an Event Description
        EventDescription eventDescription;
        RuntimeManager.StudioSystem.getEvent(eventRef.Path, out eventDescription);

        if (eventDescription.isValid())
        {
            bool is3D;
            eventDescription.is3D(out is3D);

            if (is3D)
            {
                Debug.Log($"The FMOD event '{eventRef.Path}' is 3D.");
            }
            else
            {
                Debug.Log($"The FMOD event '{eventRef.Path}' is 2D.");
            }
        }
        else
        {
            Debug.LogError("Invalid FMOD Event Description.");
        }
    }
}
