using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    public int velocity=1;
    private RaycastHit2D hit;
    public Animator animator;
    public GameObject movePoint;
    public Dialogue spaceTutorial;

    //added comment

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = movePoint.GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        //Reset Move Delta
        moveDelta = Vector3.zero;
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveDelta = new Vector3(x, y, 0);

        animator.SetFloat("Horizontal", x);
        animator.SetFloat("Vertical", y);
        animator.SetFloat("Speed", moveDelta.sqrMagnitude);

        hit = Physics2D.BoxCast(movePoint.transform.position, boxCollider.size, 0,
            new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y*Time.deltaTime*velocity),LayerMask.GetMask("Player", "Obstacle"));
        
        if(hit.collider == null) 
            transform.Translate(0,moveDelta.y*Time.deltaTime*velocity,0);
        
        hit = Physics2D.BoxCast(movePoint.transform.position, boxCollider.size, 0,
            new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x*Time.deltaTime*velocity),LayerMask.GetMask("Player", "Obstacle"));
        
        if(hit.collider == null)
            transform.Translate(moveDelta.x*Time.deltaTime*velocity,0,0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector3 GetMoveDelta()
    {
        return moveDelta;
    }
}
