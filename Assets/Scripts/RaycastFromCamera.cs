using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastFromCameraWithTag : MonoBehaviour
{
    public float maxRayDistance = 100.0f; // Maximum distance of the ray
    public string targetTag = "Interactable"; // Tag to identify interactable objects
    public TriggerInputDetector triggerInputDetector;
    private GameObject lastHitObject = null; // Keep track of the last object hit by the ray
    private Camera camera;

    public float moveSpeed = 20.0f; // Speed at which to move towards the object

    void Start()
    {
        // Find the camera in the children of this GameObject
        camera = GetComponentInChildren<Camera>();
        if (camera == null)
        {
            Debug.LogError("Camera not found in the children of the GameObject.");
        }
    }

    void Update()
    {
        if (camera != null)
        {
            RaycastHit hit;
            Vector3 rayOrigin = camera.transform.position;
            Vector3 rayDirection = camera.transform.forward;

            // Draw the ray in the Scene view
            Debug.DrawLine(rayOrigin, rayOrigin + rayDirection * maxRayDistance, Color.blue);

            // Cast a ray forward from the camera's position
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, maxRayDistance))
            {
                // Check if the ray hits an object with the specified tag
                if (hit.collider != null && hit.collider.CompareTag(targetTag))
                {
                    // Optionally, draw a line to the hit point in a different color
                    Debug.DrawLine(rayOrigin, hit.point, Color.green); // Draws a line to where the ray hit

                    // Change the object's color to red
                    SetObjectColor(hit.collider.gameObject, Color.red);

                    // If there is a last hit object and it's not the current hit object, reset its color
                    if (lastHitObject != null && lastHitObject != hit.collider.gameObject)
                    {
                        ResetObjectColor(lastHitObject);
                    }

                    // Update the last hit object
                    lastHitObject = hit.collider.gameObject;

                    // Check if both triggers are pulled
                    if (triggerInputDetector._isPlaying)
                    {
                        // Move towards the object
                        Vector3 directionToMove = (hit.point - transform.position).normalized;
                        transform.position += directionToMove * moveSpeed * Time.deltaTime;
                }
            }
            }
            else
            {
                // If no object is hit and there is a last hit object, reset its color
                if (lastHitObject != null)
                {
                    ResetObjectColor(lastHitObject);
                    lastHitObject = null;
                }
            }
        }
    }


    void SetObjectColor(GameObject obj, Color color)
    {
        // Check if the object has a Renderer component and change its color
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }
    }

    void ResetObjectColor(GameObject obj)
    {
        // Reset the object's color to white (or any original color)
        SetObjectColor(obj, Color.white);
    }
}
