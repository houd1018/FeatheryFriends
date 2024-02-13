using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class FadeEffect : MonoBehaviour
{
    private Image fadePanel;
    public float fadeDuration = 3f;

    private void Start()
    {
        fadePanel = GetComponent<Image>();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.Escape))
    //    {
    //        StartCoroutine(FadeOut());
    //    }
    //}
    public IEnumerator FadeOut(Action onComplete = null)
    {
        float elapsedTime = 0f;
        Color startColor = fadePanel.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            //Debug.Log(elapsedTime);
            fadePanel.color = Color.Lerp(startColor, Color.black, elapsedTime / fadeDuration);
            yield return null;
        }
        fadePanel.color = Color.black;
        //SceneManager.LoadScene(sceneName);
        StartCoroutine(FadeIn());
        onComplete?.Invoke();
    }

    public IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color startColor = fadePanel.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadePanel.color = Color.Lerp(startColor, Color.clear, elapsedTime / fadeDuration);
            yield return null;
        }

        fadePanel.color = Color.clear;
    }
}
