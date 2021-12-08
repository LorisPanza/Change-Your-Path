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
    public MainMenu menu;
    public AudioManager audioManager;
    public GameObject tutorial;
    public GameObject miniTutorial;
    public GameObject canvasMenu;
    public GameObject canvasPressTab;
    public GameObject GrabCanvas;
    private int previous_state;

    // mode = 0: Tutorial, 1: PlayerMode, 2: MapMode
    private int mode = 1;

 
    public enum GameState
    {
        PlayerMode,
        MapMode
    }

    public void activatePlayerMode()
    {
        mode = 1;
        mapCam.SetActive(false);
        playerCam.SetActive(true);
        player.enabled = true;
        selecter.SetActive(false);
        menu.enabled = true;
    }

    public void activateMapMode()
    {
        mode = 2;
        mapCam.SetActive(true);
        //Debug.Log(mapCam.activeSelf);
        playerCam.SetActive(false);
        player.enabled = false;
        selecter.SetActive(true);
        menu.enabled = false;
    }

    public void activateTutorialMode()
    {
        tutorial.SetActive(true);
        miniTutorial.SetActive(false);
        previous_state = mode;
        mode = 0;
        //mapCam.SetActive(false);
        //playerCam.SetActive(true);
        player.enabled = false;
        selecter.SetActive(false);
        menu.enabled = false;
    }


    private void Awake()
    {
        //if (instance == null)
        //    instance = this;
        //else if (instance != this)
        //    Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);

        //Debug.developerConsoleVisible = true;

        //activateTutorialMode();

    }


    // Start is called before the first frame update
    void Start()
    {
        previous_state = 1;
        Debug.Log(previous_state);
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);

        Debug.developerConsoleVisible = true;

        mapCam.SetActive(false);
        playerCam.SetActive(true);
        activateTutorialMode();
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == 0)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                //Debug.Log("Mode0");
                tutorial.SetActive(false);
                miniTutorial.SetActive(true);
                if (previous_state == 1)
                {
                    //Debug.Log("Sono entrato");
                    activatePlayerMode();
                }
                else if(previous_state == 2)
                {
                    activateMapMode();
                }

                menu.enabled = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (mode == 1)
                {
                    audioManager.Play("openMap");
                    activateMapMode();
                    SimpleEventManager.TriggerEvent("PlaceNewMap");
                    if (canvasPressTab.activeSelf == true)
                    {
                        canvasPressTab.SetActive(false);
                        GrabCanvas.SetActive(true);
                    }
                }
                else if (mode == 2)
                {
                    if (!selecter.GetComponent<SelecterMovement>().getChoosen())
                    {
                        audioManager.Play("closeMap");
                        activatePlayerMode();
                    }

                }
            }
            bool menuActive = menu.transform.Find("MainMenu").gameObject.activeSelf;
            bool settingsActive = menu.transform.Find("SettingsMenu").gameObject.activeSelf;
            bool quitActive = menu.transform.Find("QuitMenu").gameObject.activeSelf;
            if ( !menuActive && !settingsActive && !quitActive && Input.GetKeyDown(KeyCode.T))
            {
                //Debug.Log(" Menu: " + menuActive + "   quit:  " + quitActive + "  settings:  " + settingsActive);
                tutorial.SetActive(true);
                miniTutorial.SetActive(false);
                activateTutorialMode();
            }
        }

    }
}
