using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public Image controller;
    public AudioManager audioManager;
    public GameObject kvothe;
    private SavedMap[] mapPieces;
    public List<GameObject> chapter1;
    public NPC npc;
    public GameObject endPrototypeCanvas;
    public GameObject houseForest;
    public GameObject elderNPC;
    public GameObject tm;
    public CircleQuestConditions cqq;

    public List<GameObject> mapCollectable;

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            InitPieces();
            if (PlayerPrefs.HasKey("Master volume"))
            {
                LoadSettings();
                if (PlayerPrefs.HasKey("KvotheX"))
                {
                    LoadGame();
                }
            }
            else
            {
                audioManager.SetDefault();
            }

        }
        else
        {
            if (PlayerPrefs.HasKey("Master volume"))
            {
                LoadSettings();
            }
            else
            {
                audioManager.SetDefault();
            }
        }
    }

    void InitPieces()
    {
        chapter1[0].GetComponent<MapFeatures>().tileMap.up = "Water";
        chapter1[0].GetComponent<MapFeatures>().tileMap.right = "Water";
        chapter1[0].GetComponent<MapFeatures>().tileMap.left = "Water";
        chapter1[0].GetComponent<MapFeatures>().tileMap.down = "Snow";

        chapter1[1].GetComponent<MapFeatures>().tileMap.up = "Water";
        chapter1[1].GetComponent<MapFeatures>().tileMap.right = "Snow";
        chapter1[1].GetComponent<MapFeatures>().tileMap.left = "Snow";
        chapter1[1].GetComponent<MapFeatures>().tileMap.down = "Water";

        chapter1[2].GetComponent<MapFeatures>().tileMap.up = "Water";
        chapter1[2].GetComponent<MapFeatures>().tileMap.right = "Water";
        chapter1[2].GetComponent<MapFeatures>().tileMap.left = "Water";
        chapter1[2].GetComponent<MapFeatures>().tileMap.down = "Snow";

        chapter1[3].GetComponent<MapFeatures>().tileMap.up = "Snow";
        chapter1[3].GetComponent<MapFeatures>().tileMap.right = "Snow";
        chapter1[3].GetComponent<MapFeatures>().tileMap.left = "Snow";
        chapter1[3].GetComponent<MapFeatures>().tileMap.down = "Snow";

        //Forest 6
        chapter1[4].GetComponent<MapFeatures>().tileMap.up = "Water";
        chapter1[4].GetComponent<MapFeatures>().tileMap.right = "Forest";
        chapter1[4].GetComponent<MapFeatures>().tileMap.left = "Snow";
        chapter1[4].GetComponent<MapFeatures>().tileMap.down = "Forest";
        //Forest 7
        chapter1[5].GetComponent<MapFeatures>().tileMap.up = "Forest";
        chapter1[5].GetComponent<MapFeatures>().tileMap.right = "Forest";
        chapter1[5].GetComponent<MapFeatures>().tileMap.left = "Snow";
        chapter1[5].GetComponent<MapFeatures>().tileMap.down = "Snow";
        //Forest 8
        chapter1[6].GetComponent<MapFeatures>().tileMap.up = "Snow";
        chapter1[6].GetComponent<MapFeatures>().tileMap.right = "Forest";
        chapter1[6].GetComponent<MapFeatures>().tileMap.left = "Snow";
        chapter1[6].GetComponent<MapFeatures>().tileMap.down = "Forest";
        //forest 9
        chapter1[7].GetComponent<MapFeatures>().tileMap.up = "Forest";
        chapter1[7].GetComponent<MapFeatures>().tileMap.right = "Snow";
        chapter1[7].GetComponent<MapFeatures>().tileMap.left = "Forest";
        chapter1[7].GetComponent<MapFeatures>().tileMap.down = "Snow";
        
        //map10
        chapter1[8].GetComponent<MapFeatures>().tileMap.up = "River";
        chapter1[8].GetComponent<MapFeatures>().tileMap.right = "River";
        chapter1[8].GetComponent<MapFeatures>().tileMap.left = "Snow";
        chapter1[8].GetComponent<MapFeatures>().tileMap.down = "Snow";
        
        //map11
        chapter1[9].GetComponent<MapFeatures>().tileMap.up = "Snow";
        chapter1[9].GetComponent<MapFeatures>().tileMap.right = "Snow";
        chapter1[9].GetComponent<MapFeatures>().tileMap.left = "River";
        chapter1[9].GetComponent<MapFeatures>().tileMap.down = "River";
        
        //map12
        chapter1[10].GetComponent<MapFeatures>().tileMap.up = "River";
        chapter1[10].GetComponent<MapFeatures>().tileMap.right = "River";
        chapter1[10].GetComponent<MapFeatures>().tileMap.left = "Snow";
        chapter1[10].GetComponent<MapFeatures>().tileMap.down = "Snow";
        
        //map13
        chapter1[11].GetComponent<MapFeatures>().tileMap.up = "Snow";
        chapter1[11].GetComponent<MapFeatures>().tileMap.right = "River";
        chapter1[11].GetComponent<MapFeatures>().tileMap.left = "Snow";
        chapter1[11].GetComponent<MapFeatures>().tileMap.down = "River";
        
        
    }

    public void LoadSettings()
    {
        float master = PlayerPrefs.GetFloat("Master volume");
        AudioListener.volume = master;
        audioManager.masterLevel.value = master;

        float music = PlayerPrefs.GetFloat("Music volume");
        audioManager.SetMusicVolume(music);
        audioManager.musicLevel.value = music;

        float control = PlayerPrefs.GetFloat("Control volume");
        audioManager.SetControlVolume(control);
        audioManager.controlLevel.value = control;

        float ambient = PlayerPrefs.GetFloat("Ambient volume");
        //Set ambient level
        audioManager.ambientLevel.value = ambient;

        float brightness = PlayerPrefs.GetFloat("Brightness");
        double i = 0.25;
        if (brightness > 0.25)
            i = 0.25 - (brightness - 0.25);
        else if (brightness < 0.25)
            i = 0.25 + (0.25 - brightness);
        controller.color = new Color(0, 0, 0, (float)i);
        audioManager.brightnessLevel.value = brightness;
    }

    public void LoadGame()
    {
        float KvotheX = PlayerPrefs.GetFloat("KvotheX");
        float KvotheY = PlayerPrefs.GetFloat("KvotheY");
        kvothe.transform.position = new Vector3(KvotheX, KvotheY,0);
       
       // Debug.Log("kvothe position: "+ KvotheX+" - "+KvotheY);

        string mapState = PlayerPrefs.GetString("Map state");
        SavedMap[] map = JsonHelper.FromJson<SavedMap>(mapState);
        //foreach (SavedMap item in map)
        //{
        //    Debug.Log(item.ToString());
        //}
        foreach (SavedMap item in map)
        {
            string title = item.title;
            GameObject piece = null;
            foreach (GameObject go in chapter1)
            {
                if (go.name == title) piece = go;

            }
            piece.SetActive(true);
            piece.transform.position = new Vector3(item.mapPositionX, item.mapPositionY, 0);
            piece.transform.Find("UpBoundary").gameObject.SetActive(item.upBoundary);
            piece.transform.Find("DownBoundary").gameObject.SetActive(item.downBoundary);
            piece.transform.Find("LeftBoundary").gameObject.SetActive(item.leftBoundary);
            piece.transform.Find("RightBoundary").gameObject.SetActive(item.rightBoundary);
            MapMovement mapMov = piece.GetComponent<MapMovement>();
            if (item.rotation == 90)
            {
                mapMov.rotateCounterClockwise();
            }
            else if (item.rotation == 180)
            {
                mapMov.rotateCounterClockwise();
                mapMov.rotateCounterClockwise();
            }
            else if (item.rotation == -90 || item.rotation == 270)
            {
                mapMov.rotateClockwise();
            }
            //piece.transform.rotation = Quaternion.Euler(0, 0, item.rotation);
            
        }

        if (PlayerPrefs.HasKey("activeCollectable"))
        {
            foreach (GameObject coll in mapCollectable)
            {
                //Log(coll.name);
                if (coll.name == PlayerPrefs.GetString("activeCollectable"))
                {
                    
                    Debug.Log(coll.name);
                    coll.SetActive(true);
                }
                else
                {
                    if (coll.transform.parent.gameObject.activeSelf)
                    {
                        coll.SetActive(false);
                    }
                }
            }
        }
        else
        {
            foreach (GameObject coll in mapCollectable)
            {
                coll.SetActive(false);
            }
        }

        loadForestQuest();
        loadElderQuest();

        int index=PlayerPrefs.GetInt("Index");
        if (index < 8) tm.GetComponent<TutorialManager>().setIndex(index);
        else tm.GetComponent<TutorialManager>().enabled = false;
    }

    //Saves the player data
    public void SaveSettings()
    {
        float master = AudioListener.volume;
        PlayerPrefs.SetFloat("Master volume", master);

        float music = audioManager.musicLevel.value;
        PlayerPrefs.SetFloat("Music volume", music);

        float control = audioManager.controlLevel.value;
        PlayerPrefs.SetFloat("Control volume", control);

        float ambient = audioManager.ambientLevel.value;
        PlayerPrefs.SetFloat("Ambient volume", ambient);

        float brightness = audioManager.brightnessLevel.value;
        PlayerPrefs.SetFloat("Brightness", brightness);

        PlayerPrefs.Save();
       // Debug.Log("SAVED");
    }

    public void SaveGame()
    {
        
        Vector3 Kvotheposition = kvothe.transform.position;
        PlayerPrefs.SetFloat("KvotheX", Kvotheposition.x);
        PlayerPrefs.SetFloat("KvotheY", Kvotheposition.y);

        int numPieces = GameObject.FindGameObjectsWithTag("MapPiece").Length;
        GameObject[] maps;
        maps = new GameObject[numPieces];
        mapPieces = new SavedMap[numPieces];
        maps = GameObject.FindGameObjectsWithTag("MapPiece");

        if (tm.GetComponent<TutorialManager>().enabled) { PlayerPrefs.SetInt("Index", tm.GetComponent<TutorialManager>().getIndex()); }
        else { PlayerPrefs.SetInt("Index", 8); }

        for (int i = 0; i < numPieces; i++)
        {
            SavedMap sm = new SavedMap();
            sm.title = maps[i].name;
            sm.mapPositionX = maps[i].transform.position.x;
            sm.mapPositionY = maps[i].transform.position.y;
            sm.rotation = maps[i].transform.rotation.eulerAngles.z;
            sm.upBoundary = maps[i].transform.Find("UpBoundary").gameObject.activeSelf;
            sm.downBoundary = maps[i].transform.Find("DownBoundary").gameObject.activeSelf;
            sm.leftBoundary = maps[i].transform.Find("LeftBoundary").gameObject.activeSelf;
            sm.rightBoundary = maps[i].transform.Find("RightBoundary").gameObject.activeSelf;
            mapPieces[i] = sm;
        }

        string mapToJson = JsonHelper.ToJson(mapPieces, true);
        PlayerPrefs.SetString("Map state", mapToJson);

        GameObject[] mapCollectableSave = GameObject.FindGameObjectsWithTag("MapCollectable");

        foreach (GameObject go in mapCollectableSave)
        {
            Debug.Log(go.name);
        }

        if (mapCollectableSave.Length != 0)
        {
            PlayerPrefs.SetString("activeCollectable", mapCollectableSave[0].name);
            
            //Debug.Log(mapCollectableSave[0].name);
        }
        
        saveForestQuest();
        saveElderQuest();
    }

    public void Save()
    {
        PlayerPrefs.DeleteAll();
        SaveSettings();
        SaveGame();
    }

    public void saveForestQuest()
    {
        if (npc.quest.isComplete)
        {
            //Debug.Log("Salvo la quest completa");
            PlayerPrefs.SetString("ForestQuestCompleted", "Completed");
            PlayerPrefs.SetString("isActiveForest", "False");
        }
        else
        {
            if (npc.quest.isActive)
            {
                //Debug.Log("La quest è attiva ma non completa");
                PlayerPrefs.SetString("isActiveForest", "True");
            }
            else
            {
                PlayerPrefs.SetString("isActiveForest", "False");
            }
        }
        

        
        
    }

    public void loadForestQuest()
    {
        if (PlayerPrefs.HasKey("ForestQuestCompleted") && PlayerPrefs.GetString("isActiveForest")=="False")
        {
            //Debug.Log("Carico quest completa");
                //npc.quest.Complete();
                npc.quest.isActive = false;
                npc.quest.isComplete = true;
                houseForest.SetActive(true);
                
                //endPrototypeCanvas.SetActive(true);
                //Debug.Log("La quest è completa");
        }
           
        
        if (PlayerPrefs.GetString("isActiveForest")=="True")
        {
            //Debug.Log("Carico quest attiva ma non completa");
            npc.quest.isComplete = false;
            npc.quest.isActive = true;
            
        }
    }

    public void saveElderQuest()
    {
        if (cqq.getIscompleted() == true)
        {
            //Debug.Log("La quest elder è completa --> SALVO");
            PlayerPrefs.SetString("ElderQuestCompleted", "Completed");
            PlayerPrefs.SetString("isActiveElder", "False");
        }
        else
        {
            if (cqq.getIsactive()==true)
            {
                //Debug.Log("La quest elder è attiva ma non completa");
                PlayerPrefs.SetString("isActiveElder", "True");
                if (cqq.checkCondition())
                {
                    //Debug.Log("La condizione è salvata");
                    PlayerPrefs.SetString("checkCondition","True");
                }
            }
            else
            {
                PlayerPrefs.SetString("isActiveElder", "False");
            }
        }
    }
    
    public void loadElderQuest()
    {
        if (PlayerPrefs.HasKey("ElderQuestCompleted") && PlayerPrefs.GetString("isActiveElder")=="False")
        {
            //Debug.Log("Carico quest elder completa");
            //npc.quest.Complete();
            cqq.setIsActive(false);
            cqq.setIsComplete(true);
            elderNPC.SetActive(true);

            //endPrototypeCanvas.SetActive(true);
            //Debug.Log("La quest è completa");
        }
        
        if (PlayerPrefs.GetString("isActiveElder")=="True")
        {
            //Debug.Log("Carico quest elder attiva ma non completa");
            
            cqq.setIsComplete(false);
            cqq.setIsActive(true);
            if (PlayerPrefs.HasKey("checkCondition"))
            {
                //Debug.Log("carico la condizione");
                elderNPC.SetActive(true);
            }
            
            

        }
    }
}
