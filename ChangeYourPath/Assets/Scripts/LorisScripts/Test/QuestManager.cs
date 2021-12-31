using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{

    public NPC wilem;
    //public GameObject suggestion;
    public ActivateSuggestion activateSuggestion;

    public Robot robot;
    public CircleQuestConditions cqq;

    //public Labyrinth labyrinth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkActiveQuests()
    {
        if(SceneManager.GetActiveScene().name == "GameScene")
        {
            if (!wilem.quest.isComplete)
            {
                SimpleEventManager.TriggerEvent("NorthForest");
            }

            if (cqq.getIsactive())
            {
                //Debug.Log("Ho posizionato e controllo");
                SimpleEventManager.TriggerEvent("CircleQuest");
            }
        }
        
        
    }

    public void activateSuggestions()
    {
        if(SceneManager.GetActiveScene().name == "GameScene")
        {
            if (!wilem.quest.isComplete && wilem.quest.isActive)
            {
                //suggestion.SetActive(true);
                Debug.Log("Attivo i suggerimenti per Wilem");
                activateSuggestion.activateWilemSuggestion();
            }

            if (!cqq.getIscompleted() && cqq.getIsactive())
            {
                Debug.Log("Attivo i suggerimenti per Elder");
                activateSuggestion.activateElderSuggestion();
            }
        }
        else if (SceneManager.GetActiveScene().name == "SpringScene")
        {
            
            if (GameObject.Find("MapPieceLab1") && GameObject.Find("MapPieceLab1").GetComponent<Labyrinth>().enabled==true)
            {
                Debug.Log("Attivo i suggerimenti per labirinto");
                activateSuggestion.activateLabyrinthSuggestion();
            }
            if (robot.robotQuest.isActive && !robot.robotQuest.isComplete)
            {
                Debug.Log("Attivo i suggerimenti per robot");
                activateSuggestion.activateRobotSuggestion();
            }
            
            
        }
        
        
        
    }
    
    public void deactivateSuggestions()
    {
        //suggestion.SetActive(false);
        //Debug.Log("Disattivo i suggerimenti");
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            activateSuggestion.disactivateWilemSuggestion();
            activateSuggestion.disactivateElderSuggestion();
        }
        else if(SceneManager.GetActiveScene().name =="SpringScene")
        {
            activateSuggestion.disactivateLabyrinthSuggestion();
            activateSuggestion.disactivateRobotSuggestion();
        }
        
        

    }
    
    
}
