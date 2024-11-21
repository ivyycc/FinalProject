using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AmbienceSound : MonoBehaviour
{
    private FMOD.Studio.EventInstance MusicInstance;
    private float semitones;
    public float pitch;
    public bool isPlaying;
    private void Start()
    {
        //AudioManager.instance.InitializeWind(FMODEvents.instance.WindAmbience, this.transform.position);
        //CheckIfEventIs3D(FMODEvents.instance.Rain);
        //AudioManager.instance.InitializeWind(FMODEvents.instance.Music, this.transform.position);
    }
   /* private void OnTriggerEnter(Collider other)
    {

        pitch = Mathf.Pow(2.0f, semitones / 12.0f);
        if (this.gameObject.name == "Zone1")
        {
            AudioManager.instance.InitializeWind(FMODEvents.instance.WindAmbience, this.transform.position);
            
            MusicInstance = AudioManager.instance.InitializeRadio(FMODEvents.instance.Music, this.transform.position);
            semitones = 4;
            MusicInstance.setPitch(pitch);
            Debug.Log("Wind1 is playing...");
        }
        else if(this.gameObject.name == "Zone2")
        {
            AudioManager.instance.InitializeWind(FMODEvents.instance.Rain, this.transform.position);
            semitones = 7;
            MusicInstance.setPitch(pitch);
        }
        else if(this.gameObject.name == "Zone3")
        {
            AudioManager.instance.InitializeWind(FMODEvents.instance.Rain, this.transform.position);
            semitones = 12;
            MusicInstance.setPitch(pitch);
        }

        
        
        Debug.Log("wind playing");
        Debug.Log(other.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        if (this.gameObject.name == "Zone1")
        {
          
        }
        else if (this.gameObject.name == "Zone2")
        {
            
        }
        else if (this.gameObject.name == "Zone3")
        {
            AudioManager.instance.StopSound2();
            
        }

        
    }*/

    private void OnTriggerEnter(Collider other)
    {
        pitch = Mathf.Pow(2.0f, semitones / 12.0f);

        if (this.gameObject.name == "Zone1")
        {
            AudioManager.instance.InitializeWind(FMODEvents.instance.WindAmbience, this.transform.position);

            // Only initialize music if it's not already playing
            if (!isPlaying)
            {
                MusicInstance = AudioManager.instance.InitializeRadio(FMODEvents.instance.Music, this.transform.position);
                isPlaying = true;
            }

            semitones = 6;
            MusicInstance.setPitch(pitch);
            Debug.Log("Zone1: Pitch changed to " + pitch);
        }
        else if (this.gameObject.name == "Zone2")
        {
            AudioManager.instance.InitializeWind(FMODEvents.instance.Rain, this.transform.position);

            if (!isPlaying)
            {
                MusicInstance = AudioManager.instance.InitializeRadio(FMODEvents.instance.Music, this.transform.position);
                isPlaying = true;
            }

            semitones = 10;
            MusicInstance.setPitch(pitch);
            Debug.Log("Zone2: Pitch changed to " + pitch);
        }
        else if (this.gameObject.name == "Zone3")
        {
            AudioManager.instance.InitializeWind(FMODEvents.instance.Rain, this.transform.position);

            if (!isPlaying)
            {
                MusicInstance = AudioManager.instance.InitializeRadio(FMODEvents.instance.Music, this.transform.position);
                isPlaying = true;
            }

            semitones = 14;
            MusicInstance.setPitch(pitch);
            Debug.Log("Zone3: Pitch changed to " + pitch);
        }
    }

    // For smooth pitch transitions between zones
    private IEnumerator PitchTransition(float startPitch, float targetPitch, float duration)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float currentPitch = Mathf.Lerp(startPitch, targetPitch, elapsed / duration);
            MusicInstance.setPitch(currentPitch);
            yield return null;
        }
        MusicInstance.setPitch(targetPitch);
    }

    private void OnTriggerExit(Collider other)
    {
        // If you want to stop the music completely when leaving the last zone
        if (this.gameObject.name == "Zone3")
        {
            if (isPlaying)
            {
                AudioManager.instance.StopSound2();
                isPlaying = false;
            }
        }
        // Otherwise, you might want to transition the pitch back to a default value
        else
        {
            float currentPitch;
            MusicInstance.getPitch(out currentPitch);
            StartCoroutine(PitchTransition(currentPitch, 1.0f, 0.5f)); // Transition back to normal pitch
        }
    }

    // You might want to add this to clean up
    private void OnDisable()
    {
        if (isPlaying)
        {
            AudioManager.instance.StopSound2();
            isPlaying = false;
        }
    }

    /*void CheckIfEventIs3D(EventReference eventRef)
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
    }*/
}
