using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinalScene : MonoBehaviour
{
    // Start is called before the first frame update
    private int i = 0;
    
    private int j =0;
    public GameObject changeScene;
    void Start()
    {
        SimpleEventManager.StartListening("DennaTalk",checkCondition1);
        SimpleEventManager.StartListening("AbenthyTalk",checkCondition2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkCondition1()
    {
        i++;
        SimpleEventManager.StopListening("DennaTalk",checkCondition1);
        checkCondition();
    }
    public void checkCondition2()
    {
        j++;
        SimpleEventManager.StopListening("AbenthyTalk",checkCondition2);
        checkCondition();
    }

    public void checkCondition()
    {
        if (i + j == 2)
        {
            changeScene.SetActive(true);
        }
    }
}
