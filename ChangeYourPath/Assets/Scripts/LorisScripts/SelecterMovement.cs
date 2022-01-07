using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class SelecterMovement : MonoBehaviour
{
    //public LayerMask[] obstacles;
    public float moveSpeed = 100f;
    public LayerMask detectedLayerMap;
    public LayerMask detectedLayerPlayer, detectedLayerRobot;
    public LayerMask detectedLayerLimit;
    private int offsetMovement=18;
    private bool choosen = false,isChild=false, isChildRobot = false;
    private Collider2D chosenMapCollider=null,movementCollider=null,playerCollider=null,limitCollider=null, robotCollider=null; //colliderMatchingUp,colliderMatchingDown,colliderMatchingRight,colliderMatchingLeft;
    public Transform movePoint;
    public AudioManager audioManager;
    //public NPC wilem;
    public Robot robot;
    public Player kvothe;
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
                isChild=characterCollider(playerCollider, go, isChild, "Player");
                if (isChild)
                {
                    chosenMapCollider.GetComponent<MapMovement>().checkPositionPlayer(playerCollider.gameObject);
                }
                
                if (SceneManager.GetActiveScene().name == "SpringScene") {
                    robotCollider = checkRobot();
                    isChildRobot=characterCollider(robotCollider, go, isChildRobot, "Robot");

                    if (isChildRobot)
                    {
                        chosenMapCollider.GetComponent<MapMovement>().checkPositionPlayer(robotCollider.gameObject);
                    }
                }
                

                /*if (playerCollider)
                {
                    go.GetComponent<MapFeatures>().player = playerCollider.gameObject;
                    playerCollider.gameObject.transform.SetParent(go.transform);
                    go.GetComponent<MapMovement>().setPlayerInside(playerCollider.gameObject);
                    isChild = true;
                }*/
                GrabColorSelecter(new Vector3(0,0,0),Color.yellow, this.GetComponent<Tilemap>());
                audioManager.Play("mapSelection");
                MapMovement chosenMapMov = chosenMapCollider.gameObject.GetComponent<MapMovement>();
                GameObject map = chosenMapCollider.gameObject;
                enableSelectionMapCondition(chosenMapMov,map);
            }

        }
        else if (Input.GetKeyDown(KeyCode.Space) && choosen == true)
        {
            GameObject go = chosenMapCollider.gameObject;
            GameObject chosenMap= chosenMapCollider.gameObject;
            
            if (disableSelectionMapCondition(go,chosenMap) && go.transform.position.x%offsetMovement==0 && go.transform.position.y%offsetMovement==0)
            {
                
                audioManager.Play("mapChoice");
                GrabColorSelecter(new Vector3(0,0,0),Color.white, this.GetComponent<Tilemap>());
                go.transform.SetParent(grid.transform);
                go.GetComponent<MapMovement>().enabled = false;
                //chosenMapCollider.gameObject.GetComponent<MapFeatures>().enabled = false;

                choosen = false;

                this.GetComponent<Renderer>().material.color = Color.white;

                /*if (isChild == true)
                {
                    go.GetComponent<MapFeatures>().player = null;
                    playerCollider.gameObject.transform.SetParent(null);
                    go.GetComponent<MapMovement>().setPlayerInside(null);
                    isChild = false;
                }*/

                if (SceneManager.GetActiveScene().name == "SpringScene") {
                    Vector2 centerRobot = findCenter(robot.transform.position);
                    Vector2 centerKvothe = findCenter(kvothe.transform.position);
                    
                    Collider2D right = Physics2D.OverlapCircle(new Vector2(centerRobot.x + 18, centerRobot.y), 1f, detectedLayerMap);
                    Collider2D left = Physics2D.OverlapCircle(new Vector2(centerRobot.x - 18, centerRobot.y), 1f, detectedLayerMap);
                    Collider2D top = Physics2D.OverlapCircle(new Vector2(centerRobot.x, centerRobot.y + 18), 1f, detectedLayerMap);
                    Collider2D bottom = Physics2D.OverlapCircle(new Vector2(centerRobot.x, centerRobot.y - 18), 1f, detectedLayerMap);

                    if (robot.robotQuest.isActive) {
                        if (centerRobot != centerKvothe && right == null && left == null && top == null && bottom == null) {
                            robot.missionComplete();
                        }
                    }

                    isChildRobot=freeCharacterCollider(robotCollider, go, isChildRobot, "Robot");
                }
                
                
                isChild=freeCharacterCollider(playerCollider, go, isChild, "Player");
                
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

    Vector2 findCenter(Vector3 position) {
        double x = (double)position.x/18;
        double y = (double)position.y/18;
        float xRound = (float)Math.Round(x, 0, MidpointRounding.ToEven)*18f;
        float yRound = (float)Math.Round(y, 0, MidpointRounding.ToEven)*18f;
        return new Vector2(xRound, yRound);
    }

    bool characterCollider(Collider2D character, GameObject go, bool isChild, String stringCharacter) {
        if (character)
        {
            //Debug.Log("Ho preso il charcter");
            if (stringCharacter == "Player") 
                go.GetComponent<MapFeatures>().player = character.gameObject;
            else if (stringCharacter == "Robot") {
                go.GetComponent<MapFeatures>().robot = character.gameObject;
            }
            character.gameObject.transform.SetParent(go.transform);
            go.GetComponent<MapMovement>().setPlayerInside(character.gameObject, stringCharacter);
            return true;
        }

        return false;
    }

    bool freeCharacterCollider(Collider2D character, GameObject go, bool isChild, String stringCharacter) {
        if (isChild == true)
        {
            //Debug.Log("Ho rilasciato il charcter");
            if (stringCharacter == "Player") 
                go.GetComponent<MapFeatures>().player = null;
            else if (stringCharacter == "Robot")
                go.GetComponent<MapFeatures>().robot = null;
            character.gameObject.transform.SetParent(null);
            go.GetComponent<MapMovement>().setPlayerInside(null, stringCharacter);
            return false;
        }

        return false;
    }


    public void enableSelectionMapCondition(MapMovement chosenMapMov,GameObject map)
    {
        //MapMovement chosenMapMov = chosenMapCollider.gameObject.GetComponent<MapMovement>();
        
        chosenMapMov.matchingAllSides(chosenMapMov.movePoint);
        //GameObject map = chosenMapCollider.gameObject;
        if (chosenMapMov.getIsMatchingLeft())
        {
            GameObject leftDetectedMap=chosenMapMov.matchingLeft(chosenMapMov.movePoint);
            //Debug.Log("A sinistra ho:"+leftDetectedMap.name);
            if (leftDetectedMap != null)
            { 
                modifyLeftBoundaryColliders(leftDetectedMap,true,map);
            }
                
        }
        

        if (chosenMapMov.getIsMatchingRight())
        {
            
            GameObject rightDetectedMap = chosenMapMov.matchingRight(chosenMapMov.movePoint);
            if (rightDetectedMap!=null)
            {
                modifyRightBoundaryColliders(rightDetectedMap,true,map);
            }
        }

        if (chosenMapMov.getIsMatchingUp())
        {
            GameObject upDetectedMap = chosenMapMov.matchingUp(chosenMapMov.movePoint);
            if (upDetectedMap != null)
            {
                modifyUpBoundaryColliders(upDetectedMap,true,map);
            }
        }
            
        if (chosenMapMov.getIsMatchingDown())
        {
            GameObject downDetectedMap = chosenMapMov.matchingDown(chosenMapMov.movePoint);
            if (downDetectedMap != null)
            {
                modifyDownBoundaryColliders(downDetectedMap,true,map);
            }
        }
    }

    public bool disableSelectionMapCondition(GameObject map,GameObject chosenMap)
    {
        //MapMovement chosenMapMov = chosenMapCollider.gameObject.GetComponent<MapMovement>();
        MapMovement chosenMapMov = chosenMap.GetComponent<MapMovement>();
        if (chosenMapMov.matchingAllSides(chosenMapMov.movePoint))
        {
            //GameObject map=chosenMapCollider.gameObject;
            if (chosenMapMov.getIsMatchingLeft())
            {
                GameObject leftDetectedMap=chosenMapMov.matchingLeft(chosenMapMov.movePoint);
                if (leftDetectedMap != null)
                { 
                    modifyLeftBoundaryColliders(leftDetectedMap,false,map);
                }
                
            }
            

            if (chosenMapMov.getIsMatchingRight())
            {
                GameObject rightDetectedMap = chosenMapMov.matchingRight(chosenMapMov.movePoint);
                if (rightDetectedMap!=null)
                {
                    modifyRightBoundaryColliders(rightDetectedMap,false,map);
                }
            }
            

            if (chosenMapMov.getIsMatchingUp())
            {
                GameObject upDetectedMap = chosenMapMov.matchingUp(chosenMapMov.movePoint);
                if (upDetectedMap != null)
                {
                    modifyUpBoundaryColliders(upDetectedMap,false,map);
                }
            }
            
            
            if (chosenMapMov.getIsMatchingDown())
            {
                GameObject downDetectedMap = chosenMapMov.matchingDown(chosenMapMov.movePoint);
                if (downDetectedMap != null)
                {
                    modifyDownBoundaryColliders(downDetectedMap,false,map);
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
    
    public void modifyLeftBoundaryColliders(GameObject leftMap,bool activate,GameObject thisMap)
    {
        //GameObject thisMap=chosenMapCollider.gameObject;
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

    public void modifyRightBoundaryColliders(GameObject rightMap,bool activate,GameObject thisMap)
    {
        //GameObject thisMap=chosenMapCollider.gameObject;
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
    
    public void modifyUpBoundaryColliders(GameObject upMap,bool activate,GameObject thisMap)
    {
        //GameObject thisMap=chosenMapCollider.gameObject;
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
    
    public void modifyDownBoundaryColliders(GameObject downMap,bool activate,GameObject thisMap)
    {
        //GameObject thisMap=chosenMapCollider.gameObject;
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

    public Collider2D checkRobot() {
            return Physics2D.OverlapBox(movePoint.position, new Vector2(offsetMovement, offsetMovement), 0, detectedLayerRobot);
    }
}


