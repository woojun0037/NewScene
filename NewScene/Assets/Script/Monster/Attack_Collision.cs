using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Collision : MonoBehaviour
{
    public float GetDamaged = 1;
    bool isCollision;

    private void Start()
    {
        isCollision = false;
    }

    void OnEnable()
    {
        isCollision = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Main_gangrim")
        {
            if (!isCollision)
                damage();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Main_gangrim")
        {
            if (!isCollision)
                damage();
        }
    }

    void damage()
    {
        StartCoroutine(PlayerAttackTime());
    }

    IEnumerator PlayerAttackTime()
    {
        isCollision = true;

        FindObjectOfType<HealthManager>().HurtPlayer(GetDamaged);

        yield return new WaitForSeconds(0.8f);
        isCollision = false;
    }

}
