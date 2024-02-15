using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class FollowingController : MonoBehaviour
{

    public Transform targetObject; // The following object
    public float maxDistance = 100f; // Maximum allowed distance in 3D space

    // ------ fade ------
    public FadeEffect fadeEffect;
    private Transform restorePoint; // To store the restorePoint Transform
    private flyController flyController;


    void Start()
    {
        flyController = GetComponent<flyController>();
        // Check if the target object has any children
        if (targetObject.childCount > 0)
        {
            // Get the first child of the targetObject
            restorePoint = targetObject.GetChild(0).GetComponent<Transform>();
        }
        else
        {
            Debug.LogError("Target object has no children.");
        }
    }


    void Update()
    {
        // Check distance from the target object
        if (Vector3.Distance(transform.position, targetObject.position) > maxDistance && restorePoint != null)
        {
            Debug.Log("Lost!");
            StartCoroutine(fadeEffect.FadeOut(() => {
                // Set the XR Origin's position and rotation to that of the restorePoint
                transform.position = restorePoint.position;
                transform.rotation = restorePoint.rotation;
                flyController.speed = 0;
            }));
        }
    }
}
