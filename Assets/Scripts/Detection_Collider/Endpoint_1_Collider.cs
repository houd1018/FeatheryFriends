using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Endpoint_1_Collider : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        // Check if the object we collided with has the tag "Player"
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Howard_Scene_2");
        }
    }
}
