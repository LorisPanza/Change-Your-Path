using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRobot : MonoBehaviour
{
    public Robot robot;

    // Start is called before the first frame update
    void Start()
    {
        if (!robot.robotQuest.isComplete)
        {
            robot.gameObject.SetActive(true);
        }
        else
        {
            robot.gameObject.SetActive(false);
        }
    }
}
