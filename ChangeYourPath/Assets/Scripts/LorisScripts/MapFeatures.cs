using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFeatures : MonoBehaviour
{
    private LayerMask detectedLayerMap = 7;
    public Map tileMap;
    public List<GameObject> enviromentalElements=null;
    public GameObject player;
    private int offsetMovement=18;
    //private SelecterMovement selecterMovement = null;


    private bool checkAround(Transform position)
    {
        if (!Physics2D.OverlapCircle(position.position, .2f, detectedLayerMap))
        {
            return this.GetComponent<MapMovement>().matchingAllSides(position);
        }

        return false;
    }
    public void placeNewMap()
    {
        int i = offsetMovement;
        
        Transform selecterTransform = FindObjectOfType<SelecterMovement>().getTransform();
        
        
        while (!checkAround(selecterTransform))
        {
            selecterTransform.position.Set(selecterTransform.position.x +i,
                selecterTransform.position.y,selecterTransform.position.z);

            
        }
        this.transform.position = selecterTransform.position;
    }
    

    public void rotateSpriteClockwise()
    {
        SpriteRenderer sprite;
        for (int i = 0; i < enviromentalElements.Count; i++)
        {
            sprite = enviromentalElements[i].GetComponent<SpriteRenderer>();
            sprite.transform.Rotate(0,0,90);
         
        }

        if (player != null)
        {
            Debug.Log("Ruoto orario il player");
            sprite = player.GetComponent<SpriteRenderer>();
            sprite.transform.Rotate(0,0,90);
        }
    }
    
    public void rotateSpriteCounterClockwise()
    {
        SpriteRenderer sprite;
        for (int i = 0; i < enviromentalElements.Count; i++)
        {   
            sprite = enviromentalElements[i].GetComponent<SpriteRenderer>();
            sprite.transform.Rotate(0,0,- 90);
           
        }
        if (player != null)
        {
            sprite = player.GetComponent<SpriteRenderer>();
            sprite.transform.Rotate(0,0,-90);
        }
    }
    
    
}
