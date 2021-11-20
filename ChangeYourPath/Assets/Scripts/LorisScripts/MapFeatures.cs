using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFeatures : MonoBehaviour
{
    public Map tileMap;
    public List<GameObject> enviromentalElements=null;
    public GameObject player;
   


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
