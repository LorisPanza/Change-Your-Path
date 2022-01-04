using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignQuest : MonoBehaviour
{
    public bool isActive;
    public bool isCompleted;
    public bool up, down, right, left;
    public AudioManager audioManager;
    public MapFeatures startingMap;
    public Transform startingPos;
    public GameObject newMap;
    
    
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
            AudioSource audioSource= audioManager.GetSound("Background").source;
            audioSource.Stop();
            
            audioManager.Play("SignQuest");
            isActive = true;
            SimpleEventManager.StopListening("SignQuest",activateQuest);
        }
    }

    public void checkCondition()
    {
        up = false;
        down = false;
        right = false;
        left = false;
        int counter = 0;
        int x = (int)startingPos.position.x;
        int y = (int)startingPos.position.y;
        float z = startingPos.rotation.z;
        Debug.Log("Starting Tile x:"+x);
        Debug.Log("Starting Tile z:"+z);

        if (startingMap.tileMap.getLeft()=="Road" && 
            Physics2D.OverlapCircle(new Vector2(x-72,y), .2f,LayerMask.GetMask("Map"))==null)
        {
            left = true;
            for (int i = x; i >= x - 54; i = i - 18)
            {
                Vector2 newVec = new Vector2(i, startingPos.position.y);
                Collider2D detectdeMap=Physics2D.OverlapCircle(newVec, .2f,LayerMask.GetMask("Map"));
                if (detectdeMap)
                {
                    MapFeatures mf = detectdeMap.GetComponent<MapFeatures>();
                    if (mf.tileMap.getRight() == "Road" && mf.tileMap.getLeft() == "Road" && mf.tileMap.getUp()!="Road" && mf.tileMap.getDown()!="Road")
                    {
                        Debug.Log("Trovato sulla sinistra un pezzo che rispetta la condizione");
                        counter++;
                    }

                }
            }
       
        }
        else if (startingMap.tileMap.getRight()=="Road" && 
                 Physics2D.OverlapCircle(new Vector2(x+72,y), .2f,LayerMask.GetMask("Map"))==null)
        {
            right = true;
            for (int i = x; i <= x + 54; i = i + 18)
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
        }
        else if (startingMap.tileMap.getUp()=="Road" && 
                 Physics2D.OverlapCircle(new Vector2(x,y+72), .2f,LayerMask.GetMask("Map"))==null)
        {
            up = true;
            for (int i = y; i <= y + 54; i = i + 18)
            {
                Vector2 newVec = new Vector2(startingPos.position.x, i);
                Collider2D detectdeMap=Physics2D.OverlapCircle(newVec, .2f,LayerMask.GetMask("Map"));
                if (detectdeMap)
                {
                    MapFeatures mf = detectdeMap.GetComponent<MapFeatures>();
                    if (mf.tileMap.getUp() == "Road" && mf.tileMap.getDown() == "Road" && mf.tileMap.getRight()!="Road" && mf.tileMap.getLeft()!="Road")
                    {
                        Debug.Log("Trovato sopra un pezzo che rispetta la condizione");
                        counter++;
                    }

                }
            }
            
        }
        
        else if (startingMap.tileMap.getDown()=="Road" && 
                 Physics2D.OverlapCircle(new Vector2(x,y-72), .2f,LayerMask.GetMask("Map"))==null)
        {
            down = true;
            for (int i = y; i >= y - 54; i = i - 18)
            {
                Vector2 newVec = new Vector2(startingPos.position.x, i);
                Collider2D detectdeMap=Physics2D.OverlapCircle(newVec, .2f,LayerMask.GetMask("Map"));
                if (detectdeMap)
                {
                    MapFeatures mf = detectdeMap.GetComponent<MapFeatures>();
                    if (mf.tileMap.getUp() == "Road" && mf.tileMap.getDown() == "Road" && mf.tileMap.getRight()!="Road" && mf.tileMap.getLeft()!="Road")
                    {
                        Debug.Log("Trovato sotto un pezzo che rispetta la condizione");
                        counter++;
                    }

                }
            }
        }

        if (counter == 3)
        {
            Debug.Log("Quest compeltata");
            isCompleted = true;
            isActive = false;
            insertNewMap();
            SimpleEventManager.StopListening("CheckSignQuest",checkCondition);
            SimpleEventManager.TriggerEvent("SignQuestCompleted");
            audioManager.Play("QuestCompleted");
            AudioSource audioSource= audioManager.GetSound("SignQuest").source;
            audioSource.Stop();
            audioManager.Play("Background");
        }
    }

    public void insertNewMap()
    {
        if (left)
        {
            GameObject.Find("Selecter").transform.position = new Vector3(startingPos.position.x-72,startingPos.position.y,startingPos.position.z);
        }
        else if (right)
        { 
            GameObject.Find("Selecter").transform.position = new Vector3(startingPos.position.x+72,startingPos.position.y,startingPos.position.z);
        }
        else if (up)
        {
            GameObject.Find("Selecter").transform.position = new Vector3(startingPos.position.x,startingPos.position.y+72,startingPos.position.z);
        }
        else if (down)
        {
            GameObject.Find("Selecter").transform.position = new Vector3(startingPos.position.x,startingPos.position.y-72,startingPos.position.z);
        }
    }
}
