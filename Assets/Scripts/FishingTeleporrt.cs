using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingTeleporrt : MonoBehaviour
{
    // Set this position in the Unity Editor or through another script
    public Transform teleportPosition;

    [Header("fade")]
    // ------ fade ------
    public FadeEffect fadeEffect;


    private void OnTriggerEnter(Collider collision)
    {
        // Check if the object we collided with has the tag "Player"
        if (collision.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<FollowingController>())
            {
                collision.gameObject.GetComponent<FollowingController>().enabled = false;
            }
            StartCoroutine(fadeEffect.FadeOut(() => {
                // Teleport the player to the designated position
                collision.gameObject.transform.position = teleportPosition.position;
            }));

        }
    }
}
