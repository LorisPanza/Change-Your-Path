using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinalScene : MonoBehaviour
{
    // Start is called before the first frame update
    private int i = 0;
    public GameObject changeScene;
    void Start()
    {
        SimpleEventManager.StartListening("DennaTalk",checkCondition);
        SimpleEventManager.StartListening("AbenthyTalk",checkCondition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkCondition()
    {
        i++;

        if (i == 2)
        {
            Debug.Log("Ho parlato con i due");
            changeScene.SetActive(true);
            SimpleEventManager.StopListening("DennaTalk",checkCondition);
            SimpleEventManager.StopListening("AbenthyTalk",checkCondition);
        }
    }
}
