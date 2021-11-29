using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SaveManager : MonoBehaviour
{
    public Image controller;
    public AudioManager audioManager;
    public GameObject kvothe;
    private SavedMap[] mapPieces;
    public List<GameObject> chapter1;

    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {

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
        kvothe.transform.position = new Vector3(KvotheX, KvotheY, 0);

        string mapState = PlayerPrefs.GetString("Map state");
        SavedMap[] map = JsonHelper.FromJson<SavedMap>(mapState);
        foreach (SavedMap item in map)
        {
            Debug.Log(item.ToString());
        }
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
        Debug.Log("SAVED");
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

        for (int i = 0; i < numPieces; i++)
        {
            SavedMap sm = new SavedMap();
            sm.title = maps[i].name;
            sm.mapPositionX = maps[i].transform.position.x;
            sm.mapPositionY = maps[i].transform.position.y;
            sm.rotation = maps[i].transform.rotation.eulerAngles.z;
            mapPieces[i] = sm;
        }

        string mapToJson = JsonHelper.ToJson(mapPieces, true);
        PlayerPrefs.SetString("Map state", mapToJson);
    }

    public void Save()
    {
        SaveSettings();
        SaveGame();
    }
}
