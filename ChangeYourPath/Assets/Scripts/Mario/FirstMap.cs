using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMap : MonoBehaviour
{
    public GameObject newMapPiece;
    public AudioManager audioManager;
    public GameObject canvasNewPiece;
    public GameObject canvasPressTab;

    private MapFeatures mf;
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
            canvasPressTab.SetActive(true);
        }
    }

    IEnumerator Collectable()
    {
        //wait for space to be pressed
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }

        mf = newMapPiece.GetComponent<MapFeatures>();
        SimpleEventManager.StartListening("PlaceNewMap", Place);

        gameObject.SetActive(false);
        audioManager.Play("mapChoice");
        yield return null;
    }

    private void Place()
    {
        mf.placeNewMap();
        SimpleEventManager.StopListening("PlaceNewMap", Place);
        newMapPiece.SetActive(true);
    }
}

