using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    

    // mode = 0: Tutorial, 1: PlayerMode, 2: MapMode
    private int mode = 1;

 
    public enum GameState
    {
        PlayerMode,
        MapMode
    }

    public void activatePlayerMode()
    {
        player.GetComponent<Animator>().enabled = true;
       
        mode = 1;
        mapCam.SetActive(false);
        playerCam.SetActive(true);
        player.enabled = true;
        selecter.SetActive(false);
        menu.enabled = true;
        
        // grey out rotate, n , m
        tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetComponent<CanvasRenderer>().SetColor(Color.grey);
        tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(7).GetComponent<CanvasRenderer>().SetColor(Color.grey);
        tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(8).GetComponent<CanvasRenderer>().SetColor(Color.grey);
        // now you can press tab
        tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(4).GetComponent<CanvasRenderer>().SetColor(Color.white);
        tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(6).GetComponent<CanvasRenderer>().SetColor(Color.white);
        // grey out space
        tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<CanvasRenderer>().SetColor(Color.grey);
        tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(10).GetComponent<CanvasRenderer>().SetColor(Color.grey);
        // collect and talk
        tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = "Collect / Talk";
    }

    public void activateMapMode()
    {
        player.GetComponent<Animator>().enabled = false;
        
        mode = 2;
        mapCam.SetActive(true);
        //Debug.Log(mapCam.activeSelf);
        playerCam.SetActive(false);
        player.enabled = false;
        selecter.SetActive(true);
        //selecter.GetComponent<SelecterMovement>().activeChoosenMap();
        menu.enabled = false;
        
        // grey out tab
        tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(4).GetComponent<CanvasRenderer>().SetColor(Color.grey);
        tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(6).GetComponent<CanvasRenderer>().SetColor(Color.grey);
        // can press space
        tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<CanvasRenderer>().SetColor(Color.white);
        tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(10).GetComponent<CanvasRenderer>().SetColor(Color.white);
        // put down and grab as text in minitutorial
        tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = "Grab / Put down";
        
    }

    public void activateTutorialMode()
    {
        tutorial.SetActive(true);
        miniTutorial.SetActive(false);
        //previous_state = mode;
        mode = 0;
        //mapCam.SetActive(false);
        //playerCam.SetActive(true);
        player.enabled = false;
        //selecter.GetComponent<SelecterMovement>().disactiveChoosenMap();
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
        
        //Debug.Log(previous_state);
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
                //Debug.Log("Sono entrato");
                activatePlayerMode();
              
               
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
        
        if(GameObject.Find("Selecter") && GameObject.Find("Selecter").transform.childCount >= 0)
        {
            // can rotate, n , m
            tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetComponent<CanvasRenderer>().SetColor(Color.white);
            tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(7).GetComponent<CanvasRenderer>().SetColor(Color.white);
            tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(8).GetComponent<CanvasRenderer>().SetColor(Color.white);
            // grey out change view
            tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetComponent<CanvasRenderer>().SetColor(Color.grey);
            tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(9).GetComponent<CanvasRenderer>().SetColor(Color.grey);

        }
        if (GameObject.Find("Selecter") && GameObject.Find("Selecter").transform.childCount == 0)
        {
            // grey out rotate, n , m
            tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetComponent<CanvasRenderer>().SetColor(Color.grey);
            tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(7).GetComponent<CanvasRenderer>().SetColor(Color.grey);
            tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(8).GetComponent<CanvasRenderer>().SetColor(Color.grey);
            // can change view
            tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetComponent<CanvasRenderer>().SetColor(Color.white);
            tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(9).GetComponent<CanvasRenderer>().SetColor(Color.white);

        }

    }
}
