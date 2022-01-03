using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool isQuestGiver;
    public Quest quest;
    public Player player;
    //public Map map; reference to the map piece involved in the quest
    //public GameObject whoAreYouCanvas;
    public GameObject collectablemap;

    public Dialogue dialogue1;
    public Dialogue dialogue2;
    private Coroutine talk;
    private bool started = false;
    public AudioManager audioManager;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            talk = StartCoroutine(Talk());
            //whoAreYouCanvas.SetActive(true);
            //Debug.Log(name);
            SimpleEventManager.StartListening("StartQuest", StartQuest);
        }
    }

    void StartQuest()
    {
        if (isQuestGiver)
        {
            if (FindObjectOfType<DialogueManager>().DialogueEnded())
            {
                if (quest.isActive == false)
                {
                    quest.isActive = true;
                    if (GameObject.Find("MapPiece 6") == null) collectablemap.SetActive(true);
                    SimpleEventManager.StopListening("StartQuest", StartQuest);
                }
                else
                {
                    SimpleEventManager.StopListening("StartQuest", StartQuest);
                }
               
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (started)
            {
                AudioSource dialoguebackground = audioManager.GetSound("DialogueBackground").source;
                dialoguebackground.Stop();
                AudioSource background=audioManager.GetSound("Background").source;
                background.Play();
                AudioSource voiceSrc = audioManager.GetSound("Voice").source;
                voiceSrc.Stop();
            }
            //whoAreYouCanvas.SetActive(false);
            StopCoroutine(talk);
            StopCoroutine(Talk());
            started = false;
            FindObjectOfType<DialogueManager>().HideBox();
            
        }
    }

    public void TriggerDialogue1()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue1);
    }
    
    public void TriggerDialogue2()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue2);
    }

    IEnumerator Talk()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space) && GameObject.Find("GameManager").GetComponent<GameManager>().getMode()==1)
            {
                //Debug.Log("Quest: "+quest.isComplete);
                if (!started)
                {
                    //whoAreYouCanvas.SetActive(false);
                    if (quest.isComplete == true)
                    {
                        TriggerDialogue2();
                    }
                    else
                    {
                        TriggerDialogue1();
                        Debug.Log("I'm a routine...");
                    }
                    
                    started = true;
                }
                else
                {
                    FindObjectOfType<DialogueManager>().DisplayNextSentence();
                }
                yield return null;
            }
            yield return null;
        }
    }

}
