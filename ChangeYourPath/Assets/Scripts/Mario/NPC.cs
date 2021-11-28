using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool isQuestGiver;
    public Quest quest;
    public Player player;
    //public Map map; reference to the map piece involved in the quest

    public Dialogue dialogue;
    private Coroutine talk;
    private bool started = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            talk = StartCoroutine(Talk());
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
                SimpleEventManager.StopListening("StartQuest", StartQuest);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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
