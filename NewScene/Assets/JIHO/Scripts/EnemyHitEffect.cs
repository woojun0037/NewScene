using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitEffect : MonoBehaviour
{


    private void OnEnable()
    {
        StartCoroutine(Delay());   
    }

    private IEnumerator Delay()
    {
        float time = 0;
        while(time < 0.5f)
        {
            time += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        Destroy(this.gameObject);
    }
}
