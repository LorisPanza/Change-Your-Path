using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileMovement : MonoBehaviour
{
    private bool isMoving;

    private Vector3 origPos, targetPos;
    private int offset = 18;
    private BoxCollider2D boxCollider;
    private float timeToMove = 0.2f;
    private bool hit;
    [SerializeField] private LayerMask map;
    
    
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") < 0 && !isMoving)
            StartCoroutine(MoveMap(Vector3.left));
        if (Input.GetAxisRaw("Vertical") < 0 && !isMoving)
            StartCoroutine(MoveMap(Vector3.down));
        if (Input.GetAxisRaw("Horizontal") > 0 && !isMoving)
            StartCoroutine(MoveMap(Vector3.right));
        if (Input.GetAxisRaw("Vertical") > 0 && !isMoving)
            StartCoroutine(MoveMap(Vector3.up));
    }

    private IEnumerator MoveMap(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        origPos = transform.position;
        targetPos= origPos + direction*offset;
        
        hit = CanMove(direction, origPos);
        
        if (!hit)
        {
            while (elapsedTime < timeToMove)
            {
                transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPos;
        }
       
        isMoving = false;

    }

    private bool CanMove(Vector3 direction,Vector3 origPos)
    {
      return Physics2D.BoxCast(origPos, boxCollider.size, 0,
            (Vector2)direction,12 , map);
       
    }
}
