using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AmbienceSound : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.InitializeWind(FMODEvents.instance.WindAmbience, this.transform.position);

    }
    private void OnTriggerEnter(Collider other)
    {
        //AudioManager.instance.InitializeWind(FMODEvents.instance.WindAmbience, this.transform.position);
        Debug.Log("wind playing");
        Debug.Log(other.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
