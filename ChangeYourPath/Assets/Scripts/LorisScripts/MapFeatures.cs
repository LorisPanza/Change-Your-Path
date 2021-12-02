using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFeatures : MonoBehaviour
{
    public LayerMask detectedLayerMap;
    public Map tileMap;
    public List<GameObject> enviromentalElements = null;
    public GameObject player;
    private int offsetMovement = 18;
    //private SelecterMovement selecterMovement = null;


    private bool checkAround(Transform position)
    {
        if (!Physics2D.OverlapCircle(position.position, .2f, detectedLayerMap))
        {
            MapMovement mv = this.GetComponent<MapMovement>();
            if (mv.matchingAllSides(position) & !mv.getIsMatchingDown() & !mv.getIsMatchingLeft() &
                !mv.getIsMatchingRight() & !mv.getIsMatchingUp())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    public void placeNewMap()
    {
        int offset = offsetMovement;
        bool placed = false;

        Transform checkHere = FindObjectOfType<SelecterMovement>().movePoint;
        Vector3 selecterPosition = new Vector3(checkHere.position.x,
            checkHere.position.y, checkHere.position.z);

        if (checkAround(checkHere))
        {
            placed = true;
        }
        int levelGrid = 1;
        int i = -levelGrid;
        int j = levelGrid;
        int maxCicle = 30;

        while (!placed && maxCicle-- > 0)
        {
            for (int ii = i; ii <= -i; ii++)
            {
                checkHere.position = new Vector3(selecterPosition.x + ii * offset,
                selecterPosition.y + j * offset, selecterPosition.z);

                if (checkAround(checkHere))
                {
                    placed = true;
                    break;
                }

                checkHere.position = new Vector3(selecterPosition.x + ii * offset,
                selecterPosition.y - j * offset, selecterPosition.z);

                if (checkAround(checkHere))
                {
                    placed = true;
                    break;
                }

            }

            if (!placed)
            {
                for (int jj = j - 1; jj >= -j + 1; jj--)
                {
                    checkHere.position = new Vector3(selecterPosition.x + i * offset,
                            selecterPosition.y + jj * offset, selecterPosition.z);

                    if (checkAround(checkHere))
                    {
                        placed = true;
                        break;

                    }

                    checkHere.position = new Vector3(selecterPosition.x - i * offset,
                    selecterPosition.y + jj * offset, selecterPosition.z);

                    if (checkAround(checkHere))
                    {
                        placed = true;
                        break;
                    }
                }
            }
            levelGrid += 1;
        }
        if (placed) this.transform.position = checkHere.position;
        else
        {
            Debug.Log("Unable to place the tile with the complex method");
            this.transform.position = new Vector3(checkHere.position.x + 36,
            checkHere.position.y + 36, checkHere.position.z);
            placed = true;
        }
    }



    public void rotateSpriteClockwise()
    {
        SpriteRenderer sprite;
        for (int i = 0; i < enviromentalElements.Count; i++)
        {

            sprite = enviromentalElements[i].GetComponent<SpriteRenderer>();
            sprite.transform.Rotate(0, 0, 90);

        }

        if (player != null)
        {
            //Debug.Log("Ruoto orario il player");
            sprite = player.GetComponent<SpriteRenderer>();
            sprite.transform.Rotate(0, 0, 90);
        }
    }

    public void rotateSpriteCounterClockwise()
    {
        SpriteRenderer sprite;
        for (int i = 0; i < enviromentalElements.Count; i++)
        {
            sprite = enviromentalElements[i].GetComponent<SpriteRenderer>();
            sprite.transform.Rotate(0, 0, -90);

        }
        if (player != null)
        {
            sprite = player.GetComponent<SpriteRenderer>();
            sprite.transform.Rotate(0, 0, -90);
        }
    }

    public void rotateBoundaryClockwise()
    {
        GameObject rightBoundary = this.transform.Find("RightBoundary").gameObject;
        GameObject leftBoundary = this.transform.Find("LeftBoundary").gameObject;
        GameObject upBoundary = this.transform.Find("UpBoundary").gameObject;
        GameObject downBoundary = this.transform.Find("DownBoundary").gameObject;
        
        rightBoundary.transform.Rotate(0, 0, 90);
        leftBoundary.transform.Rotate(0, 0, 90);
        upBoundary.transform.Rotate(0, 0, 90);
        downBoundary.transform.Rotate(0, 0, 90);
    }
    
    public void rotateBoundaryCounterClockwise()
    {
        GameObject rightBoundary = this.transform.Find("RightBoundary").gameObject;
        GameObject leftBoundary = this.transform.Find("LeftBoundary").gameObject;
        GameObject upBoundary = this.transform.Find("UpBoundary").gameObject;
        GameObject downBoundary = this.transform.Find("DownBoundary").gameObject;
        
        rightBoundary.transform.Rotate(0, 0, -90);
        leftBoundary.transform.Rotate(0, 0, -90);
        upBoundary.transform.Rotate(0, 0, -90);
        downBoundary.transform.Rotate(0, 0, -90);
    }
    
    

}
