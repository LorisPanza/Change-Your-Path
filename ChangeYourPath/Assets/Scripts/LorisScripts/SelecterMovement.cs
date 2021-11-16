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
    //private MapFeatures collideMap,thisMap;
    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && choosen == false)
        {
            chosenMapCollider = enableSelectionMapCollider();
            if (chosenMapCollider)
            {
                GameObject go = chosenMapCollider.gameObject;
                go.GetComponent<MapMovement>().enabled = true;
                go.GetComponent<MapFeatures>().enabled = true;
                choosen = true;
                
                playerCollider = checkPlayer();
                if (playerCollider)
                {
                    go.GetComponent<MapFeatures>().player = playerCollider.gameObject;
                    playerCollider.gameObject.transform.SetParent(go.transform);
                    isChild = true;
                }
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.Space) && choosen == true)
        {
            
            if (disableSelectionMapCondition())
            {
                chosenMapCollider.gameObject.GetComponent<MapMovement>().enabled = false;
                chosenMapCollider.gameObject.GetComponent<MapFeatures>().enabled = false;
            
                choosen = false;
                
                if (isChild == true)
                {
                    playerCollider.gameObject.transform.SetParent(null);
                }
            }
            else
            {
                Debug.Log("Non puoi metterlo");
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
               
                } 
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    
                    movementCollider = Physics2D.OverlapCircle(
                        movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * offsetMovement, 0f) , .2f, detectedLayerMap);
                    if (!movementCollider)
                    {
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * offsetMovement, 0f);
                       
                    }
                
                }
            }
            
        }
    }


    public Collider2D enableSelectionMapCollider()
    {
        Collider2D mapColliderCopy = Physics2D.OverlapCircle(movePoint.position, .2f, detectedLayerMap);
        return mapColliderCopy;
    }

    public bool disableSelectionMapCondition()
    {
        GameObject detectedMap;
        MapMovement chosenMapMov = chosenMapCollider.gameObject.GetComponent<MapMovement>();
        if (chosenMapMov.matchingAllSides(chosenMapMov.movePoint))
        {
            if (chosenMapMov.getIsMatchingLeft())
            {
                GameObject leftDetectedMap=chosenMapMov.matchingLeft(chosenMapMov.movePoint);
                if (leftDetectedMap != null)
                { 
                    disableLeftBoundaryColliders(leftDetectedMap);
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
    
    public void disableLeftBoundaryColliders(GameObject leftMap)
    {
        GameObject thisMap=chosenMapCollider.gameObject;
        GameObject leftBoundary = thisMap.transform.Find("LeftBoundary").gameObject;
        //GameObject rightBoundary = leftMap.transform.Find(("RightBoundary")).gameObject;
        leftBoundary.SetActive(false);
        //rightBoundary.SetActive(false);
    }
}


