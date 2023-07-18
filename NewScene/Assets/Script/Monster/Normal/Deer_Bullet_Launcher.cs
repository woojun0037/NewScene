using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer_Bullet_Launcher : MonoBehaviour
{
    public GameObject lightAttack_prefab;
    public GameObject Range;
    private Transform PlayerPosition;
    private Vector3 moveDirection;
    private Vector3 rangemoveDirection;
    private void OnEnable()
    {
        PlayerPosition = GameObject.FindWithTag("Main_gangrim").transform;
        StartCoroutine(attacker());
    }

    IEnumerator attacker()
    {
        for (int i =0; i < 1; i++)
        {
            moveDirection = PlayerPosition.transform.position;
            moveDirection.y = transform.position.y;
            rangemoveDirection = moveDirection;
            rangemoveDirection.y = transform.position.y;// - 0.06f

            GameObject rangeObject = Instantiate(Range, rangemoveDirection, Quaternion.identity);
            yield return new WaitForSeconds(1.2f);

            Destroy(rangeObject);
            Instantiate(lightAttack_prefab, moveDirection, Quaternion.identity);
            yield return new WaitForSeconds(1f);
            if(i >= 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
