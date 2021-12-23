using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialCanvasOrdered;
    //public GameObject secondMap;
    private int index;


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < tutorialCanvasOrdered.Length; i++)
        {
            tutorialCanvasOrdered[i].SetActive(false);
        }

        if (index < tutorialCanvasOrdered.Length)
        {
            tutorialCanvasOrdered[index].SetActive(true);
        }

        if (index == 0)
        {
            //Movement with arrows
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                index++;
            }
        } else if (index == 1) 
        {
            //Collect with space
            if (!GameObject.Find("MapCollectable1"))
            {
                index++;
            }

        } else if(index == 2)
        {
            //Press TAB to open map mode
            if (GameObject.Find("MapPiece 2"))
            {
                Debug.Log("MapPiece 2");
                index++;
            }
        } else if(index == 3)
        {
            //Press space to grab
            if (GameObject.Find("Selecter") && GameObject.Find("Selecter").transform.childCount > 0)
            {
                index++;
            }
        } else if (index == 4)
        {
            //Movement with arrows
            if (GameObject.Find("Selecter") && GameObject.Find("Selecter").transform.childCount > 0 &&
                (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)))
            {
                index++;
            }
        }
        else if (index == 5)
        {
            //Rotate with N and M
            if (GameObject.Find("Selecter") && GameObject.Find("Selecter").transform.childCount > 0 &&
                (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.M)))
            {
                index++;
            }
        }
        else if (index == 6)
        {
            //Release with space
            if (GameObject.Find("Selecter") && GameObject.Find("Selecter").transform.childCount == 0)
            {
                index++;
            }
        }
        else if (index == 7)
        {
            //Return in player mode with TAB
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                index++;
            }
        }
    }

    public int getIndex()
    {
        return index;
    }
    
    public void setIndex(int value)
    {
        index=value;
    }
}
