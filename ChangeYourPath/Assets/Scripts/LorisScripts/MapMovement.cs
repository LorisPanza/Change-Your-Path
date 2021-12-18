using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MapMovement : MonoBehaviour
{
    public float moveSpeed = 100f;
    public LayerMask detectedLayer;
    private int offsetMovement = 18;
    private int offsetMatching = 18;
    private Collider2D colliderMovement;
    public Transform movePoint;
    private MapFeatures collideMap, thisMap;
    private bool isMatching, isMatchingRight, isMatchingLeft, isMatchingDown, isMatchingUp;
    public AudioManager audioManager;
    private GameObject player=null;


    // Start is called before the first frame update
    void Start()
    {
        //movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.N))
        {
            audioManager.Play("mapFlip");
            rotateClockwise();
            matchingAllSides(movePoint);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            audioManager.Play("mapFlip");
            rotateCounterClockwise();
            matchingAllSides(movePoint);
        }
/*
        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        //{


            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                //checkHorizontalMovement();
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                //checkVerticalMovement();
            }


        }
        
        */
    }


    public bool matchingAllSides(Transform movePointCopy)
    {
        isMatching = true;
        isMatchingDown = false;
        isMatchingRight = false;
        isMatchingUp = false;
        isMatchingLeft = false;
        matchingDown(movePointCopy);
        matchingLeft(movePointCopy);
        matchingRight(movePointCopy);
        matchingUp(movePointCopy);

        return isMatching;
    }

    public GameObject matchingRight(Transform movePointCopy)
    {

        GameObject mapObject;
        MapFeatures mapFeatures;
        Collider2D rightCollider = Physics2D.OverlapCircle(
            movePointCopy.position + new Vector3(offsetMatching, 0f, 0f), .2f, detectedLayer);
        if (rightCollider)
        {
            mapObject = rightCollider.gameObject;
            mapFeatures = mapObject.GetComponent<MapFeatures>();
            if (mapObject.name != this.name)
            {
                thisMap = this.GetComponent<MapFeatures>();
                if (mapFeatures.tileMap.getLeft() == thisMap.tileMap.getRight())
                {
                    isMatchingRight = true;
                    //Debug.Log("Matching" + thisMap.tileMap.getRight() + "On the right");
                    return mapObject;

                }
                else
                {
                    //Debug.Log("No matching type on the right: " + mapFeatures.tileMap.getLeft());
                    isMatching = false;
                    return null;
                }
            }
        }

        return null;
    }


    public GameObject matchingLeft(Transform movePointCopy)
    {
        
        GameObject mapObject;
        MapFeatures mapFeatures;
        Collider2D leftCollider = Physics2D.OverlapCircle(
            movePointCopy.position + new Vector3(-offsetMatching, 0, 0f), .2f, detectedLayer);
        if (leftCollider)
        {
            mapObject = leftCollider.gameObject;
            mapFeatures = mapObject.GetComponent<MapFeatures>();
            if (mapObject.name != this.name)
            {
                thisMap = this.GetComponent<MapFeatures>();
                if (mapFeatures.tileMap.getRight() == thisMap.tileMap.getLeft())
                {
                    isMatchingLeft = true;
                    //Debug.Log("Matching" + thisMap.tileMap.getLeft() + "On the left");
                    return mapObject;
                }
                else
                {
                    //Debug.Log("No matching type on the left:"+mapFeatures.tileMap.getRight());
                    isMatching = false;
                    return null;
                }
            }
        }

        return null;
    }

    public GameObject matchingUp(Transform movePointCopy)
    {
        
        GameObject mapObject;
        MapFeatures mapFeatures;
        Collider2D upCollider = Physics2D.OverlapCircle(
            movePointCopy.position + new Vector3(0f, offsetMatching, 0f), .2f, detectedLayer);
        if (upCollider)
        {
            mapObject = upCollider.gameObject;
            mapFeatures = mapObject.GetComponent<MapFeatures>();
            if (mapObject.name != this.name)
            {
                thisMap = this.GetComponent<MapFeatures>();
                if (mapFeatures.tileMap.getDown() == thisMap.tileMap.getUp())
                {
                    isMatchingUp = true;
                    //Debug.Log("Matching" + thisMap.tileMap.getUp() + "Up");
                    return mapObject;
                }
                else
                {
                    //Debug.Log("No matching type on the up:"+mapFeatures.tileMap.getDown());
                    isMatching = false;
                    return null;
                }
            }
        }
        return null;
    }

    public GameObject matchingDown(Transform movePointCopy)
    {
        //isMatching = true;
        GameObject mapObject;
        MapFeatures mapFeatures;
        Collider2D downCollider = Physics2D.OverlapCircle(
            movePointCopy.position + new Vector3(0f, -offsetMatching, 0f), .2f, detectedLayer);
        if (downCollider)
        {
            mapObject = downCollider.gameObject;
            mapFeatures = mapObject.GetComponent<MapFeatures>();
            if (mapObject.name != this.name)
            {
                thisMap = this.GetComponent<MapFeatures>();
                if (mapFeatures.tileMap.getUp() == thisMap.tileMap.getDown())
                {
                    isMatchingDown = true;
                    //Debug.Log("Matching" + thisMap.tileMap.getDown() + "Down");
                    return mapObject;
                }
                else
                {
                    //Debug.Log("No matching type down: "+mapFeatures.tileMap.getUp());
                    isMatching = false;
                    return null;
                }
            }
        }

        return null;
    }



    public void rotateClockwise()
    {
        if (player != null)
        {
            checkPositionPlayer(player);
            //player.transform.Rotate(0,0,-80);
        }
        transform.Rotate(0, 0, -90);
        thisMap = this.GetComponent<MapFeatures>();
        thisMap.tileMap.clockwiseRotation();
        thisMap.rotateSpriteClockwise();
        thisMap.rotateBoundaryClockwise();

        if (name == "MapPiece 6" || name == "MapPiece 8" || name == "MapPiece 7" || name == "MapPiece 9")
        {
            AdjustTreesClockwise();
            RotateTreesClockwise();
        }

    }

    public void rotateCounterClockwise()
    {
        if (player != null)
        {
            checkPositionPlayer(player);
        }
        transform.Rotate(0, 0, 90);
        thisMap = this.GetComponent<MapFeatures>();
        thisMap.tileMap.counterclockwiseRotation();
        thisMap.rotateSpriteCounterClockwise();
        thisMap.rotateBoundaryCounterClockwise();

        if (name == "MapPiece 6" || name == "MapPiece 8" || name == "MapPiece 7" || name == "MapPiece 9")
        {
            AdjustTreesCounterClockwise();
            RotateTreesCounterClockwise();
        }
    }

    /*
    public void checkHorizontalMovement()
    {

        colliderMovement = Physics2D.OverlapCircle(
            movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * offsetMovement, 0f, 0f), .2f, detectedLayer);
        if (!colliderMovement)
        {
            movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * offsetMovement, 0f, 0f);
            //checkMatching(movePoint);
            audioManager.Play("mapMovement");
            matchingAllSides(movePoint);
        }
    }

    public void checkVerticalMovement()
    {

        colliderMovement = Physics2D.OverlapCircle(
            movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * offsetMovement, 0f), .2f, detectedLayer);
        if (!colliderMovement)
        {
            movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * offsetMovement, 0f);
            //checkMatching(movePoint);
            audioManager.Play("mapMovement");
            matchingAllSides(movePoint);
        }
    }

*/
    
    
    public bool getIsMatchingRight()
    {
        return isMatchingRight;
    }

    public bool getIsMatchingUp()
    {
        return isMatchingUp;
    }

    public bool getIsMatchingLeft()
    {
        return isMatchingLeft;
    }

    public bool getIsMatchingDown()
    {
        return isMatchingDown;
    }

    private void RotateTreesClockwise()
    {
        if (GetComponent<TreeRotation>().forest0.activeSelf == true)
        {
            GetComponent<TreeRotation>().forest0.SetActive(false);
            GetComponent<TreeRotation>().forest90.SetActive(true);
        }
        else if (GetComponent<TreeRotation>().forest90.activeSelf == true)
        {
            GetComponent<TreeRotation>().forest90.SetActive(false);
            GetComponent<TreeRotation>().forest180.SetActive(true);
        }
        else if (GetComponent<TreeRotation>().forest180.activeSelf == true)
        {
            GetComponent<TreeRotation>().forest180.SetActive(false);
            GetComponent<TreeRotation>().forest270.SetActive(true);
        }
        else if (GetComponent<TreeRotation>().forest270.activeSelf == true)
        {
            GetComponent<TreeRotation>().forest270.SetActive(false);
            GetComponent<TreeRotation>().forest0.SetActive(true);
        }

    }

    private void RotateTreesCounterClockwise()
    {
        if (GetComponent<TreeRotation>().forest0.activeSelf == true)
        {
            GetComponent<TreeRotation>().forest0.SetActive(false);
            GetComponent<TreeRotation>().forest270.SetActive(true);
        }
        else if (GetComponent<TreeRotation>().forest270.activeSelf == true)
        {
            GetComponent<TreeRotation>().forest270.SetActive(false);
            GetComponent<TreeRotation>().forest180.SetActive(true);
        }
        else if (GetComponent<TreeRotation>().forest180.activeSelf == true)
        {
            GetComponent<TreeRotation>().forest180.SetActive(false);
            GetComponent<TreeRotation>().forest90.SetActive(true);
        }
        else if (GetComponent<TreeRotation>().forest90.activeSelf == true)
        {
            GetComponent<TreeRotation>().forest90.SetActive(false);
            GetComponent<TreeRotation>().forest0.SetActive(true);
        }
    }


    private void AdjustTreesClockwise()
    {
        GetComponent<TreeRotation>().forest0.transform.Rotate(0, 0, 90);
        GetComponent<TreeRotation>().forest90.transform.Rotate(0, 0, 90);
        GetComponent<TreeRotation>().forest180.transform.Rotate(0, 0, 90);
        GetComponent<TreeRotation>().forest270.transform.Rotate(0, 0, 90);
    }

    private void AdjustTreesCounterClockwise()
    {
        GetComponent<TreeRotation>().forest0.transform.Rotate(0, 0, -90);
        GetComponent<TreeRotation>().forest90.transform.Rotate(0, 0, -90);
        GetComponent<TreeRotation>().forest180.transform.Rotate(0, 0, -90);
        GetComponent<TreeRotation>().forest270.transform.Rotate(0, 0, -90);
    }



    /*
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
    */

    public void setPlayerInside(GameObject p)
    {
        player = p;
    }

    public void checkPositionPlayer(GameObject p)
    {
        float offset = 0.05f;
        float mapx = this.transform.position.x;
        float mapy = this.transform.position.y;
        float px = p.transform.position.x;
        float py = p.transform.position.y;
        float pz = p.transform.position.z;

        float horizontalLimDx = mapx + 7.5f;
        float horizontalLimSx = mapx - 7.5f;
        float verticalLimUp = mapy + 7.5f;
        float verticalLimDw = mapy - 7.5f;

        if (px > horizontalLimDx)
        {
            //Debug.Log("px: "+px+"-hd"+horizontalLimDx);
            p.transform.position = new Vector3(horizontalLimDx-offset,p.transform.position.y,pz);
        }
        else if (px < horizontalLimSx)
        {
            //Debug.Log("px: "+px+"-hs"+horizontalLimSx);
            p.transform.position = new Vector3(horizontalLimSx+offset,p.transform.position.y,pz);
        }

        if (py > verticalLimUp)
        {
            //Debug.Log("py: "+py+"-vu"+verticalLimUp);
            p.transform.position = new Vector3(p.transform.position.x, verticalLimUp - offset, pz);
        }
        else if(py< verticalLimDw)
        {
            //Debug.Log("py: "+py+"-vd"+verticalLimDw);
            p.transform.position = new Vector3(p.transform.position.x, verticalLimDw + offset, pz);
        }



    }

    
}

