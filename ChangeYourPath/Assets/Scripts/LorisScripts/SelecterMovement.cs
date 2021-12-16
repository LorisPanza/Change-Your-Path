using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecterMovement : MonoBehaviour
{
    public float moveSpeed = 100f;
    public LayerMask detectedLayerMap;
    public LayerMask detectedLayerPlayer;
    private int offsetMovement=18;
    private bool choosen = false,isChild=false;
    private Collider2D chosenMapCollider=null,movementCollider=null,playerCollider=null; //colliderMatchingUp,colliderMatchingDown,colliderMatchingRight,colliderMatchingLeft;
    public Transform movePoint;
    public AudioManager audioManager;
    public NPC wilem;
    public GameObject grabCanvas;
    private GameObject grid;
    
    
    void Start()
    {
        movePoint.parent = null;
    }

    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && choosen == false) 
        {
            if (grabCanvas.activeSelf == true) grabCanvas.SetActive(false);
            chosenMapCollider = Physics2D.OverlapCircle(movePoint.position, .2f, detectedLayerMap);
            
            if (chosenMapCollider)
            {

                GameObject go = chosenMapCollider.gameObject;

                 grid = go.transform.parent.gameObject;
                go.transform.SetParent(this.transform);
                go.GetComponent<MapMovement>().enabled = true;
                //go.GetComponent<MapFeatures>().enabled = true;
                choosen = true;

                this.GetComponent<Renderer>().material.color = Color.magenta;

                playerCollider = checkPlayer();
                if (playerCollider)
                {
                    go.GetComponent<MapFeatures>().player = playerCollider.gameObject;
                    playerCollider.gameObject.transform.SetParent(go.transform);
                    isChild = true;
                }
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
                go.transform.SetParent(grid.transform);
                go.GetComponent<MapMovement>().enabled = false;
                //chosenMapCollider.gameObject.GetComponent<MapFeatures>().enabled = false;

                choosen = false;

                this.GetComponent<Renderer>().material.color = Color.white;

                if (isChild == true)
                {
                    go.GetComponent<MapFeatures>().player = null;
                    playerCollider.gameObject.transform.SetParent(null);
                    isChild = false;
                }
                if (!wilem.quest.isComplete) SimpleEventManager.TriggerEvent("NorthForest");
            } 
            else
            {
                audioManager.Play("mapDenied");
            
            }
           
            
        }
        

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            
            if (!choosen)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * offsetMovement, 0f, 0f);

                }

                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * offsetMovement, 0f);
                }
            }
            if (choosen)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    
                    movementCollider = Physics2D.OverlapCircle(
                        movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * offsetMovement, 0f, 0f), .2f, detectedLayerMap);
                    if (!movementCollider)
                    {
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * offsetMovement, 0f, 0f);
                       
                    }
                    audioManager.Play("mapMovement");
               
                } 
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    
                    movementCollider = Physics2D.OverlapCircle(
                        movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * offsetMovement, 0f) , .2f, detectedLayerMap);
                    if (!movementCollider)
                    {
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * offsetMovement, 0f);
                       
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
}


