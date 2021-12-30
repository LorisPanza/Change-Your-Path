using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RobotQuest
{
    public bool isActive;
    public bool isComplete = false;

    //attributes useful to keep the status of the quest

    public void Complete()
    {
        isActive = false;
        isComplete = true;
    }


}
