using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform focus;
    public float smoothTime = 1;

    Vector3 offset;

    void Awake()
    {
        offset = focus.position - transform.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, focus.position - offset, Time.deltaTime * smoothTime);
    }
}
