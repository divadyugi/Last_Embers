using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    //higher value, the faster the camera snaps to target
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position+offset;
        Vector3 smoothPosition =Vector3.Lerp(transform.position,desiredPosition,smoothSpeed);
        transform.position=smoothPosition;
    }
}
