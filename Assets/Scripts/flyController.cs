using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyController : MonoBehaviour
{
    public float speed = 0f; // Speed of movement

    private Transform xrCamera;

    void Start()
    {
        // Find the XR Camera in the XR Rig
        xrCamera = GetComponentInChildren<Camera>().transform;
    }

    void Update()
    {
        // Move in the direction the XR Camera is facing
        Vector3 moveDirection = xrCamera.forward;
        transform.position += moveDirection * speed * Time.deltaTime;
    }
}
