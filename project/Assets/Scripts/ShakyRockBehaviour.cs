using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakyRockBehaviour : MonoBehaviour
{
    private Animator anim;
    public PlayerMovement checkOnRock;
    public bool hang;

    public float current, max;
   // private FMOD.Studio.EventInstance rockShakyInstance;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void Update()
    {
        hang = checkOnRock.isHanging;
        current = checkOnRock.currentHangTime;
        max = checkOnRock.maxHangTime;


        if (hang == true)
        {
            anim.SetInteger("RAnim", 1);
            //AudioManager.instance.PlayOneShot(FMODEvents.instance.hitShakyRock, this.transform.position);
        }
        else if(hang == false)
        {
            anim.SetInteger("RAnim", 0);
            //AudioManager.instance.StopSound(rockShakyInstance);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);//if player stands on the shaky rock
            AudioManager.instance.PlayOneShot(FMODEvents.instance.ShakyRockBreak, this.transform.position);
        }
    }

    
}
