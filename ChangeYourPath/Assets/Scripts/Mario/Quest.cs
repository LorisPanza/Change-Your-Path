using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isActive;

    public string title;
    public string description;

    //attributes useful to keep the status of the quest

    public QuestGoal goal;

    public void Complete()
    {
        isActive = false;
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