using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;
    //probably here references to tha map pieces involved
    
    public bool IsReached()
    {
        // check the condition to pass the quest
        return true;
    }

    public void MapPiecePushed()
    {
        if (goalType == GoalType.MapPuzzle)
        {
            //here you od something to keep the status of the quest.
            // for example: there should be putted 3 pieces in line one after the other.
            //here you keep the partial status
        }
    }

}

public enum GoalType
{
    MapPuzzle,
    Others
}