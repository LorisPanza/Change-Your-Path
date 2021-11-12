using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecterMovement : MonoBehaviour
{
    public float moveSpeed = 100f;
    public LayerMask detectedLayer;
    private int offsetMovement=18;

    private bool choosen = false;
    //private int offsetMatching = 18;
    private Collider2D mapCollider,colliderMovement; //colliderMatchingUp,colliderMatchingDown,colliderMatchingRight,colliderMatchingLeft;
    public Transform movePoint;
    //private MapFeatures collideMap,thisMap;
    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && choosen == false) 
        {
            mapCollider = Physics2D.OverlapCircle(movePoint.position, .2f, detectedLayer);
            if (mapCollider)
            {
                mapCollider.gameObject.GetComponent<MapMovement>().enabled = true;
                choosen = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && choosen == true)
        {
            if (mapCollider.gameObject.GetComponent<MapMovement>().checkMatching(mapCollider.gameObject.GetComponent<MapMovement>().movePoint))
            {
                mapCollider.gameObject.GetComponent<MapMovement>().enabled = false;
                choosen = false;
            }
            else
            {
                Debug.Log("Non puoi metterlo");
            }
            
        }
        

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (!choosen)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * offsetMovement, 0f, 0f);

                }

                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * offsetMovement, 0f);
                }
            }
            if (choosen)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    //Debug.Log("Muovo orizzontale");
                    colliderMovement = Physics2D.OverlapCircle(
                        movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * offsetMovement, 0f, 0f), .2f, detectedLayer);
                    if (!colliderMovement)
                    {
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * offsetMovement, 0f, 0f);
                       
                    }
               
                } 
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    //Debug.Log("Muovo Verticale");
                    colliderMovement = Physics2D.OverlapCircle(
                        movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * offsetMovement, 0f) , .2f, detectedLayer);
                    if (!colliderMovement)
                    {
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * offsetMovement, 0f);
                       
                    }
                
                }
            }
            
        }
    }
}
