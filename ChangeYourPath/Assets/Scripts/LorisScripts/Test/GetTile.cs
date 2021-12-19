using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GetTile : MonoBehaviour
{
    public GameObject player;

    private Tile t;

    public Tilemap tm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            t=tileRec(tm, player.transform.position);
            Debug.Log(t.name);
            if (t.name == "map piece snow ocean" || t.name=="map piece ocean")
            {
                player.transform.position = tm.transform.position;
            }
        }
    }
    
    Tile tileRec(Tilemap tileMap, Vector3 pos) { 
        Vector3Int tilePos = tileMap.WorldToCell(pos);
        Tile tile = tileMap.GetTile<Tile>(tilePos);

        return tile;
    }
}
