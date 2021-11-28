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
    public MainMenu menu;
    public AudioManager audioManager;

    public enum GameState
    {
        PlayerMode,
        MapMode
    }

    public void activatePlayerMode()
    {
        playerMode = true;
        mapCam.SetActive(false);
        //CameraManager cm=playerCam.GetComponent<CameraManager>();
        //cm.enabled = true;
        playerCam.SetActive(true);
        player.enabled=true;
        selecter.SetActive(false);
        menu.enabled = true;
    }
    
    public void activateMapMode()
    {
        playerMode = false;
        mapCam.SetActive(true);
        //CameraManager cm=playerCam.GetComponent<CameraManager>();
        playerCam.SetActive(false);
        //cm.enabled = false;
        player.enabled=false;
        selecter.SetActive(true);
        menu.enabled = false;
    }


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy (gameObject);
 
        DontDestroyOnLoad (gameObject);
 
        Debug.developerConsoleVisible = true;
        
        
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
                audioManager.Play("openMap");
                activateMapMode();
                SimpleEventManager.TriggerEvent("PlaceNewMap");
            }
            else
            {
                if (!selecter.GetComponent<SelecterMovement>().getChoosen())
                {
                    audioManager.Play("closeMap");
                    activatePlayerMode();
                }
                
            }
        }
        
    }
}
