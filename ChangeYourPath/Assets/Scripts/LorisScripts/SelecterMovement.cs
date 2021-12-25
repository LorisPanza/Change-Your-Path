using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SelecterMovement : MonoBehaviour
{
    //public LayerMask[] obstacles;
    public float moveSpeed = 100f;
    public LayerMask detectedLayerMap;
    public LayerMask detectedLayerPlayer;
    public LayerMask detectedLayerLimit;
    private int offsetMovement=18;
    private bool choosen = false,isChild=false;
    private Collider2D chosenMapCollider=null,movementCollider=null,playerCollider=null,limitCollider=null; //colliderMatchingUp,colliderMatchingDown,colliderMatchingRight,colliderMatchingLeft;
    public Transform movePoint;
    public AudioManager audioManager;
    //public NPC wilem;
    public QuestManager questManager;
    private GameObject grid;
    private float sideLength=8f;
    private bool errorFlag = false;
    
    
    void Start()
    {
        movePoint.parent = null;
    }

    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && choosen == false && this.transform.position.x%offsetMovement==0 
           && this.transform.position.y%offsetMovement==0) 
        {
            chosenMapCollider = Physics2D.OverlapCircle(movePoint.position, .2f, detectedLayerMap);
            
            
            
            
            if (chosenMapCollider)
            {

                GameObject go = chosenMapCollider.gameObject;

                grid = go.transform.parent.gameObject;
                go.transform.SetParent(this.transform);
                go.GetComponent<MapMovement>().enabled = true;
                //go.GetComponent<MapFeatures>().enabled = true;
                choosen = true;

                //this.GetComponent<Renderer>().material.color = Color.magenta;

                playerCollider = checkPlayer();
                if (playerCollider)
                {
                    go.GetComponent<MapFeatures>().player = playerCollider.gameObject;
                    playerCollider.gameObject.transform.SetParent(go.transform);
                    go.GetComponent<MapMovement>().setPlayerInside(playerCollider.gameObject);
                    isChild = true;
                }
                GrabColorSelecter(new Vector3(0,0,0),Color.yellow, this.GetComponent<Tilemap>());
                audioManager.Play("mapSelection");
                enableSelectionMapCondition();
            }

        }
        else if (Input.GetKeyDown(KeyCode.Space) && choosen == true)
        {
            GameObject go = chosenMapCollider.gameObject;
            if (disableSelectionMapCondition() && go.transform.position.x%offsetMovement==0 && go.transform.position.y%offsetMovement==0)
            {
                
                audioManager.Play("mapChoice");
                GrabColorSelecter(new Vector3(0,0,0),Color.white, this.GetComponent<Tilemap>());
                go.transform.SetParent(grid.transform);
                go.GetComponent<MapMovement>().enabled = false;
                //chosenMapCollider.gameObject.GetComponent<MapFeatures>().enabled = false;

                choosen = false;

                this.GetComponent<Renderer>().material.color = Color.white;

                if (isChild == true)
                {
                    go.GetComponent<MapFeatures>().player = null;
                    playerCollider.gameObject.transform.SetParent(null);
                    go.GetComponent<MapMovement>().setPlayerInside(null);
                    isChild = false;
                }
                //if (!wilem.quest.isComplete) SimpleEventManager.TriggerEvent("NorthForest");
                questManager.checkActiveQuests();
            } 
            else
            {
                audioManager.Play("mapDenied");
            
            }
           
            
        }
        
        if((Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.M)) && choosen==true && errorFlag==true)
        {
            GrabColorSelecter(new Vector3(0,0,0),Color.white,this.GetComponent<Tilemap>());
        }
       
        

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            
            if (!choosen)
            {

                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    
                    movementCollider = Physics2D.OverlapCircle(
                        movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * offsetMovement, 0f, 0f), .2f,detectedLayerLimit);
                    
                    if (!movementCollider)
                    {
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * offsetMovement, 0f, 0f);
                        
                    }
                    
                }
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    movementCollider = Physics2D.OverlapCircle(
                        movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * offsetMovement, 0f) , .2f, detectedLayerLimit);
                    
                    if (!movementCollider)
                    {
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * offsetMovement, 0f);
                    }
                    
                }
            }
            if (choosen)
            {
                
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    
                    movementCollider = Physics2D.OverlapCircle(
                        movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * offsetMovement, 0f, 0f), .2f,detectedLayerMap);
                    
                    limitCollider = Physics2D.OverlapCircle(
                        movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * offsetMovement, 0f, 0f), .2f,detectedLayerLimit);
                    
                    if (!movementCollider && !limitCollider)
                    {
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * offsetMovement, 0f, 0f);
                       
                    }
                    
                    if (errorFlag)
                    {
                        GrabColorSelecter(new Vector3(0,0,0),Color.yellow, this.GetComponent<Tilemap>());
                        errorFlag = false;
                    }
                
                    audioManager.Play("mapMovement");
               
                } 
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    
                    movementCollider = Physics2D.OverlapCircle(
                        movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * offsetMovement, 0f) , .2f, detectedLayerMap);
                    limitCollider = Physics2D.OverlapCircle(
                        movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * offsetMovement, 0f) , .2f, detectedLayerLimit);
                    
                    if (!movementCollider && !limitCollider)
                    {
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * offsetMovement, 0f);
                       
                    }   
                    
                    if (errorFlag)
                    {
                        GrabColorSelecter(new Vector3(0,0,0),Color.yellow, this.GetComponent<Tilemap>());
                        errorFlag = false;
                    }
                    audioManager.Play("mapMovement");
                
                }
            }

            
        }
    }


    public void enableSelectionMapCondition()
    {
        MapMovement chosenMapMov = chosenMapCollider.gameObject.GetComponent<MapMovement>();
        
        chosenMapMov.matchingAllSides(chosenMapMov.movePoint);
        
        if (chosenMapMov.getIsMatchingLeft())
        {
            GameObject leftDetectedMap=chosenMapMov.matchingLeft(chosenMapMov.movePoint);
            if (leftDetectedMap != null)
            { 
                modifyLeftBoundaryColliders(leftDetectedMap,true);
            }
                
        }
        

        if (chosenMapMov.getIsMatchingRight())
        {
            
            GameObject rightDetectedMap = chosenMapMov.matchingRight(chosenMapMov.movePoint);
            if (rightDetectedMap!=null)
            {
                modifyRightBoundaryColliders(rightDetectedMap,true);
            }
        }

        if (chosenMapMov.getIsMatchingUp())
        {
            GameObject upDetectedMap = chosenMapMov.matchingUp(chosenMapMov.movePoint);
            if (upDetectedMap != null)
            {
                modifyUpBoundaryColliders(upDetectedMap,true);
            }
        }
            
        if (chosenMapMov.getIsMatchingDown())
        {
            GameObject downDetectedMap = chosenMapMov.matchingDown(chosenMapMov.movePoint);
            if (downDetectedMap != null)
            {
                modifyDownBoundaryColliders(downDetectedMap,true);
            }
        }
    }

    public bool disableSelectionMapCondition()
    {
        MapMovement chosenMapMov = chosenMapCollider.gameObject.GetComponent<MapMovement>();
        if (chosenMapMov.matchingAllSides(chosenMapMov.movePoint))
        {
            if (chosenMapMov.getIsMatchingLeft())
            {
                GameObject leftDetectedMap=chosenMapMov.matchingLeft(chosenMapMov.movePoint);
                if (leftDetectedMap != null)
                { 
                    modifyLeftBoundaryColliders(leftDetectedMap,false);
                }
                
            }
            

            if (chosenMapMov.getIsMatchingRight())
            {
                GameObject rightDetectedMap = chosenMapMov.matchingRight(chosenMapMov.movePoint);
                if (rightDetectedMap!=null)
                {
                    modifyRightBoundaryColliders(rightDetectedMap,false);
                }
            }
            

            if (chosenMapMov.getIsMatchingUp())
            {
                GameObject upDetectedMap = chosenMapMov.matchingUp(chosenMapMov.movePoint);
                if (upDetectedMap != null)
                {
                    modifyUpBoundaryColliders(upDetectedMap,false);
                }
            }
            
            
            if (chosenMapMov.getIsMatchingDown())
            {
                GameObject downDetectedMap = chosenMapMov.matchingDown(chosenMapMov.movePoint);
                if (downDetectedMap != null)
                {
                    modifyDownBoundaryColliders(downDetectedMap,false);
                }
            }
          
            return true;
        }
        else
        {
            errorFlag = true;
            if (!chosenMapMov.getIsMatchingRight() && !chosenMapMov.isVoidRight())
            {
                ErrorColorSelecter(new Vector3(0,0,0),Color.red,this.GetComponent<Tilemap>(),"Right");
            }
            
            if (!chosenMapMov.getIsMatchingLeft() && !chosenMapMov.isVoidLeft())
            {
                ErrorColorSelecter(new Vector3(0,0,0),Color.red,this.GetComponent<Tilemap>(),"Left");
            }
            
            if (!chosenMapMov.getIsMatchingUp() && !chosenMapMov.isVoidUp())
            {
                ErrorColorSelecter(new Vector3(0,0,0),Color.red,this.GetComponent<Tilemap>(),"Up");
            }
            
            if (!chosenMapMov.getIsMatchingDown() && !chosenMapMov.isVoidDown())
            {
                ErrorColorSelecter(new Vector3(0,0,0),Color.red,this.GetComponent<Tilemap>(),"Down");
            }
            return false;
        }

        

    }

    public Collider2D checkPlayer()
    {
        return Physics2D.OverlapBox(movePoint.position, new Vector2(offsetMovement, offsetMovement), 0, detectedLayerPlayer);
    
    }
    
    public void modifyLeftBoundaryColliders(GameObject leftMap,bool activate)
    {
        GameObject thisMap=chosenMapCollider.gameObject;
        GameObject leftBoundary = thisMap.transform.Find("LeftBoundary").gameObject;
        GameObject rightBoundary = leftMap.transform.Find(("RightBoundary")).gameObject;
        if (activate)
        {
            leftBoundary.SetActive(true);
            rightBoundary.SetActive(true); 
        }
        else
        {
            leftBoundary.SetActive(false);
            rightBoundary.SetActive(false);
        }
    }

    public void modifyRightBoundaryColliders(GameObject rightMap,bool activate)
    {
        GameObject thisMap=chosenMapCollider.gameObject;
        GameObject rightBoundary = thisMap.transform.Find("RightBoundary").gameObject;
        GameObject leftBoundary = rightMap.transform.Find(("LeftBoundary")).gameObject;
        if (activate)
        {
            leftBoundary.SetActive(true);
            rightBoundary.SetActive(true);
        }
        else
        {
            leftBoundary.SetActive(false);
            rightBoundary.SetActive(false);
        }
        
    }
    
    public void modifyUpBoundaryColliders(GameObject upMap,bool activate)
    {
        GameObject thisMap=chosenMapCollider.gameObject;
        GameObject upBoundary = thisMap.transform.Find("UpBoundary").gameObject;
        GameObject downBoundary = upMap.transform.Find(("DownBoundary")).gameObject;
        if (activate)
        {
            upBoundary.SetActive(true);
            downBoundary.SetActive(true);

        }
        else
        {
            upBoundary.SetActive(false);
            downBoundary.SetActive(false);
    
        }
    }
    
    public void modifyDownBoundaryColliders(GameObject downMap,bool activate)
    {
        GameObject thisMap=chosenMapCollider.gameObject;
        GameObject downBoundary = thisMap.transform.Find("DownBoundary").gameObject;
        GameObject upBoundary = downMap.transform.Find(("UpBoundary")).gameObject;
        if (activate)
        {
            upBoundary.SetActive(true);
            downBoundary.SetActive(true);

        }
        else
        {
            upBoundary.SetActive(false);
            downBoundary.SetActive(false);
    
        }
    }

    public bool getChoosen()
    {
        return choosen;
    }

    public void disactiveChoosenMap()
    {
        if (chosenMapCollider != null)
        {
            chosenMapCollider.gameObject.GetComponent<MapMovement>().enabled=false;
        }
    }
    
    public void activeChoosenMap()
    {
        if (chosenMapCollider != null)
        {
            chosenMapCollider.gameObject.GetComponent<MapMovement>().enabled=true;
        }
    }
    
    
    
    public void GrabColorSelecter(Vector3 origin, Color c,Tilemap tilemap)
    {

        int rightX = (int)(origin.x + sideLength);
        int leftX = (int)(origin.x - sideLength)-1;
        int upY = (int) (origin.y + sideLength);
        int downY = (int) (origin.y - sideLength)-1;
        


        for (int i = leftX; i <= rightX;  i++)
        {
            tilemap.SetColor(new Vector3Int(i,upY,(int)origin.z),c);
            tilemap.SetColor(new Vector3Int(i,downY,(int)origin.z),c);
        }
        for (int i = downY; i <= upY;  i++)
        {
            tilemap.SetColor(new Vector3Int(rightX,i,(int)origin.z),c);
            tilemap.SetColor(new Vector3Int(leftX,i,(int)origin.z),c);
        }
    }
    
    public void ErrorColorSelecter(Vector3 origin, Color c,Tilemap tilemap,String side)
    {

        int rightX = (int)(origin.x + sideLength);
        int leftX = (int)(origin.x - sideLength)-1;
        int upY = (int) (origin.y + sideLength);
        int downY = (int) (origin.y - sideLength)-1;

        if (side == "Right")
        {
            for (int i = downY; i <= upY;  i++)
                tilemap.SetColor(new Vector3Int(rightX,i,(int)origin.z),c);
        }
        
        if (side == "Left")
        {
            for (int i = downY; i <= upY;  i++)
                tilemap.SetColor(new Vector3Int(leftX,i,(int)origin.z),c);
        }

        if (side == "Up")
        {
            for (int i = leftX; i <= rightX;  i++)
                tilemap.SetColor(new Vector3Int(i,upY,(int)origin.z),c);
                
        }

        if (side == "Down")
        {
            for (int i = leftX; i <= rightX;  i++)
                tilemap.SetColor(new Vector3Int(i,downY,(int)origin.z),c);
        }

        
    }
}


