using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public Camera shakerCam;
    Vector3 camPos;

    [SerializeField][Range(0.01f, 0.1f)] float ShakerRange = 0.05f;
    [SerializeField][Range(0.1f, 1f)] float Duration = 0.5f;

    public void Shake()
    {
        camPos = shakerCam.transform.position;
        InvokeRepeating("StartShake", 0f, 0.005f);
        Invoke("StopShake", Duration);
    }

    void StartShake()
    {
        float camPosX = Random.value * ShakerRange * 2 - ShakerRange;
        float camPosY = Random.value * ShakerRange * 2 + ShakerRange;
        Vector3 camPos = shakerCam.transform.position;
        camPos.x += camPosX;
        camPos.y += camPosY;
        shakerCam.transform.position = camPos; 
    }

    void StopShake()
    {
        CancelInvoke("StartShake");
        shakerCam.transform.position = camPos;
    }
}
