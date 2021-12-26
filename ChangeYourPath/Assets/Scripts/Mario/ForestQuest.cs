using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ForestQuest : MonoBehaviour
{
    public NPC npc;
    //public Tilemap mapPiece10;


    
    void Awake()
    {
        SimpleEventManager.StartListening("NorthForest", CheckIsActive);
    }

    void CheckIsActive()
    {
        if (npc.quest.isActive)
        {
            npc.quest.checkQuestCondition(this.GetComponent<MapMovement>(),this.name);
        }
        if (npc.quest.isComplete)
            SimpleEventManager.StopListening("NorthForest", CheckIsActive);
    }
}
