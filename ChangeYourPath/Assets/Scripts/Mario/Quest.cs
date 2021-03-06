using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isActive;
    public bool isComplete = false;

    private bool one;
    private bool two;
    private bool three;
    private bool four;

    //attributes useful to keep the status of the quest

    public QuestGoal goal;
    public GameObject whereNpcIs;

    public void Complete()
    {
        isActive = false;
        isComplete = true;
    }

    public void checkQuestCondition(MapMovement mapMovement, string s)
    {
        mapMovement.matchingAllSides(mapMovement.movePoint);
        //Debug.Log("Hello"+s);
        int match = 0;
        if (mapMovement.getIsMatchingDown() & mapMovement.GetComponent<MapFeatures>().tileMap.getDown() == "Forest")
        {
            //Debug.Log("Missione sotto completa pezzo: "+s);
            match++;
        }
            
        if (mapMovement.getIsMatchingLeft() & mapMovement.GetComponent<MapFeatures>().tileMap.getLeft() == "Forest")
        {
            //Debug.Log("Missione sinistra completa pezzo: "+s);
            match++;
        }
        if (mapMovement.getIsMatchingRight() & mapMovement.GetComponent<MapFeatures>().tileMap.getRight() == "Forest"){
            //Debug.Log("Missione destra completa pezzo: "+s);
            match++;
        } 
        if (mapMovement.getIsMatchingUp() & mapMovement.GetComponent<MapFeatures>().tileMap.getUp() == "Forest")
        {
            //Debug.Log("Missione sopra completa pezzo: "+s);
            match++;
        }

        if (s == "MapForestQuest1")
        {

            if (match >= 2)
            {
                one = true;
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
            }
            else
            {
                four = false;
            }

        }

        if (one & two & three & four)
        {
            Vector3 from = whereNpcIs.transform.position;
            Vector3 dest1 = GameObject.Find("MapForestQuest1").transform.position;
            Vector3 dest2 = GameObject.Find("MapForestQuest2").transform.position;
            Vector3 dest3 = GameObject.Find("MapForestQuest3").transform.position;
            Vector3 dest4 = GameObject.Find("MapForestQuest4").transform.position;

            if (ExistVerticalPathDown(from, dest1) ||
                ExistVerticalPathDown(from, dest2) ||
                ExistVerticalPathDown(from, dest3) ||
                ExistVerticalPathDown(from, dest4))
            {
                Debug.Log("MISIION COMPLETE");
                Complete();

            }
            
        }
    }

    public bool ExistVerticalPathDown (Vector3 from, Vector3 dest)
    {
        Collider2D mapCollider = null;
        while(from != dest)
        {
            mapCollider = Physics2D.OverlapCircle(from, .2f, LayerMask.GetMask("Map"));
            if (mapCollider == null) return false;
            from.y -= 18;
        }
        return true;
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