using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{

    [field: Header("UI SFX")]
    [field: SerializeField] public EventReference PlayGame { get; private set; }
    [field: SerializeField] public EventReference Click { get; private set; }
    [field: SerializeField] public EventReference Back { get; private set; }

    [field: Header("All SFX")]
    [field: SerializeField] public EventReference playerWalk { get; private set; }
    [field: SerializeField] public EventReference playerWalkWood { get; private set; }
    [field: SerializeField] public EventReference playerJump { get; private set; }
    [field: SerializeField] public EventReference pickUp { get; private set; }
    [field: SerializeField] public EventReference RadioStatic { get; private set; }
    [field: SerializeField] public EventReference RadioStaticHalf { get; private set; }
    [field: SerializeField] public EventReference Alarm { get; private set; }
    [field: SerializeField] public EventReference hitRock { get; private set; }
    [field: SerializeField] public EventReference hitShakyRock { get; private set; }
    [field: SerializeField] public EventReference ShakyRockBreak { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference Music { get; private set; }
    [field: SerializeField] public EventReference Music2 { get; private set; }
    [field: SerializeField] public EventReference Music3 { get; private set; }
    [field: SerializeField] public EventReference MainMenuMusic { get; private set; }
    [field: SerializeField] public EventReference CutScenesMusic { get; private set; }

    [field: Header("Ambience")]
    [field: SerializeField] public EventReference Rain { get; private set; }
    [field: SerializeField] public EventReference WindAmbience { get; private set; }



    public static FMODEvents instance { get; private set; }

    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMODEevents in the scene");
            Destroy(gameObject);
            return;
        }

        instance = this;


        //if (instance != null)
        //{
        //  Debug.LogError("Found more than one Audio Manager in the scene");
        //}
        //instance = this;

        /*if (instance != null) //&& instance != this)
        {
            Debug.LogError("Found more than one Audio Manager in the scene");
            Destroy(gameObject);
            return;
        }
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;*/
        // DontDestroyOnLoad(gameObject);

    }
}