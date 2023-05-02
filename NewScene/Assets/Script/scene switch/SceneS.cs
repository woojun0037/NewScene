using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneS : MonoBehaviour
{
    public string Stage1;
    public float fadeTime = 1.0f;
    public KeyCode switchKey = KeyCode.Space;
    public Image fadeImage;

    private bool isFading = false;

    private void Update()
    {
        if (Input.GetKeyDown(switchKey) && !isFading)
        {
            StartCoroutine(FadeOutAndSwitchScene());
        }
    }

    private IEnumerator FadeOutAndSwitchScene()
    {
        isFading = true;
        fadeImage.gameObject.SetActive(true);

        float elapsedTime = 0.0f;
        while (elapsedTime < fadeTime)
        {
            float alpha = Mathf.Lerp(0.0f, 1.0f, elapsedTime / fadeTime);
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene("Stage1");

        elapsedTime = 0.0f;
        while (elapsedTime < fadeTime)
        {
            float alpha = Mathf.Lerp(1.0f, 0.0f, elapsedTime / fadeTime);
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
        isFading = false;
    }
}



