using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCollectable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        SimpleEventManager.StartListening("NewPiece", NewPieceFound);
    }

    private void OnDisable()
    {
        SimpleEventManager.StopListening("NewPiece", NewPieceFound);
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SimpleEventManager.TriggerEvent("NewPiece");
        }
    }

    private void NewPieceFound()
    {
        StartCoroutine(Collectable());

    }

    IEnumerator Collectable()
    {
        //wait for space to be pressed
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }

        print("New piece collected");
        gameObject.SetActive(false);
        yield return null;

    }
}
