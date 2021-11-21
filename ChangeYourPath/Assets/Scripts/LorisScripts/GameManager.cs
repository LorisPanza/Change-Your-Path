using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject mapCam;
    public GameObject playerCam;
    public Player player;
    public GameObject selecter;
    private bool playerMode;

    public enum GameState
    {
        PlayerMode,
        MapMode
    }

    public void activatePlayerMode()
    {
        playerMode = true;
        mapCam.SetActive(false);
        playerCam.SetActive(true);
        player.enabled=true;
        selecter.SetActive(false);
        Debug.Log("PlayerMode");
    }
    
    public void activateMapMode()
    {
        playerMode = false;
        mapCam.SetActive(true);
        playerCam.SetActive(false);
        player.enabled=false;
        selecter.SetActive(true);
        
    }


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy (gameObject);
 
        DontDestroyOnLoad (gameObject);
 
        Debug.developerConsoleVisible = true;
        Debug.Log ("Starting");
        
        activatePlayerMode();

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (playerMode)
            {
                activateMapMode();
            }
            else
            {
                activatePlayerMode();
            }
        }
        
    }
}
