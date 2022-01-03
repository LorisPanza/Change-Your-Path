using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public GameObject canvas;
    public AudioManager audioManager;
    public GameObject tutorial;


    private bool dialogueEnded = false;
    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();

    }

    public void StartDialogue(Dialogue dialogue)
    {
        //Debug.Log("Starting conversation with " + dialogue.name);
        GameObject.Find("GameManager").GetComponent<GameManager>().enabled = false;
        
        dialogueEnded = false;

        PopOutBox();
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        AudioSource background = audioManager.GetSound("Background").source;
        background.Stop();
        
        audioManager.Play("DialogueBackground");
        AudioSource voiceSrc = audioManager.GetSound("Voice").source;
        voiceSrc.Play();


        DisplayNextSentence();
        //grey out TAB in minitutorial
        tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetComponent<CanvasRenderer>().SetColor(Color.grey);
        tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(9).GetComponent<CanvasRenderer>().SetColor(Color.grey);

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
        
        //Debug.Log("End of conversation");
        HideBox();
        dialogueEnded = true;
        //Quest di Wilem
        SimpleEventManager.TriggerEvent("StartQuest");
        //SimpleEventManager.TriggerEvent("EndElderQuest");
        
        
        AudioSource diabackground = audioManager.GetSound("DialogueBackground").source;
        diabackground.Stop();
        audioManager.Play("Background");

        //AudioSource background = audioManager.GetSound("Background").source;
        //background.Play();

        AudioSource voiceSrc = audioManager.GetSound("Voice").source;
        voiceSrc.Stop();
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
        GameObject.Find("GameManager").GetComponent<GameManager>().enabled = true;
        nameText.text = "";
        dialogueText.text = "";
        canvas.SetActive(false);
        //Now can press TAB
        tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetComponent<CanvasRenderer>().SetColor(Color.white);
        tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(9).GetComponent<CanvasRenderer>().SetColor(Color.white);
    }
}
