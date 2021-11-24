using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isActive;

    private bool one;
    private bool two;
    private bool three;
    private bool four;

    //attributes useful to keep the status of the quest

    public QuestGoal goal;

    public void Complete()
    {
        isActive = false;
    }

    public void checkQuestCondition(MapMovement mapMovement, string s)
    {
        mapMovement.matchingAllSides(mapMovement.movePoint);
        Debug.Log("Hello"+s);
        int match = 0;
        if (mapMovement.getIsMatchingDown() & mapMovement.GetComponent<MapFeatures>().tileMap.getDown() == "Forest")
        {
            Debug.Log("Missione sotto completa pezzo: "+s);
            match++;
        }
            
        if (mapMovement.getIsMatchingLeft() & mapMovement.GetComponent<MapFeatures>().tileMap.getLeft() == "Forest")
        {
            Debug.Log("Missione sinistra completa pezzo: "+s);
            match++;
        }
        if (mapMovement.getIsMatchingRight() & mapMovement.GetComponent<MapFeatures>().tileMap.getRight() == "Forest"){
            Debug.Log("Missione destra completa pezzo: "+s);
            match++;
        } 
        if (mapMovement.getIsMatchingUp() & mapMovement.GetComponent<MapFeatures>().tileMap.getUp() == "Forest")
        {
            Debug.Log("Missione sopra completa pezzo: "+s);
            match++;
        }

        if (s == "MapForestQuest1")
        {

            if (match >= 2)
            {
                one = true;
                Debug.Log("MISIION COMPLETE 1");
            }
            else
            {
                one = false;
            }

        }

        if (s == "MapForestQuest2")
        {

            if (match >= 2)
            {
                two = true;
                Debug.Log("MISIION COMPLETE 2");
            }
            else
            {
                two = false;
            }

        }

        if (s == "MapForestQuest3")
        {

            if (match >= 2)
            {
                three = true;
                Debug.Log("MISIION COMPLETE 3");
            }
            else
            {
                three = false;
            }

        }

        if (s == "MapForestQuest4")
        {

            if (match >= 2)
            {
                four = true;
                Debug.Log("MISIION COMPLETE 4");
            }
            else
            {
                four = false;
            }

        }

        if (one & two & three & four) Debug.Log("MISIION COMPLETE");
    }




}




// inside map there should be something like this:
//references of the quest as attribute
//if (quest.isActive){
//    Quest.goal.method to modify the status to check the goal
//}

//It contain also:
//if (quest.goal.IsReached)
//{
//    Quest.Complete();
//}

//the method to chek the goal should be implemented