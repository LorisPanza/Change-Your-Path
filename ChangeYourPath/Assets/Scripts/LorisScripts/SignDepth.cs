using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignDepth : MonoBehaviour
{
   
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.gameObject.transform.parent.gameObject.GetComponent<SpriteRenderer>().sortingOrder =
                    this.transform.parent.gameObject.GetComponent<SpriteRenderer>().sortingOrder - 1;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.gameObject.transform.parent.gameObject.GetComponent<SpriteRenderer>().sortingOrder =
                    this.transform.parent.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
            }
        }
}
