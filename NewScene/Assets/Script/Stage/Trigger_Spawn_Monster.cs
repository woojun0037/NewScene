using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Spawn_Monster : MonoBehaviour
{
    public GameObject Monster;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Main_gangrim")
        {
            Monster.SetActive(true);
        }
    }

}
