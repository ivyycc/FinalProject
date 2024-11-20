using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    [Header("Volume")]
    [Range(0, 1)]
    public float masterVolume = 1;
    [Range(0, 1)]
    public float musicVolume = 1;
    [Range(0, 1)]
    public float SFXVolume = 1;
    [Range(0, 1)]
    public float AmbienceVolume = 1;

    private Bus masterBus;
    private Bus musicBus;
    private Bus sfxBus;
    private Bus AmbienceBus;

    [SerializeField] public List<EventInstance> eventInst;


    private EventInstance musicEventInstance;
    private EventInstance musicEventInstance2;
    private EventInstance WindEventInstance;
    private EventInstance RainEventInstance;
    private EventInstance radioInstance;
    public static AudioManager instance { get; private set; }

    public Dictionary<EventReference, FMOD.Studio.EventInstance> activeSoundInstances = new Dictionary<EventReference, FMOD.Studio.EventInstance>();

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        

        /*if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene");
            Destroy(gameObject);
            return;
        }

        instance = this;


        eventInst = new List<EventInstance>();
        */

        masterBus = RuntimeManager.GetBus("bus:/");
        //RuntimeManager.LoadBank("Master");
        //RuntimeManager.LoadBank("Master.strings");

        FMODUnity.RuntimeManager.LoadBank("Master", true); // Replace "Master" with your actual bank name
        FMODUnity.RuntimeManager.LoadBank("Master.strings", true);

        FMODUnity.RuntimeManager.LoadBank("Ambience", true); // Replace "Master" with your actual bank name
       

        FMODUnity.RuntimeManager.LoadBank("Music", true); // Replace "Master" with your actual bank name

        FMODUnity.RuntimeManager.LoadBank("SFX", true); // Replace "Master" with your actual bank name

        musicBus = RuntimeManager.GetBus("bus:/Music");
        AmbienceBus = RuntimeManager.GetBus("bus:/Ambience");
        sfxBus = RuntimeManager.GetBus("bus:/SoundEffects");

        //InitializeMusic(FMODEvents.instance.Music);
    }

    public void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.buildIndex == 1)
        {

            //InitializeMusic(FMODEvents.instance.Music);

        }
        if (currentScene.buildIndex == 2)
        {
            //nitializeWind(FMODEvents.instance.WindAmbience);
        }

    }

    private void Update()
    {
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);
        AmbienceBus.setVolume(AmbienceVolume);
        sfxBus.setVolume(SFXVolume);
    }



    public void PlayOneShot(EventReference sound, Vector3 worldpos)
    {
        FMODUnity.RuntimeManager.PlayOneShot(sound, worldpos);

    }

    public EventInstance CreateEventInstance(EventReference eR)
    {
        EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(eR);
        eventInst.Add(eventInstance);

        return eventInstance;
    }

    public void InitializeSound(EventReference soundEventRef, Vector3 position)
    {
        // Stop existing instance of this sound if it exists
        if (activeSoundInstances.ContainsKey(soundEventRef))
        {
            StopSound3(soundEventRef);
            Debug.Log("sound is stopped because it already exists");
        }

        FMOD.Studio.EventInstance soundEventInstance = RuntimeManager.CreateInstance(soundEventRef);

        var attributes = RuntimeUtils.To3DAttributes(position);
        soundEventInstance.set3DAttributes(attributes);
        soundEventInstance.start();

        // Store the instance
        activeSoundInstances[soundEventRef] = soundEventInstance;
    }


    public void StopSound3(EventReference soundEventRef)
    {
        if (activeSoundInstances.TryGetValue(soundEventRef, out FMOD.Studio.EventInstance instance))
        {
            instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            instance.release();
            activeSoundInstances.Remove(soundEventRef);
        }
        else
        {
            Debug.Log("MUSIC not stopping :(");
        }
    }
    public void InitializeMusic2(EventReference musicEventReference2)
    {
        musicEventInstance2 = RuntimeManager.CreateInstance(musicEventReference2);

        musicEventInstance2.start();
        Debug.Log("MUSIC STARTED");

        musicEventInstance2.release();
        Debug.Log("Wind STARTED");
    }

    public void InitializeWind(EventReference WindEventRef, Vector3 position)
    {
        WindEventInstance = RuntimeManager.CreateInstance(WindEventRef);
        

        // Set the 3D attributes using the provided position
        var attributes = RuntimeUtils.To3DAttributes(position);
        WindEventInstance.set3DAttributes(attributes);

        WindEventInstance.start();

        
        //WindEventInstance.release();
        
    }

   
    public void InitializeStartMusic(EventReference startMusicRef)
    {
        //WindEventInstance = RuntimeManager.CreateInstance(WindEventRef);
        FMOD.Studio.EventInstance GameMusicInstance = RuntimeManager.CreateInstance(startMusicRef);
        //musicEventInstance = CreateEventInstance(musicEventReference);event:/Music/BackgroundMusic
        GameMusicInstance.start();
        Debug.Log("Wind STARTED");
    }

    public EventInstance InitializeRadio(EventReference windEvent, Vector3 position)
    {
        radioInstance = RuntimeManager.CreateInstance(windEvent);
        radioInstance.set3DAttributes(RuntimeUtils.To3DAttributes(position));
        radioInstance.start();
        return radioInstance;
    }
    public void StopSound(EventInstance eventInstance)
    {
    
        if (eventInstance.isValid()) // Ensure the event instance exists
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); // Use STOP_MODE.IMMEDIATE for abrupt stop
            eventInstance.release(); // Release the instance to free up resources
            Debug.Log("sound stopped");
        }
        else
        {
            Debug.LogWarning("Attempted to stop a sound that isn't valid or playing.");
        }
    }
   
    public void StopSound2(EventReference soundEvent)
    {

        FMOD.Studio.EventInstance eventInstance;
        eventInstance = FMODUnity.RuntimeManager.CreateInstance(soundEvent);

        // Stop the instance
        eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); // Use STOP_MODE.IMMEDIATE to stop abruptly
        eventInstance.release(); // Release the instance to free up resources
    }


    public void CleanUp()
    {
        //stop and release any created instances
        if (eventInst != null)
        {

            foreach (EventInstance eventInstance in eventInst)
            {
                eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                eventInstance.release();
            }
        }
    }



 
    private void OnDestroy()
    {
        CleanUp();
    }
    private void OnEnable()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        CleanUp();
    }

    /*public void StopMusic2()
    {
        if (musicEventInstance2.isValid())
        {
            musicEventInstance2.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            musicEventInstance2.release();
        }
    }*/



    /* if (instance != null && instance != this)
     {
         Debug.LogError("Found more than one Audio Manager in the scene");
         Destroy(gameObject);
         return;
     }*/

    //if (instance != null)
    //{
    //  Debug.LogError("Found more than one Audio Manager in the scene");
    //}


    //instance = this;
    // DontDestroyOnLoad(gameObject);

    /* if (instance == null)
     {
         instance = this;
         DontDestroyOnLoad(gameObject);
     }
     else
     {
         Destroy(gameObject);
         return;
     }*/
}