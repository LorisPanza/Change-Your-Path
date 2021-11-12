using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName="New TileMap",menuName = "TileMaps/TileMap")]
public class Map : ScriptableObject
{
    public string up ;
    public string right;
    public string down;
    public string left;

    public string getUp()
    {
        return up;
    }
    public string getDown()
    {
        return down;
    }
    public string getLeft()
    {
        return left;
    }
    public string getRight()
    {
        return right;
    }

    public void clockwiseRotation()
    {
        string varright,varleft,vardown;
        varright = right;
        right=up;

        vardown = down;
        down=varright;

        varleft = left;
        left=vardown;
        
        up = varleft;
        
    }

    public void counterclockwiseRotation()
    {
        string varright, varleft, vardown;
        varleft = left;
        left = up;

        vardown = down;
        down = varleft;

        varright = right;
        right = vardown;

        up = varright;
    }
}
