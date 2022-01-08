using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private bool flag=false;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Inizio dialog manager");
        sentences = new Queue<string>();
        //SimpleEventManager.StartListening("EndGame",flagChange);

    }

    private void Awake()
    {
        Debug.Log("Inizio dialog manager");
        SimpleEventManager.StartListening("EndGame",flagChange);
    }

    public void flagChange()
    {
        Debug.Log("Ho ascoltato");
        flag = true;
        //SimpleEventManager.StopListening("EndGame",flag);
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

        if (!flag)
        {
            AudioSource background = audioManager.GetSound("Background").source;
            background.Stop();
            audioManager.Play("DialogueBackground"); 
        }
        
        
        if (dialogue.name == "Robot") {
            audioManager.Play("RobotVoice");
            //AudioSource voiceSrc = audioManager.GetSound("RobotVoice").source;
            //voiceSrc.Play();
        } else {
            audioManager.Play("Voice");
            //AudioSource voiceSrc = audioManager.GetSound("Voice").source;
            //voiceSrc.Play();
        }

        
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

        if (!flag)
        {
            AudioSource diabackground = audioManager.GetSound("DialogueBackground").source;
            diabackground.Stop();
            audioManager.Play("Background");
        }
        

        //AudioSource background = audioManager.GetSound("Background").source;
        //background.Play();

        if (SceneManager.GetActiveScene().name == "SpringScene")
        {
            AudioSource voiceSrc1 = audioManager.GetSound("RobotVoice").source;
            voiceSrc1.Stop();
        }
        

        AudioSource voiceSrc2 = audioManager.GetSound("Voice").source;
        voiceSrc2.Stop();
        

        
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
