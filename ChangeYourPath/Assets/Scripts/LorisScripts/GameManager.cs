using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mapCam;
    public GameObject playerCam;
    public Player player;
    public GameObject selecter;

    public enum GameState
    {
        PlayerMode,
        MapMode
    }

    public void activatePlayerMode()
    {
        mapCam.SetActive(false);
        playerCam.SetActive(true);
        player.enabled=true;
        selecter.SetActive(false);
        
    }
    
    public void activateMapMode()
    {
        mapCam.SetActive(true);
        playerCam.SetActive(false);
        player.enabled=false;
        selecter.SetActive(true);
        
    }


    private void Awake()
    {
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
            activateMapMode();
        }
        else
        {
            activateMapMode();
        }
    }
}
