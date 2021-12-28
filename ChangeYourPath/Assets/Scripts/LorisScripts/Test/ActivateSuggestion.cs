using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSuggestion : MonoBehaviour
{                           
    // Assign in inspector
    public GameObject canvas;
    private bool isShowing;
    public bool flag = false;
 
    void Update() {
        if (Input.GetKeyDown(KeyCode.H) && flag) {
            canvas.gameObject.SetActive(isShowing);
            isShowing = !isShowing;
        }
    }

    private void Start()
    {
        isShowing = false;
    }

    public void activateWilemSuggestion()
    {
        canvas.SetActive(true);
        flag = true;
    }

    public void disactivateWilemSuggestion()
    {
        if (flag == true)
        { 
            canvas.SetActive(false); 
            flag = false; 
        }
        
    }
}
