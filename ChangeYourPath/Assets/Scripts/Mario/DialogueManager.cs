using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public GameObject canvas;


    private bool dialogueEnded = false;
    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();

    }

    public void StartDialogue(Dialogue dialogue) {
        Debug.Log("Starting conversation with " + dialogue.name);
        dialogueEnded = false;

        PopOutBox();
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //no more sentences
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
        HideBox();
        dialogueEnded = true;
    }

    public bool DialogueEnded()
    {
        return dialogueEnded;
    }

    void PopOutBox()
    {
        canvas.SetActive(true);
    }

    public void HideBox()
    {
        nameText.text = "";
        dialogueText.text = "";
        canvas.SetActive(false);
    }
}
