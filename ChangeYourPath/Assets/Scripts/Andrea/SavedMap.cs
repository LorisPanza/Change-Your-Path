using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedMap
{
    public string title;
    public float mapPositionX;
    public float mapPositionY;
    public float rotation;

    public override string ToString()
    {
        return title.ToString() + ", " + mapPositionX.ToString() + ", " + mapPositionX.ToString() + ", " + rotation.ToString();;
    }
}
