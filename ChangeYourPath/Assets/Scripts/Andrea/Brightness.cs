using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brightness : MonoBehaviour
{
    public Image controller;

    void Start() {
        //controller.color = new Color(0, 0, 0, 0);
    }

    public void setBrightness(float brightness) {
        double i = 0.25;
        if (brightness > 0.25)
            i = 0.25 - (brightness - 0.25);
        else if (brightness < 0.25)
            i = 0.25 + (0.25 - brightness);
        controller.color = new Color(0, 0, 0, (float)i);
    }
}
