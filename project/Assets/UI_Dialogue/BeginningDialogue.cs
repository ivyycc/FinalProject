using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class BeginningDialogue : MonoBehaviour
{
    public CollectObject CollectObject;
    public GameObject dialoguePanel;
    public GameObject dialoguePanel2;
    public GameObject interactText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (CollectObject.isInInteractRange)
        {
            interactText.SetActive(true);
        }
        else
        {
            interactText.SetActive(false);
        }


        if (Input.GetKeyDown(KeyCode.Q))
        {
            dialoguePanel.SetActive(false);
            dialoguePanel2.SetActive(false);
        }

        if (CollectObject.numOfObjectsInteractedWith >= 3)
        {
            dialoguePanel.SetActive(false);
            dialoguePanel2.SetActive(true);
        }

       

    }
}
