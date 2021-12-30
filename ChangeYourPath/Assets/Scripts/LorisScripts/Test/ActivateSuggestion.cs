using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSuggestion : MonoBehaviour
{                           
    // Assign in inspector
    public GameObject wilemCanvas;
    public GameObject elderCanvas;
    private bool isShowing;
    public bool wilemFlag = false;
    public bool elderFlag = false;
 
    void Update() {
        if (Input.GetKeyDown(KeyCode.H) && wilemFlag) {
            wilemCanvas.gameObject.SetActive(isShowing);
            isShowing = !isShowing;
        }
        if (Input.GetKeyDown(KeyCode.H) && elderFlag) {
            elderCanvas.gameObject.SetActive(isShowing);
            isShowing = !isShowing;
        }
    }

    private void Start()
    {
        isShowing = false;
    }

    public void activateWilemSuggestion()
    {
        //Debug.Log("Attivo suggestion Wilem");
        wilemCanvas.SetActive(true);
        wilemFlag = true;
    }

    public void disactivateWilemSuggestion()
    {
        if (wilemFlag == true)
        { 
            //Debug.Log("Chiudo il suggester di wilem");
            wilemCanvas.SetActive(false); 
            wilemFlag = false; 
        }
        
    }
    
    public void activateElderSuggestion()
    {
        elderCanvas.SetActive(true);
        elderFlag = true;
    }
    
    public void disactivateElderSuggestion()
    {
        if (elderFlag == true)
        {
            elderCanvas.SetActive(false);
            elderFlag = true;
        }
    }
}
