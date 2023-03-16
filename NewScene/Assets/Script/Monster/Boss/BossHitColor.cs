using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitColor : MonoBehaviour
{
    Material mat;

    void Awake()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    private void OnTriggerEnter(Collider other) //Triggerø° ¥Í¿Ω
    {
         if (other.tag == "Weapon")
        {
            StartCoroutine(OnDamage());
        }
    }

    IEnumerator OnDamage()
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);
    }
}
