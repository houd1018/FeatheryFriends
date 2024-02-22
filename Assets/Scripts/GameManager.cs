using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    // --------Animation----------
    private bool anim_first = false;
    private bool anim_second = false;


    [Header("fishing")]
    public int fishRequired = 2;
    public int fishCount = 0;
    public Transform fishingEndpoint;
    public Transform momEndpoint;
    public bool isFishing = false;
    public bool hasfished = false;
    public GameObject momEagle;

    [Header("fade")]
    // ------ fade ------
    public FadeEffect fadeEffect;


    [Header("MomEagle_State")]
    public MomEagleState mom_currentState = MomEagleState.Begin;

    public enum MomEagleState
    {
        Begin,
        FlyRoute_1,
        FlyRoute_2
    }

    private void Update()
    {

        PlayAnime();


        // fishing transition
        if (fishCount >= fishRequired && !hasfished)
        {
            Debug.Log("mom teleport");
            StartCoroutine(fadeEffect.FadeOut(() =>
            {
                // Set the XR Origin's position and rotation to that of the restorePoint
                player.transform.position = fishingEndpoint.position;
                player.transform.rotation = fishingEndpoint.rotation;
                player.GetComponent<flyController>().speed = 0;

                momEagle.GetComponent<Animator>().enabled = false;
                momEagle.transform.position = momEndpoint.position;
                momEagle.transform.rotation = momEndpoint.rotation;

                hasfished = true;
                isFishing = false;
            }));
        }
    }

    void PlayAnime()
    {
        switch (mom_currentState)
        {
            case MomEagleState.FlyRoute_1:
                if (!anim_first)
                {
                    momEagle.GetComponent<Animator>().Play("Base Layer.FlyRoute_1");
                    anim_first = true;
                }
                break;

            case MomEagleState.FlyRoute_2:
                if (!anim_second)
                {
                    momEagle.GetComponent<Animator>().enabled = true;
                    momEagle.GetComponent<Animator>().Play("Base Layer.FlyRoute_2");
                    anim_second = true;
                    player.gameObject.GetComponent<FollowingController>().enabled = true;
                }
                break;
        }


    }

}
