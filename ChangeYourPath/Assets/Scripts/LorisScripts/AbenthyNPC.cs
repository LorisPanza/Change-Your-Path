using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbenthyNPC : MonoBehaviour
{
   
    public Dialogue dialogue1;
    //public Dialogue dialogue2;
    private Coroutine talk;
    private bool started = false;
    public AudioManager audioManager;

    //public GameObject mapCollectable13;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            talk = StartCoroutine(Talk());
            //whoAreYouCanvas.SetActive(true);
            //Debug.Log(name);
            //SimpleEventManager.StartListening("StartQuest", StartQuest);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (started)
            {
                AudioSource background=audioManager.GetSound("Background").source;
                background.Play();
                //AudioSource dialoguebackground = audioManager.GetSound("DialogueBackground").source;
                //dialoguebackground.Stop();
                //audioManager.Play("Background");
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
                    TriggerDialogue1();
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
    
    public void TriggerDialogue1()
    {
        //Debug.Log("Triggero fine quest");
        SimpleEventManager.TriggerEvent("AbenthyTalk");
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue1);
    }
}
