using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElderNPC : MonoBehaviour
{
    
    public Dialogue dialogue1;
    public Dialogue dialogue2;
    private Coroutine talk;
    private bool started = false;
    public AudioManager audioManager;
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
            //whoAreYouCanvas.SetActive(false);
            StopCoroutine(talk);
            started = false;
            FindObjectOfType<DialogueManager>().HideBox();

            
            AudioSource dialoguebackground = audioManager.GetSound("DialogueBackground").source;
            dialoguebackground.Stop();
            audioManager.Play("Background");
            AudioSource voiceSrc = audioManager.GetSound("Voice").source;
            voiceSrc.Stop();
        }
    }
    
    
    IEnumerator Talk()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
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
        Debug.Log("Triggero fine quest");
        SimpleEventManager.TriggerEvent("EndElderQuest");
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue1);
    }
}
