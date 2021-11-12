using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MapMovement : MonoBehaviour
{
    public float moveSpeed = 100f;
    public LayerMask detectedLayer;
    private int offsetMovement=18;
    private int offsetMatching = 18;
    private Collider2D colliderMovement,colliderMatchingUp,colliderMatchingDown,colliderMatchingRight,colliderMatchingLeft;
    public Transform movePoint;
    private MapFeatures collideMap,thisMap;
    
    
    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.N))
        {
          rotateClockwise();
          checkMatching(movePoint);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            rotateCounterClockwise();
            checkMatching(movePoint);
        }
        
        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            
            
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                //Debug.Log("Muovo orizzontale");
                colliderMovement = Physics2D.OverlapCircle(
                    movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * offsetMovement, 0f, 0f), .2f, detectedLayer);
                if (!colliderMovement)
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * offsetMovement, 0f, 0f);
                    checkMatching(movePoint);
                }
               
            } 
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                //Debug.Log("Muovo Verticale");
                colliderMovement = Physics2D.OverlapCircle(
                    movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * offsetMovement, 0f) , .2f, detectedLayer);
                if (!colliderMovement)
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * offsetMovement, 0f);
                    checkMatching(movePoint);
                }
                
            }
            
            
        }
    }
    

    public bool checkMatching(Transform movePointCopy)
    {
        bool IsMatching = true;
        // right
        colliderMatchingRight = Physics2D.OverlapCircle(
            movePointCopy.position + new Vector3( offsetMatching, 0f, 0f), .2f, detectedLayer);
        //left
        colliderMatchingLeft = Physics2D.OverlapCircle(
            movePointCopy.position + new Vector3( -offsetMatching, 0f, 0f), .2f, detectedLayer);
        //up
        colliderMatchingUp = Physics2D.OverlapCircle(
            movePointCopy.position + new Vector3(0,offsetMatching, 0f),.2f,detectedLayer);
        //down
        colliderMatchingDown = Physics2D.OverlapCircle(
            movePointCopy.position + new Vector3(0,-offsetMatching, 0f),.2f,detectedLayer);
        
        

        if (colliderMatchingRight)
        {
            collideMap=colliderMatchingRight.gameObject.GetComponent<MapFeatures>();
            if (collideMap.gameObject.name != this.name)
            {
                thisMap=this.GetComponent<MapFeatures>();
                if (collideMap.tileMap.getLeft() == thisMap.tileMap.getRight())
                {
                    Debug.Log("Matching"+thisMap.tileMap.getRight()+"On the right");
                }
                else
                {
                    Debug.Log("No matching type on the right");
                    IsMatching = false;
                }
            }
            
        }
        
        if (colliderMatchingLeft)
        {
            collideMap=colliderMatchingLeft.gameObject.GetComponent<MapFeatures>();
            if (collideMap.gameObject.name != this.name)
            {
                thisMap=this.GetComponent<MapFeatures>();
                if (collideMap.tileMap.getRight() == thisMap.tileMap.getLeft())
                {
                    Debug.Log("Matching"+thisMap.tileMap.getLeft()+"On the left");
                }
                else
                {
                    Debug.Log("No matching type on the left");
                    IsMatching = false;
                } 
            }
            
        }
        
        if (colliderMatchingUp)
        {
            collideMap=colliderMatchingUp.gameObject.GetComponent<MapFeatures>();
            if (collideMap.gameObject.name != this.name)
            {
                thisMap=this.GetComponent<MapFeatures>();
                if (collideMap.tileMap.getDown() == thisMap.tileMap.getUp())
                {
                    Debug.Log("Matching"+thisMap.tileMap.getUp()+"Up");
                }
                else
                {
                    Debug.Log("No matching type up");
                    IsMatching = false;
                }  
            }
            
        }

        if (colliderMatchingDown)
        {
            collideMap = colliderMatchingDown.gameObject.GetComponent<MapFeatures>();
            if (collideMap.gameObject.name != this.name)
            {
                thisMap = this.GetComponent<MapFeatures>();
                if (collideMap.tileMap.getUp() == thisMap.tileMap.getDown())
                {
                    Debug.Log("Matching" + thisMap.tileMap.getDown() + "Down");
                }
                else
                {
                    Debug.Log("No matching type down");
                    IsMatching = false;
                }
            }
            
        }

        colliderMatchingUp = null;
        colliderMatchingLeft = null;
        colliderMatchingRight = null;
        colliderMatchingDown = null;
        return IsMatching;
    }
    
    public double calculateDistance(Vector3 v1, Vector3 v2)
    {
        Vector3 difference = new Vector3(v1.x - v2.x, v1.y - v2.y,0);
        double distance = Math.Sqrt(Math.Pow(difference.x, 2f) + Math.Pow(difference.y, 2f));
        return distance;
    }

    public void rotateClockwise()
    {
        transform.Rotate(0,0,-90);
        thisMap = this.GetComponent<MapFeatures>();
        thisMap.tileMap.clockwiseRotation();
    }

    public void rotateCounterClockwise()
    {
        transform.Rotate(0,0,90);
        thisMap = this.GetComponent<MapFeatures>();
        thisMap.tileMap.counterclockwiseRotation();
    }
    
    
    
}

