using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    private bool isInRange;
    public bool flag=true;

    public GameObject signCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.Space) && GameObject.Find("GameManager").GetComponent<GameManager>().getMode()==1)
        {
            if (signCanvas.activeInHierarchy)
            {
                if (flag)
                {
                    Debug.Log("Triggero inizio evento sign");
                    SimpleEventManager.TriggerEvent("SignQuest");
                    flag = false;
                }
                signCanvas.SetActive(false);
            }
            else
            {
                signCanvas.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            signCanvas.SetActive(false);
        }
    }
}
