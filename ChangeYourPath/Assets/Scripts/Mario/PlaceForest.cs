using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceForest : MonoBehaviour
{
    public GameObject forest1;
    public GameObject forest2;
    public GameObject forest3;
    public GameObject forest4;

    public AudioManager audioManager;
    public GameObject canvasNewPiece;

    private MapFeatures mf1;
    private MapFeatures mf2;
    private MapFeatures mf3;
    private MapFeatures mf4;
    private IEnumerator waitForKey;


    private void Start()
    {
        waitForKey = Collectable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvasNewPiece.SetActive(true);
            StartCoroutine(waitForKey);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvasNewPiece.SetActive(false);
            StopCoroutine(waitForKey);
        }
    }

    IEnumerator Collectable()
    {
        //wait for space to be pressed
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }

        mf1 = forest1.GetComponent<MapFeatures>();
        mf2 = forest2.GetComponent<MapFeatures>();
        mf3 = forest3.GetComponent<MapFeatures>();
        mf4 = forest4.GetComponent<MapFeatures>();
        SimpleEventManager.StartListening("PlaceNewMap", Place1);
        SimpleEventManager.StartListening("PlaceNewMap", Place2);
        SimpleEventManager.StartListening("PlaceNewMap", Place3);
        SimpleEventManager.StartListening("PlaceNewMap", Place4);

        gameObject.SetActive(false);
        audioManager.Play("mapChoice");
        yield return null;
    }

    private void Place1()
    {
        mf1.placeNewMap();
        SimpleEventManager.StopListening("PlaceNewMap", Place1);
        forest1.SetActive(true);
    }

    private void Place2()
    {
        mf2.placeNewMap();
        SimpleEventManager.StopListening("PlaceNewMap", Place2);
        forest2.SetActive(true);
    }

    private void Place3()
    {
        mf3.placeNewMap();
        SimpleEventManager.StopListening("PlaceNewMap", Place3);
        forest3.SetActive(true);
    }

    private void Place4()
    {
        mf4.placeNewMap();
        SimpleEventManager.StopListening("PlaceNewMap", Place4);
        forest4.SetActive(true);
    }
}
