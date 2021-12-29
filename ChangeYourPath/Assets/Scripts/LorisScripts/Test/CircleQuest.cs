using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CircleQuest : MonoBehaviour
{
    private CircleQuestConditions state;
    public GameObject oldMan;
    public GameObject mapCollectable13;

    public AudioManager audioManager;
    // Start is called before the first frame update

   

    void Start()
    {
        SimpleEventManager.StartListening("CircleQuest", CheckIsActive);
        //lo ascolta dal selecter solo se la quest è attiva.
        state = this.GetComponent<CircleQuestConditions>();
        //Debug.Log(state.getIscompleted());
        if (!state.getIscompleted())
        {
            SimpleEventManager.StartListening("EndElderQuest", endQuest);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CheckIsActive()
    {
        //Debug.Log("é attivo");
        if (state.getIsactive())
        {
            
            if (state.checkCondition())
            {
                Debug.Log("CondizioneSoddisfatta");
                //SimpleEventManager.StopListening("CircleQuest",CheckIsActive);
                
                audioManager.Play("QuestCompleted");
                oldMan.SetActive(true);
                
                
                //state.setIsActive(false);
                
            }
            else
            {
                
                oldMan.SetActive(false);
                //Debug.Log("Mission failed: continuo ad ascoltare il selecter");
            }
        }
            //controlla se attiva --> deve avere un riferimento a qualcuno che sa quando è attiva
    }

    public void endQuest()
    {
        SimpleEventManager.StopListening("CircleQuest",CheckIsActive);
        SimpleEventManager.StopListening("EndElderQuest",endQuest);
        Debug.Log("Missione terminata vecchio");
        mapCollectable13.SetActive(true);
        
        state.setIsActive(false);
        state.setIsComplete(true);
    }
}
