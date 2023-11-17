using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSensitivity : MonoBehaviour
{
    CameraMovemant cameSensitivity;
    public float AddSenstivity;

    // Start is called before the first frame update
    void Start()
    {
        cameSensitivity = FindObjectOfType<CameraMovemant>();
        if(cameSensitivity.sensitivity < 800)
        cameSensitivity.sensitivity += AddSenstivity;
    }

}
