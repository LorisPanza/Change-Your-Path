using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
    public GameObject lastSelecterPosition;
    public QuestManager questManager;
    public Robot robot;


    // mode = 1: PlayerMode, 2: MapMode
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
        if (SceneManager.GetActiveScene().name == "SpringScene") robot.enabled = true;
        lastSelecterPosition.transform.position = new Vector3(selecter.transform.position.x,
            selecter.transform.position.y, selecter.transform.position.z);
        selecter.SetActive(false);
        menu.enabled = true;
        
        questManager.deactivateSuggestions();

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
        if (SceneManager.GetActiveScene().name == "SpringScene") robot.enabled = false;
        selecter.SetActive(true);
        //selecter.GetComponent<SelecterMovement>().activeChoosenMap();
        menu.enabled = false;

        questManager.activateSuggestions();
        // grey out tab
        tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(4).GetComponent<CanvasRenderer>().SetColor(Color.grey);
        tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(6).GetComponent<CanvasRenderer>().SetColor(Color.grey);
        // can press space
        tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<CanvasRenderer>().SetColor(Color.white);
        tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(10).GetComponent<CanvasRenderer>().SetColor(Color.white);
        // put down and grab as text in minitutorial
        tutorial.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = "Grab / Put down";

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

        activatePlayerMode();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (mode == 1)
            {
                audioManager.Play("openMap");
                activateMapMode();
                //SimpleEventManager.TriggerEvent("PlaceNewMap"); 
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


        if (GameObject.Find("Selecter") && GameObject.Find("Selecter").transform.childCount >= 0)
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
