using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingFade : MonoBehaviour
{
    public Image image;
    GameObject canvas;

    private void Awake()
    {
        image = GetComponent<Image>();
        canvas = GameObject.Find("Canvas");
    }

    private void Update()
    {
        canvas.SetActive(false);
        Color color = image.color;

        if (color.a >= 0)
        {
            color.a += Time.deltaTime;
        }
        image.color = color;

        if (image.color.a > 1)
        {
            SceneManager.LoadScene("Ending");
        }
    }
}

