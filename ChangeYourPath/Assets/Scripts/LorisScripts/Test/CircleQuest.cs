using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleQuest : MonoBehaviour
{
    public CircleQuestConditions state;
    // Start is called before the first frame update
    void Start()
    {
        SimpleEventManager.StartListening("CircleQuest", CheckIsActive);  //lo ascolta dal selecter solo se la quest è attiva.
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
                Debug.Log("Disattivo la quest");
                SimpleEventManager.StopListening("CircleQuest",CheckIsActive);
                state.setIsActive(false);
                
            }
            else
            {
                //Debug.Log("Mission failed: continuo ad ascoltare il selecter");
            }
        }
            //controlla se attiva--> deve avere un riferimento a qualcuno che sa quando è attiva
    }
}
