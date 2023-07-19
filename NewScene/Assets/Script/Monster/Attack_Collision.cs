using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Collision : MonoBehaviour
{
    public float GetDamaged = 1;
    public Collider objectCollider;


    private void Start()
    {
        objectCollider = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Main_gangrim")
        {
            FindObjectOfType<HealthManager>().HurtPlayer(GetDamaged);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Main_gangrim")
        {
            FindObjectOfType<HealthManager>().HurtPlayer(GetDamaged);
        }
    }

}
