using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRotation : MonoBehaviour
{
    public GameObject forest0;
    public GameObject forest90;
    public GameObject forest180;
    public GameObject forest270;

    void Start()
    {
        forest0.SetActive(false);
        forest90.SetActive(false);
        forest180.SetActive(false);
        forest270.SetActive(false);

        if (this.name == "MapPiece 6" || this.name == "MapPiece 8")
        {
            //Debug.Log(name + ":  " + transform.rotation.z);
            if (transform.rotation.eulerAngles.z == 0)
            {
                forest0.SetActive(true);
                InitTrees0();
            }
            else if (transform.rotation.eulerAngles.z == 90)
            {
                forest270.SetActive(true);
                InitTrees90();
            }
            else if (transform.rotation.eulerAngles.z == 180)
            {
                forest180.SetActive(true);
                InitTrees180();
            }
            else if (transform.rotation.eulerAngles.z == 270)
            {
                forest90.SetActive(true);
                InitTrees270();
            }
            //Debug.Log(name + ":  " + transform.rotation.eulerAngles.z);
        }
        else if (this.name == "MapPiece 7")
        {
            if (transform.rotation.eulerAngles.z == 0)
            {
                forest270.SetActive(true);
                InitTrees0();
            }
            else if (transform.rotation.eulerAngles.z == 90)
            {
                forest180.SetActive(true);
                InitTrees90();
            }
            else if (transform.rotation.eulerAngles.z == 180)
            {
                forest90.SetActive(true);
                InitTrees180();
            }
            else if (transform.rotation.eulerAngles.z == 270)
            {
                forest0.SetActive(true);
                InitTrees270();
            }
        }
        else if (this.name == "MapPiece 9")
        {
            if (transform.rotation.eulerAngles.z == 0)
            {
                forest180.SetActive(true);
                InitTrees0();
            }
            else if (transform.rotation.eulerAngles.z == 90)
            {
                forest90.SetActive(true);
                InitTrees90();
            }
            else if (transform.rotation.eulerAngles.z == 180)
            {
                forest0.SetActive(true);
                InitTrees180();
            }
            else if (transform.rotation.eulerAngles.z == 270)
            {
                forest270.SetActive(true);
                InitTrees270();
            }
        }

    }

    private void InitTrees0()
    {
        forest0.transform.rotation = Quaternion.Euler(0, 0, 0);
        forest90.transform.rotation = Quaternion.Euler(0, 0, -90);
        forest180.transform.rotation = Quaternion.Euler(0, 0, 180);
        forest270.transform.rotation = Quaternion.Euler(0, 0, 90);
    }
    private void InitTrees90()
    {
        forest90.transform.rotation = Quaternion.Euler(0, 0, -90);
        forest180.transform.rotation = Quaternion.Euler(0, 0, 180);
        forest270.transform.rotation = Quaternion.Euler(0, 0, 90);
        forest0.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    private void InitTrees180()
    {
        forest180.transform.rotation = Quaternion.Euler(0, 0, 180);
        forest270.transform.rotation = Quaternion.Euler(0, 0, 90);
        forest0.transform.rotation = Quaternion.Euler(0, 0, 0);
        forest90.transform.rotation = Quaternion.Euler(0, 0, -90);
    }
    private void InitTrees270()
    {
        forest270.transform.rotation = Quaternion.Euler(0, 0, 90);
        forest0.transform.rotation = Quaternion.Euler(0, 0, 0);
        forest90.transform.rotation = Quaternion.Euler(0, 0, -90);
        forest180.transform.rotation = Quaternion.Euler(0, 0, 180);
    }
}
