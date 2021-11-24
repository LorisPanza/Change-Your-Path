using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCollectable : MonoBehaviour
{
    public GameObject newMapPiece;

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
            StartCoroutine(waitForKey);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            StopCoroutine(waitForKey);
            Debug.Log("Exited");
        }
    }

    IEnumerator Collectable()
    {
        //wait for space to be pressed
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }

        print("New piece collected");
        mf = newMapPiece.GetComponent<MapFeatures>();
        SimpleEventManager.StartListening("PlaceNewMap", Place);
        gameObject.SetActive(false);
        yield return null;

    }
 
    private void Place()
    {
        mf.placeNewMap();
        SimpleEventManager.StopListening("PlaceNewMap", Place);
        newMapPiece.SetActive(true);
    }
}
