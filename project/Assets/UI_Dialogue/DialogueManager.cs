using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text headingText;
    public TMP_Text dialogueText;
    public GameObject dialoguePanel;
    //public Button next;

    [System.Serializable]
    public class Dialogue
    {
        public string speakerName;
        [TextArea(3, 10)]
        public string[] lines;
    }

    public Dialogue[] dialogues;

    private Queue<string> sentences;
    private Queue<string> speakers;

    private int currentDialogueIndex = 0;

    public bool triggerDialogue;

    void Start()
    {
        sentences = new Queue<string>();
        speakers = new Queue<string>();
        dialoguePanel.SetActive(false);
        triggerDialogue = true;
    }

    void Update()
    {
        if (triggerDialogue)
        {
            triggerDialogue = false;
            TriggerDialogue();
        }

        if (Input.GetKeyDown(KeyCode.Q))//dialoguePanel.activeSelf
        {
            EndDialogue();
            Debug.Log("Exit Dialogue");
        }
    }

    public void TriggerDialogue()
    {
        if (Input.GetKeyDown(KeyCode.Q))//dialoguePanel.activeSelf
        {
            dialoguePanel.SetActive(false);
            Debug.Log("Exit Dialogue");
        }
        else
        {
            StartDialogue(dialogues[currentDialogueIndex].speakerName, dialogues[currentDialogueIndex].lines);
            currentDialogueIndex = (currentDialogueIndex + 1) % dialogues.Length;
        }
    }

    public void StartDialogue(string speakerName, string[] lines)
    {
        dialoguePanel.SetActive(true);
        speakers.Clear();
        sentences.Clear();

        foreach (string line in lines)
        {
            speakers.Enqueue(speakerName);
            sentences.Enqueue(line);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        string speaker = speakers.Dequeue();


        headingText.text = speaker;

       
        if (speaker == "Randy")
        {
            headingText.color = new Color(0.1882f, 1.0f, 0.5843f);

        }
        else if (speaker == "Ranger")
        {
            headingText.color = new Color(1f, 0.753f, 0f); 
        }
        else
        {
            headingText.color = Color.white; 
        }

        dialogueText.text = sentence;
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Trigger1")
        {
            triggerDialogue = true;
            collision.enabled = false;
        }
        if (collision.gameObject.tag == "Trigger2")
        {
            triggerDialogue = true;
            collision.enabled = false;
        }
        if (collision.gameObject.tag == "Trigger3")
        {
            triggerDialogue = true;
            collision.enabled = false;
        }
        if (collision.gameObject.tag == "Trigger4")
        {
            triggerDialogue = true;
            collision.enabled = false;
        }
        if (collision.gameObject.tag == "Trigger5")
        {
            triggerDialogue = true;
            collision.enabled = false;
        }
    }
}
