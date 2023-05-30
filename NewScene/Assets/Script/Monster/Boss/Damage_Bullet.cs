using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Bullet : MonoBehaviour
{
    public float GetDamaged;

    public float activationDelay = 0.5f; // 5초의 지연 시간 설정
    [SerializeField]
    private Collider objectCollider;
    public bool GetColliderTime;

    private void Start()
    {
        if (GetColliderTime)
        {
            objectCollider = GetComponent<Collider>();
            objectCollider.enabled = true;
            StartCoroutine(ActivateCollider());
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Main_gangrim")
        {
            FindObjectOfType<HealthManager>().HurtPlayer(GetDamaged);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Main_gangrim")
        {
            FindObjectOfType<HealthManager>().HurtPlayer(GetDamaged);
        }
    }

    private IEnumerator ActivateCollider()
    {
        yield return new WaitForSeconds(activationDelay);
        objectCollider.enabled = true;
    }
}
