using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text headingText;
    public TMP_Text dialogueText;
    public GameObject dialoguePanel;

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
    }

    void Update()
    {
        if (triggerDialogue)
        {
            triggerDialogue = false;
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        if (dialoguePanel.activeSelf)
        {
            dialoguePanel.SetActive(false);
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
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}
