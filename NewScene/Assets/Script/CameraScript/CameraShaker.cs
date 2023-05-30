using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private Transform cam;
    public PlayerSkill skill;
    public float shakeTime = 0.3f;
    public float shakeSpeed = 1.0f;
    public float shakeAmount = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        ShakeInput();
    }

    public void ShakeInput()
    {
        if(skill.isShake)
        {
            StartCoroutine(Shake());      
        }
    }

    IEnumerator Shake()
    {
        yield return new WaitForSeconds(1.2f);
        Vector3 originPosition = cam.localPosition;
        float elapsedTime = 0.0f;
        while (elapsedTime < shakeTime)
        {
            Vector3 randomPoint = originPosition + Random.insideUnitSphere * shakeAmount;
            cam.localPosition = Vector3.Lerp(cam.localPosition, randomPoint, Time.deltaTime * shakeSpeed);
            elapsedTime += Time.deltaTime;
            skill.isShake = false;
            yield return null;
        }
        cam.localPosition = originPosition;
    }
}
