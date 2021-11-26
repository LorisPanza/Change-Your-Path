using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Entered");
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
            //this.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
            //this.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }
}
