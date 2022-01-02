using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCollider : MonoBehaviour
{
    public Robot robot;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            if (robot.robotQuest.isActive) {
                robot.RestartMiniGame();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            if (robot.robotQuest.isActive) {
                robot.RestartMiniGame();
            }
        }
    }
}
