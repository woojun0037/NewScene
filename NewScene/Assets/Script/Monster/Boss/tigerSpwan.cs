using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tigerSpwan : MonoBehaviour
{
    public GameObject Spwan;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Main_gangrim")
        {
            Spwan.SetActive(true);
        }
    }
}
