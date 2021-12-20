using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeTileColor : MonoBehaviour
{
    private float sideLength=8f;
    public Tilemap tilemap;
    public Tile tile;
    private Vector3Int position = new Vector3Int(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            tilemap.SetTile(position,tile);
            //tilemap.SetTileFlags(position,TileFlags.None);
            
            tilemap.SetColor(position,Color.yellow);
            GrabColorSelecter(position,Color.yellow);
            
        }
    }

    public void GrabColorSelecter(Vector3 origin, Color c)
    {

        int rightX = (int)(origin.x + sideLength);
        int leftX = (int)(origin.x - sideLength);
        int upY = (int) (origin.y + sideLength);
        int downY = (int) (origin.y - sideLength);
        
        Debug.Log("right-left: "+rightX + "-" + leftX);
        Debug.Log("up-down: "+upY + "-" + downY);
        

        for (int i = leftX; i <= rightX;  i++)
        {
            tilemap.SetColor(new Vector3Int(i,upY,(int)origin.z),Color.yellow);
            tilemap.SetColor(new Vector3Int(i,downY,(int)origin.z),Color.yellow);
        }
        for (int i = downY; i <= upY;  i++)
        {
            tilemap.SetColor(new Vector3Int(rightX,i,(int)origin.z),Color.yellow);
            tilemap.SetColor(new Vector3Int(leftX,i,(int)origin.z),Color.yellow);
        }
    }
}
