using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_KnockBack : MonoBehaviour
{
    [SerializeField] private float knockbackDuration = 0.1f; //넉백 받는 시간
    [SerializeField] private float knockbackForce = 500f;

    [SerializeField]
    Rigidbody monsterRigidbody;
    GameObject Player;

    private void Start()
    {
        Player = GameObject.FindWithTag("Main_gangrim");
        monsterRigidbody = GetComponent<Rigidbody>(); // Rigidbody 할당
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            StartCoroutine(Knockback());
        }
    }

    IEnumerator Knockback()
    {
        Vector3 dir = transform.position - Player.transform.position;
        dir.y = 0f;

        Vector3 knockbackDir = dir.normalized;
        monsterRigidbody.AddForce(knockbackDir * knockbackForce);

        yield return new WaitForSeconds(knockbackDuration);

        monsterRigidbody.velocity = Vector3.zero;
        monsterRigidbody.angularVelocity = Vector3.zero;
    }
}
