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
            Debug.Log("OnTriggerStay");
            if (!isCollision)
                damage();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Main_gangrim")
        {
            Debug.Log("OnCollisionStay");
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
        Debug.Log("PlayerAttackTime");
        isCollision = true;

        FindObjectOfType<HealthManager>().HurtPlayer(GetDamaged);

        yield return new WaitForSeconds(1f);
        isCollision = false;
    }

}
