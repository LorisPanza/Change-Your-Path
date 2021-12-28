using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSuggestion : MonoBehaviour
{                           
    // Assign in inspector
    public GameObject canvas;
    private bool isShowing;
 
    void Update() {
        if (Input.GetKeyDown(KeyCode.H)) {
            isShowing = !isShowing;
            canvas.gameObject.SetActive(isShowing);
        }
    }
}
