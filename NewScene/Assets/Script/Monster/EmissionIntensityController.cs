using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionIntensityController : MonoBehaviour
{
    public Renderer renderer;
    public Color baseColor;
    public float minIntensity = 5f;
    public float maxIntensity = 10f;
    public float duration = 2f;

    private float timer = 0f;
    private bool isIncreasing = true;
    bool isEmission;
    //private void Start()
    //{
    //    isEmission = true;
    //}

    //private void Update()
    //{
    //    if (isEmission)
    //    {
    //        isEmission = false;
    //        EmssionMaxMin();
    //    }

    //}

    public void continualEmission()
    {
        timer += Time.deltaTime;

        float t = Mathf.PingPong(timer / duration, 1f);

        float intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
        Color emissionColor = baseColor * intensity;
        renderer.material.SetColor("_EmissionColor", emissionColor);

        renderer.material.EnableKeyword("_EMISSION");
    }


    IEnumerator ChangeEmission()
    {
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / duration;

            if (isIncreasing)
            {
                // 증가
                float intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
                SetEmissionIntensity(intensity);
            }
            else
            {
                // 감소
                float intensity = Mathf.Lerp(maxIntensity, minIntensity, t);
                SetEmissionIntensity(intensity);
            }

            yield return null;
        }
    }
    private void SetEmissionIntensity(float intensity)
    {
        Color emissionColor = baseColor * intensity;
        //renderer.material.SetColor("_EmissionColor", emissionColor); //보통 머테리얼
        renderer.material.SetColor("_Emissive_Color", emissionColor); //쉐이더 용

        renderer.material.EnableKeyword("_EMISSION");
    }


    IEnumerator CorEmissions()
    {
        StartCoroutine(ChangeEmission());

        yield return new WaitForSeconds(1f);
        isIncreasing = false;

        StartCoroutine(ChangeEmission());
        
        yield return new WaitForSeconds(1f);
        isIncreasing = true;

        yield return new WaitForSeconds(0.1f);
        SetEmissionIntensity(minIntensity);
    }
    public void EmssionMaxMin()
    {
        StartCoroutine(CorEmissions());
    }

}

