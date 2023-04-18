using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHit : MonoBehaviour
{
    public Enemy enemy;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "monster")
        {
            StartCoroutine(Hitcor());
        }
    }

    IEnumerator Hitcor()
    {
        enemy.curHearth -= 1f;
        yield return new WaitForSeconds(0.5f);
    }
}
