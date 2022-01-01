using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignQuest : MonoBehaviour
{
    public bool isActive;
    public bool isCompleted;
    public MapMovement[] maps;
    public Transform startingPos;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SimpleEventManager.StartListening("SignQuest",activateQuest);
        SimpleEventManager.StartListening("CheckSignQuest",checkCondition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateQuest()
    {
        if (!isCompleted && !isActive)
        {
            Debug.Log("Quest sign attivata");
            isActive = true;
            SimpleEventManager.StopListening("SignQuest",activateQuest);
        }
    }

    public void checkCondition()
    {
        int counter = 0;
        int x = (int)startingPos.position.x;
        Debug.Log("Starting Tile x:"+x);
        for (int i = x; i >= x - 72; i = i - 18)
        {
            Vector2 newVec = new Vector2(i, startingPos.position.y);
            Collider2D detectdeMap=Physics2D.OverlapCircle(newVec, .2f,LayerMask.GetMask("Map"));
            if (detectdeMap)
            {
                MapFeatures mf = detectdeMap.GetComponent<MapFeatures>();
                if (mf.tileMap.getRight() == "Road" && mf.tileMap.getLeft() == "Road" && mf.tileMap.getUp()!="Road" && mf.tileMap.getDown()!="Road")
                {
                  Debug.Log("Trovato sulla destra un pezzo che rispetta la condizione");
                  counter++;
                }

            }
        }

        if (counter == 4)
        {
            Debug.Log("Quest compeltata");
            isCompleted = true;
            isActive = false;
            SimpleEventManager.StopListening("CheckSignQuest",checkCondition);
        }
    }
}
