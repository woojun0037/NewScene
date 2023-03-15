using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStart : MonoBehaviour
{
    public GameObject Tiger;
    // Update is called once per frame
    void Start()
    {
        Tiger.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Main_gangrim")
        {
            Tiger.gameObject.SetActive(true);
        }
    }
}
