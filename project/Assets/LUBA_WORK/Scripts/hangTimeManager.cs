using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hangTimeManager : MonoBehaviour
{

    PlayerMovement player;
    WeatherController weather;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>();
        weather = FindAnyObjectByType<WeatherController>();
        player.maxHangTime = 5;
    }

    // Update is called once per frame
    void Update()
    {
        calculateHangTime();
    }

    void calculateHangTime()
    {
        // weather logic here
        //hang time logic here
    }
}
