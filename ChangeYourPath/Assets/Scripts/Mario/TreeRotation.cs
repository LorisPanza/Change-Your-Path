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

        if (this.transform.rotation.z == 0) forest0.SetActive(true);
        if (this.transform.rotation.z == 90) forest90.SetActive(true);
        if (this.transform.rotation.z == 180) forest180.SetActive(true);
        if (this.transform.rotation.z == -90) forest270.SetActive(true);

    }
}
