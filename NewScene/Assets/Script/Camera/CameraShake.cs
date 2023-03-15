using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float ShakeTime = 1.0f;
    public float ShakeSpeed = 2.0f;
    public float ShakeAmount = 1.0f;

    private Transform cam;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.V))
        {
            Debug.Log("ют╥б");
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        Vector3 originPosition = cam.localPosition;
        float elapsedTime = 0.0f;

        while(elapsedTime < ShakeTime)
        {
            Vector3 randomPoint = originPosition + Random.insideUnitSphere * ShakeAmount;
            cam.localPosition = Vector3.Lerp(cam.localPosition, randomPoint , Time.deltaTime * ShakeSpeed);

            yield return null;

            elapsedTime += Time.deltaTime;
        }

        cam.localPosition = originPosition;
    }
}
