using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeFinalScene : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //deletePreviousMemory();
            GameObject.Find("SaveManager").GetComponent<SaveManager2>().Save();
            SceneManager.LoadScene("FinalScene");
         
        }

    }
   
    //private void deletePreviousMemory()
    //{
        //PlayerPrefs.DeleteAll();
    //}
}
