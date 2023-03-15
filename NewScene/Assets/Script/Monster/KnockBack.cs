using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private Rigidbody Rigid;
    [SerializeField] private float strength = 16, delay = 0.15f;

    public UnityEvent OnBegin, OnDone;

    public void PlayerFeedback(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector3 direction = (transform.position - sender.transform.position).normalized;
        Rigid.AddForce(direction * strength, ForceMode.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        Rigid.velocity = Vector3.zero;

        OnDone?.Invoke();
    }
}
