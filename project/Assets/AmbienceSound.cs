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

    }
    private void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.name == "Zone1")
        {
            AudioManager.instance.InitializeWind(FMODEvents.instance.WindAmbience, this.transform.position);
        }
        else if(this.gameObject.name == "Zone2")
        {
            AudioManager.instance.InitializeWind(FMODEvents.instance.Rain, this.transform.position);
        }
        else if(this.gameObject.name == "Zone3")
        {
            AudioManager.instance.InitializeWind(FMODEvents.instance.Rain, this.transform.position);
        }
        
        Debug.Log("wind playing");
        Debug.Log(other.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        AudioManager.instance.StopSound(FMODEvents.instance.WindAmbience);
        AudioManager.instance.StopSound(FMODEvents.instance.Rain);
        Debug.Log("Wind stopped");
    }
}
