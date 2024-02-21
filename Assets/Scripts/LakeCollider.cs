using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeCollider : MonoBehaviour
{
    public SoundManager soundManager;

    private void OnTriggerEnter(Collider collision)
    {
        // Check if the object we collided with has the tag "Player"
        if (collision.CompareTag("Player"))
        {
            soundManager.PlaySound_Fly_near_to_the_lake();

        }
    }
}
