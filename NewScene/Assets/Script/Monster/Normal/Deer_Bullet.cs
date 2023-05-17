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
    private void OnTriggerEnter(Collider other) //�÷��̾ ������ bool ȣ�� �� ��Ʈ��
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
        yield return new WaitForSeconds(1f); //��Ʈ�� �ð�
        isattack = false;
    }
}
