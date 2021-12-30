using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitch : MonoBehaviour
{
   public void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         deletePreviousMemory();
         SceneManager.LoadScene("SpringScene");
         
      }

   }
   
   private void deletePreviousMemory()
   {
      PlayerPrefs.DeleteKey("KvotheX");
      PlayerPrefs.DeleteKey("KvotheY");
      PlayerPrefs.DeleteKey("Map state");
      PlayerPrefs.DeleteKey("activeCollectable");
      
   }
}
