using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSuggestion : MonoBehaviour
{                           
    // Assign in inspector
    public GameObject wilemCanvas;
    public GameObject elderCanvas;
    public GameObject labyrinthCanvas;
    public GameObject robotCanvas;
    private bool isShowing;
    public bool wilemFlag = false;
    public bool elderFlag = false;
    public bool labyrinthFlag = false;
    public bool robotFlag = false;
 
    void Update() {
        if (Input.GetKeyDown(KeyCode.H) && wilemFlag) {
            wilemCanvas.gameObject.SetActive(isShowing);
            isShowing = !isShowing;
        }
        if (Input.GetKeyDown(KeyCode.H) && elderFlag) {
            elderCanvas.gameObject.SetActive(isShowing);
            isShowing = !isShowing;
        }
        
        if (Input.GetKeyDown(KeyCode.H) && labyrinthFlag) {
            labyrinthCanvas.gameObject.SetActive(isShowing);
            isShowing = !isShowing;
        }
        
        if (Input.GetKeyDown(KeyCode.H) && robotFlag) {
            robotCanvas.gameObject.SetActive(isShowing);
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
            elderFlag = false;
        }
    }

    public void activateLabyrinthSuggestion()
    {
        Debug.Log("Attivo canvas labirinto");
        labyrinthCanvas.gameObject.SetActive(true);
        labyrinthFlag = true;
    }
    
    public void disactivateLabyrinthSuggestion()
    {
        if (labyrinthFlag == true)
        {
            labyrinthCanvas.SetActive(false);
            labyrinthFlag = false;
        }
    }
    
    public void activateRobotSuggestion()
    {
        Debug.Log("Attivo canvas robot");
        robotCanvas.gameObject.SetActive(true);
        robotFlag = true;
    }
    
    public void disactivateRobotSuggestion()
    {
        if (robotFlag == true)
        {
            robotCanvas.SetActive(false);
            robotFlag = false;
        }
    }
}