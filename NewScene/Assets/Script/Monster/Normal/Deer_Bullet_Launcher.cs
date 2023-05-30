using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer_Bullet_Launcher : MonoBehaviour
{
    public GameObject lightAttack_prefab;
    private Transform PlayerPosition;
    private Vector3 moveDirection;

    private void OnEnable()
    {
        PlayerPosition = GameObject.FindWithTag("Main_gangrim").transform;
        StartCoroutine(attacker());
    }

    IEnumerator attacker()
    {
        for (int i =0; i < 5; i++)
        {
            moveDirection = PlayerPosition.transform.position;
            moveDirection.y = transform.position.y;

            Instantiate(lightAttack_prefab, moveDirection, Quaternion.identity);
            yield return new WaitForSeconds(1f);
            if(i >= 5)
            {
                Destroy(gameObject);
            }
        }
    }
}
