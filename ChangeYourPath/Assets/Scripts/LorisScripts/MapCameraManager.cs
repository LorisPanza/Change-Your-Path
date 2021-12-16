using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraManager : MonoBehaviour
{
    public Transform target;
    private float smoothTime = 0.125f;

    public Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition= Vector3.Lerp(transform.position,desiredPosition,smoothTime);
        transform.position= smoothedPosition;
        //transform.LookAt(target);
    }

    void Awake()
    {
       
    }

    void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, focus.position - offset, Time.deltaTime * smoothTime);
    }
}
