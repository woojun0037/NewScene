using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdownEffect : MonoBehaviour
{
    bool canCollide = true;

    void OnParticleCollision(GameObject other)

    {
        if (canCollide && other.gameObject.tag == "Main_gangrim")
        {
            //FindObjectOfType<HealthManager>().HurtPlayer(3);
            StartCoroutine(ProcessCollision());
        }
    }

    IEnumerator ProcessCollision()
    {
        canCollide = false;
        FindObjectOfType<HealthManager>().HurtPlayer(3);

        // 일정 시간 대기
        yield return new WaitForSeconds(1f);

        canCollide = true; // 다시 충돌을 허용
    }

}
