using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public Material material;
    public ParticleSystem[] particleSystems;
    public string parameterName = "_EffectAmount";

    private void Start()
    {
        material.SetFloat(parameterName, 0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startBoost();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            endBoost();
        }
    }

    public void startBoost()
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Play();
        }
        material.SetFloat(parameterName, 0.5f);
    }

    public void endBoost()
    {
        material.SetFloat(parameterName, 0);
    }
}
