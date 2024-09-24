using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObject : MonoBehaviour
{
    [SerializeField] private GameObject[] radio;
    [SerializeField] private bool[] isCollected;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            for(int i=0; i<radio.Length; i++)
            {
                if(radio[i] == other)
                {
                    isCollected[i] = true;
                }
                else
                {
                    isCollected[i] = false;
                }
            }

            if(isCollected[0] == true && isCollected[1] == true && isCollected[2] == true)
            {
                //All objects collected 
            }

            //object collected = true
            //disable object in the scene
            //check if all objects collected 
        }
    }
}
