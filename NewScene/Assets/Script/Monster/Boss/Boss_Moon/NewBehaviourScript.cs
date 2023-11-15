using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject canv;
    [SerializeField] GameObject minMap;

    void Start()
    {
        minMap = GameObject.Find("MiniMap");
        canv = GameObject.Find("Canvas");
    }

    public void On()
    {
        minMap.SetActive(true);
        canv.SetActive(true);
    }

    public void Off()
    {
        minMap.SetActive(false);
        canv.SetActive(false);
    }
}
