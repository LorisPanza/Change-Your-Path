using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CircleQuest : MonoBehaviour
{
    private CircleQuestConditions state;
    public GameObject oldMan;

    public AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        SimpleEventManager.StartListening("CircleQuest", CheckIsActive);  //lo ascolta dal selecter solo se la quest è attiva.
        state = this.GetComponent<CircleQuestConditions>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void CheckIsActive()
    {
        if (state.getIsactive())
        {
            if (state.checkCondition())
            {
                //Debug.Log("Disattivo la quest");
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
            //controlla se attiva--> deve avere un riferimento a qualcuno che sa quando è attiva
    }
}
