using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer_Bullet_Launcher : MonoBehaviour
{
    public GameObject lightAttack_prefab;

    private Transform PlayerPosition;
    private int lightObjectes = 5;
    private float spawntime = 1f;
    private float moveSpeed = 2f;
    private float time;

    private int spawnedObj = 0;
    private Vector3 moveDirection;
    private void OnEnable()
    {
        PlayerPosition = GameObject.FindWithTag("Main_gangrim").transform;
        moveDirection = (PlayerPosition.position - transform.position).normalized;
        moveDirection.y = 0;

        time = 0f;
        spawnedObj = 0;
    }

    void Start()
    {
        PlayerPosition = GameObject.FindWithTag("Main_gangrim").transform;
        moveDirection = (PlayerPosition.position - transform.position).normalized;
        moveDirection.y = 0;

        //StartCoroutine(attacker());
    }

    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        time += Time.deltaTime;
        if (time >= spawntime && spawnedObj < lightObjectes)
        {
            Instantiate(lightAttack_prefab, transform.position, Quaternion.identity); //È¸Àü X
            spawnedObj++;
            time = 0f;
        }

        if(spawnedObj == lightObjectes)
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    //IEnumerator attacker()
    //{
    //    for (int i = 0; i < 5; i++)
    //    {
    //        Instantiate(lightAttack_prefab, transform.position, Quaternion.identity);
    //        yield return new WaitForSeconds(1f);
    //    }
    //}
}
