using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_KnockBack : MonoBehaviour
{
    [SerializeField] private float knockbackDuration = 0.1f; //넉백 받는 시간
    [SerializeField] private float knockbackForce = 500f;

    [SerializeField] Rigidbody monsterRigidbody;
    [SerializeField] GameObject Player;

    private bool isKnockbackCoroutine = false;
    [SerializeField] private float knockBackDelayTime =1;

    private Enemy enemy;

    private void Start()
    {
        Player = GameObject.FindWithTag("Main_gangrim");
        monsterRigidbody = GetComponent<Rigidbody>(); // Rigidbody 할당
        enemy = GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon") && !isKnockbackCoroutine)
        {
            if (enemy.curHearth > 0)
                StartCoroutine(Knockback());
        }
    }

    IEnumerator Knockback()
    {
        isKnockbackCoroutine = true;

        Vector3 dir = transform.position - Player.transform.position;
        dir.y = 0f;

        Vector3 knockbackDir = dir.normalized;
        monsterRigidbody.AddForce(knockbackDir * knockbackForce);

        yield return new WaitForSeconds(knockbackDuration);

        monsterRigidbody.velocity = Vector3.zero;
        monsterRigidbody.angularVelocity = Vector3.zero;

        yield return new WaitForSeconds(knockBackDelayTime);
        isKnockbackCoroutine = false;
    }
}
