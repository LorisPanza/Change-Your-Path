using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool isQuestGiver;
    public Quest quest;
    public Player player;
    //public Map map; reference to the map piece involved in the quest
    public GameObject whoAreYouCanvas;
    public GameObject collectablemap;

    public Dialogue dialogue;
    private Coroutine talk;
    private bool started = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            talk = StartCoroutine(Talk());
            whoAreYouCanvas.SetActive(true);
            Debug.Log(name);
            SimpleEventManager.StartListening("StartQuest", StartQuest);
        }
    }

    void StartQuest()
    {
        if (isQuestGiver)
        {
            if (FindObjectOfType<DialogueManager>().DialogueEnded())
            {
                quest.isActive = true;
                if (GameObject.Find("MapPiece 6") == null) collectablemap.SetActive(true);
                SimpleEventManager.StopListening("StartQuest", StartQuest);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            whoAreYouCanvas.SetActive(false);
            StopCoroutine(talk);
            started = false;
            FindObjectOfType<DialogueManager>().HideBox();

        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    IEnumerator Talk()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!started)
                {
                    whoAreYouCanvas.SetActive(false);
                    TriggerDialogue();
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
