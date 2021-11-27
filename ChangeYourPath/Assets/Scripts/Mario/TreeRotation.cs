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

        //Debug.Log(name + ":  " + transform.rotation.z);
        if (transform.rotation.eulerAngles.z == 0)
        {
            forest0.SetActive(true);
            InitTrees(0);
        }
        else if (transform.rotation.eulerAngles.z == 90)
        {
            forest90.SetActive(true);
            InitTrees(90);
        }
        else if (transform.rotation.eulerAngles.z == 180)
        {
            forest180.SetActive(true);
            InitTrees(180);
        }
        else if (transform.rotation.eulerAngles.z == 270)
        {
            forest270.SetActive(true);
            InitTrees(90);
        }
        Debug.Log(name + ":  " + transform.rotation.eulerAngles.z);
    }

    private void InitTrees(int degree)
    {
        forest0.transform.rotation = Quaternion.Euler(0, 0, degree);
        forest90.transform.rotation = Quaternion.Euler(0, 0, degree - 90);
        forest180.transform.rotation = Quaternion.Euler(0, 0, degree - 180);
        forest270.transform.rotation = Quaternion.Euler(0, 0, degree + 90);
    }
}
