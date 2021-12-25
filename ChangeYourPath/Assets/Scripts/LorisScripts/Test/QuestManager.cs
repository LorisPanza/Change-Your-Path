using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public NPC wilem;
    

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
            Debug.Log("Ho posizionato e controllo");
            SimpleEventManager.TriggerEvent("CircleQuest");
        }
    }
}
