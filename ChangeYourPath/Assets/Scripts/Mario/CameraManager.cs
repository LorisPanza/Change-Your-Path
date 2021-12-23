using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform focus;
    public float smoothTime = 2;

    Vector3 offset;

    void Awake()
    {
        transform.position = new Vector3(focus.position.x, focus.position.y, transform.position.z);
        offset = focus.position - transform.position;
        Debug.Log("target pos: "+focus.position.x+" - "+focus.position.y);
        
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, focus.position - offset, Time.deltaTime * smoothTime);
    }
}
