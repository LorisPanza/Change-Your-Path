using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public NPC wilem;
    //public GameObject suggestion;
    public ActivateSuggestion activateSuggestion;
    

    public CircleQuestConditions cqq;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkActiveQuests()
    {
        if (!wilem.quest.isComplete)
        {
            SimpleEventManager.TriggerEvent("NorthForest");
        }

        if (cqq.getIsactive())
        {
            //Debug.Log("Ho posizionato e controllo");
            SimpleEventManager.TriggerEvent("CircleQuest");
        }
    }

    public void activateSuggestions()
    {

        if (!wilem.quest.isComplete && wilem.quest.isActive)
        {
            //suggestion.SetActive(true);
            //Debug.Log("Attivo i suggerimenti");
            activateSuggestion.activateWilemSuggestion();
        }

        if (!cqq.getIscompleted() && cqq.getIsactive())
        {
            activateSuggestion.activateElderSuggestion();
        }

    }
    
    public void deactivateSuggestions()
    {
        //suggestion.SetActive(false);
        //Debug.Log("Disattivo i suggerimenti");
        activateSuggestion.disactivateWilemSuggestion();
        activateSuggestion.disactivateElderSuggestion();

    }
    
    
}
