using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CircleQuestConditions : MonoBehaviour
{
    private bool isActive=false;
    private bool flag = false;

    private int satisfied;
    //public MapMovement map11;
    //public MapMovement map12;
    //public MapMovement map13;
    //public MapMovement map10;
    public MapMovement[] tilemaps;
    

    private bool isComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        SimpleEventManager.StartListening("Map13IsPositioned",activateQuest);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("MapPiece13") != null && flag==false)
        {
            Debug.Log("Pezzo 13 trovato");
            SimpleEventManager.TriggerEvent("Map13IsPositioned");
            flag = true;
        }
    }

    public void activateQuest()
    {
        isActive = true;
        Debug.Log("Quest Attivata");
        SimpleEventManager.StopListening("Map13IsPositioned",activateQuest);
    }

    public bool getIsactive()
    {
        return isActive;
    }

    public bool checkCondition()
    {
        int check;
        satisfied = 0;
        foreach(MapMovement map in tilemaps)
        {
            map.matchingAllSides(map.movePoint);
            check = 0;
            if(map.getIsMatchingDown() && map.GetComponent<MapFeatures>().tileMap.getDown()=="River")
            {
                Debug.Log(map.name+"down river");
                check++;
            }
            if(map.getIsMatchingUp() && map.GetComponent<MapFeatures>().tileMap.getUp()=="River")
            {
                Debug.Log(map.name+"up river");
                check++;
            }
            if(map.getIsMatchingRight() && map.GetComponent<MapFeatures>().tileMap.getRight()=="River")
            {
                Debug.Log(map.name+"destra river");
                check++;
            }
            if(map.getIsMatchingLeft() && map.GetComponent<MapFeatures>().tileMap.getLeft()=="River")
            {
                Debug.Log(map.name+"sinistra river");
                check++;
            }

            if (check == 2)
            {
                Debug.Log("Due lati corretti");
                satisfied++;
            }
        }

        if (satisfied == 4)
        {
            Debug.Log("Mission complete");
            return true;
        }
        else
        {
            return false;
        }

    }

    public void setIsActive(bool s)
    {
        isActive = s;
    }
}
