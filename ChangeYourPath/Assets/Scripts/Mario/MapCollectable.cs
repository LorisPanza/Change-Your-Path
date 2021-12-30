using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCollectable : MonoBehaviour
{
    public GameObject newMapPiece;
    public AudioManager audioManager;
    private bool flag = true;
    private MapFeatures mf;
    private IEnumerator waitForKey;
   


    private void Start()
    {
        //Debug.Log("Passo dallo start");
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
        }
    }

    IEnumerator Collectable()
    {
        //wait for space to be pressed
        while (!Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.Space) && GameObject.Find("GameManager").GetComponent<GameManager>().getMode()==2))
        {
            yield return null;
        }

        mf = newMapPiece.GetComponent<MapFeatures>();
        SimpleEventManager.StartListening("PlaceNewMap", Place);
        SimpleEventManager.TriggerEvent("PlaceNewMap");
  
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
