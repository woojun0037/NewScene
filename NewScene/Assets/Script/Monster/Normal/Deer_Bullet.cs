using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer_Bullet : MonoBehaviour
{

    public float deerDamage;
    bool isattack;

    private void OnEnable()
    {
        isattack = false;
    }
    private void OnTriggerEnter(Collider other) //플레이어가 들어오면 bool 호출 후 도트딜
    {
        if (other.gameObject.tag == "Main_gangrim" && !isattack)
        {
            isattack = true;
            StartCoroutine(PlayerAttackTime());            
        }

    }
    IEnumerator PlayerAttackTime()
    {
        FindObjectOfType<HealthManager>().HurtPlayer(deerDamage);
        yield return new WaitForSeconds(1f); //도트딜 시간
        isattack = false;
    }
}
