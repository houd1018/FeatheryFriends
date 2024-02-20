using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;


    [Header("fishing")]
    public int fishRequired = 2;
    public int fishCount = 0;
    public Transform fishingEndpoint;
    private bool hasfished = false;
    public GameObject momEagle;

    [Header("fade")]
    // ------ fade ------
    public FadeEffect fadeEffect;

    private void Update()
    {
        // fishing transition
        if (fishCount >= fishRequired && !hasfished)
        {
            StartCoroutine(fadeEffect.FadeOut(() =>
            {
                // Set the XR Origin's position and rotation to that of the restorePoint
                player.transform.position = fishingEndpoint.position;
                player.transform.rotation = fishingEndpoint.rotation;
                player.GetComponent<flyController>().speed = 0;
                hasfished = true;

                momEagle.transform.position = fishingEndpoint.position;
                momEagle.transform.rotation = fishingEndpoint.rotation;
                momEagle.GetComponent<Animator>().SetBool("Phase_2", true);
            }));

            if (player.GetComponent<FollowingController>())
            {
                player.gameObject.GetComponent<FollowingController>().enabled = true;
            }
        }
    }


}
