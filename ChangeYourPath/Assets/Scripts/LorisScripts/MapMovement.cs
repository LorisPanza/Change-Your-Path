using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class MapMovement : MonoBehaviour
{
    public float moveSpeed = 100f;
    public LayerMask detectedLayer;
    private int offsetMovement = 18;
    private int offsetMatching = 18;
    private Collider2D colliderMovement;
    public Transform movePoint;
    private MapFeatures collideMap, thisMap;
    private bool isMatching, isMatchingRight, isMatchingLeft, isMatchingDown, isMatchingUp,rightVoid,leftVoid,upVoid,downVoid;
    public AudioManager audioManager;
    private GameObject player=null, robot = null;
    


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
        rightVoid = false;
        leftVoid = false;
        upVoid = false;
        downVoid = false;
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
        else
        {
            rightVoid = true;
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
        else
        {
            leftVoid = true;
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
        else
        {
            upVoid = true;
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
        else
        {
            downVoid = true;
        }

        return null;
    }



    public void rotateClockwise()
    {
        thisMap = this.GetComponent<MapFeatures>();
        thisMap.tileMap.clockwiseRotation();
        thisMap.rotateSpriteClockwise();
        thisMap.rotateBoundaryClockwise();
        if (player != null)
        {
            checkPositionPlayer(player);
            //Vector3 newPos=managePlayerRotation(player.transform.position,GetComponent<Tilemap>());
            transform.Rotate(0, 0, -90);
            //player.transform.position = newPos;
            tileReposition(this.GetComponent<Tilemap>(), player.transform.position);
        }
        else
        {
            transform.Rotate(0, 0, -90);
        }
        
        if (name == "MapPiece 6" || name == "MapPiece 8" || name == "MapPiece 7" || name == "MapPiece 9")
        {
            AdjustTreesClockwise();
            RotateTreesClockwise();
        }

    }

    public void rotateCounterClockwise()
    {
        thisMap = this.GetComponent<MapFeatures>();
        thisMap.tileMap.counterclockwiseRotation();
        thisMap.rotateSpriteCounterClockwise();
        thisMap.rotateBoundaryCounterClockwise();
        if (player != null)
        {
            checkPositionPlayer(player);
            //Vector3 newPos=managePlayerRotation(player.transform.position,GetComponent<Tilemap>());
            transform.Rotate(0, 0, 90);
            //player.transform.position = newPos;
            tileReposition(this.GetComponent<Tilemap>(), player.transform.position);
        }
        else
        {
            transform.Rotate(0, 0, 90);

        }

        

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

    public void setPlayerInside(GameObject p, String character)
    {
        if (character == "Player")
            player = p;
        else if (character == "Robot")
        {
            robot = p;
            Debug.Log("Set robot inside...");
        }
            
          
    }

    public void checkPositionPlayer(GameObject p)
    {
        float offset = 0.005f;
        float mapx = this.transform.position.x;
        float mapy = this.transform.position.y;
        float mapz = this.transform.position.z;
        float px = p.transform.position.x;
        float py = p.transform.position.y;
        float pz = p.transform.position.z;
        //Debug.Log("X: "+px+"Y: "+py+"Z: "+pz);
        //Debug.Log("X: "+mapx+"Y: "+mapy+"Z: "+mapz);

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

    public void tileReposition(Tilemap tileMap, Vector3 pos)
    {
        Vector3 npos = new Vector3(pos.x, (pos.y - 1f), pos.z);
        //Debug.Log("X: "+pos.x+"Y: "+pos.y+"Z: "+pos.z);
        Vector3Int tilePos = tileMap.WorldToCell(npos);
        Tile tile = tileMap.GetTile<Tile>(tilePos);

        //Debug.Log(tile.name);
        if (tile.name == "map piece ocean" || tile.name=="map piece snow ocean")
        {
            if (tileMap.name == "MapPiece10" || tileMap.name == "MapPiece11" || tileMap.name == "MapPiece12" ||
                tileMap.name == "MapPiece13")
            {
                movePlayerRiver(GetComponent<Tilemap>(),player);
            }
            else
            {
                player.transform.position = tileMap.transform.position;
            }
        }
    }

    public bool isVoidRight()
    {
        return rightVoid;
    }
    public bool isVoidLeft()
    {
        return leftVoid;
    }
    public bool isVoidUp()
    {
        return upVoid;
    }
    public bool isVoidDown()
    {
        return downVoid;
    }

    public Vector3 managePlayerRotation(Vector3 playerPos,Tilemap map)
    {
        //Vector3 playerPos = p.transform.position;
        Vector3 mapPos = map.transform.position;
        Vector3 newPos=new Vector3(playerPos.x,playerPos.y,playerPos.z);
        float diffx = playerPos.x - mapPos.x;
        float diffy = playerPos.y - mapPos.y;
        
        //Debug.Log("X: "+playerPos.x+"Y: "+playerPos.y+"Z: "+playerPos.z);
        //Debug.Log("DifferenzaX: "+diffx+"DifferenzaY: "+diffy);

        if (diffx > 0 && diffy > 0)
        {
            //Debug.Log("partenza in alto a destra");
            newPos = new Vector3(playerPos.x,mapPos.y-diffy,playerPos.z);
        }
        if (diffx > 0 && diffy < 0)
        {
            //Debug.Log("partenza in basso a destra");
            newPos = new Vector3(mapPos.x-diffx,playerPos.y,playerPos.z);
        }
        if (diffx < 0 && diffy < 0)
        {
            //Debug.Log("partenza in basso a sinistra");
            newPos = new Vector3(playerPos.x,mapPos.y-diffy,playerPos.z);
        }
        if (diffx < 0 && diffy > 0)
        {
            //Debug.Log("partenza in alto a sinistra");
            newPos = new Vector3(mapPos.x-diffx,playerPos.y,playerPos.z);
        }

        //Debug.Log("newPosx: "+newPos.x+"newPosy: "+newPos.y);
        return newPos;
        //p.transform.position = newPos;




    }

    public void movePlayerRiver(Tilemap tilemap,GameObject p)
    {
        float off = 6f;
        MapFeatures mapFeatures = tilemap.GetComponent<MapFeatures>();
        Vector3 pos = p.transform.position;
        Vector3 map = mapFeatures.transform.position;
        float diffx = pos.x - map.x;
        float diffy = pos.y - map.y;
        //Debug.Log("X: "+pos.x+"Y: "+pos.y);
        if (tilemap.name == "MapPiece10" || tilemap.name == "MapPiece11" || tilemap.name == "MapPiece12" ||
            tilemap.name == "MapPiece13")
        {
            if (mapFeatures.tileMap.getRight() == "River" && mapFeatures.tileMap.getUp() == "River")
            {
                //Debug.Log("River in alto e destra");
                if (!(diffx > 0 && diffy>0))
                {
                    //Debug.Log("Non sto nel centro");
                    p.transform.position = new Vector3(map.x - off, map.y - off, pos.z);
                }
                else
                {
                    //Debug.Log("Sto nel centro");
                    p.transform.position = new Vector3(map.x + off, map.y + off, pos.z);
                }
            }
            else if (mapFeatures.tileMap.getRight() == "River" && mapFeatures.tileMap.getDown() == "River")
            {
                //Debug.Log("River in basso e destra");
                if (!(diffx>0 && diffy <0))
                {
                    //Debug.Log("Non sto nel centro");
                    p.transform.position = new Vector3(map.x - off, map.y + off, pos.z);
                }
                else
                {
                    //Debug.Log("Sto nel centro");
                    p.transform.position = new Vector3(map.x + off, map.y - off, pos.z);
                }
            } 
            else if (mapFeatures.tileMap.getDown() == "River" && mapFeatures.tileMap.getLeft() == "River")
            {
                //Debug.Log("River in basso e sinistra");
                if (!(diffx<0 && diffy<0))
                {
                    //Debug.Log("Non sto nel centro");
                    p.transform.position = new Vector3(map.x + off, map.y + off, pos.z);
                }
                else
                {
                    //Debug.Log("Sto nel centro");
                    p.transform.position = new Vector3(map.x - off, map.y - off, pos.z);
                }
            }
            else if (mapFeatures.tileMap.getUp() == "River" && mapFeatures.tileMap.getLeft() == "River")
            {
                if (!(diffx<0 && diffy>0))
                {
                    //Debug.Log("Non sto nel centro");
                    p.transform.position = new Vector3(map.x + off, map.y - off, pos.z);
                }
                else
                {
                    //Debug.Log("Sto nel centro");
                    p.transform.position = new Vector3(map.x - off, map.y + off, pos.z);
                }
            }
        }
        
    }
   
    
}

