using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Bullet : MonoBehaviour
{
    public float GetDamaged;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Main_gangrim")
        {
            FindObjectOfType<HealthManager>().HurtPlayer(GetDamaged);
        }
    }
}
