using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource m_AudioSource;

    public AudioClip At_the_nest;
    public AudioClip After_take_off;
    public AudioClip Fly_near_to_the_lake;
    public AudioClip Touch_the_lake_collider;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void PlaySound_At_the_nest()
    {
        m_AudioSource.clip = At_the_nest;
        m_AudioSource.Play();
    }

    public void PlaySound_After_take_off()
    {
        m_AudioSource.clip = After_take_off;
        m_AudioSource.Play();
    }

    public void PlaySound_Fly_near_to_the_lake()
    {
        m_AudioSource.clip = Fly_near_to_the_lake;
        m_AudioSource.Play();
    }
    public void PlaySound_Fly_Touch_the_lake_collider()
    {
        m_AudioSource.clip = Touch_the_lake_collider;
        m_AudioSource.Play();
    }


}
