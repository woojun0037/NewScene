using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForward : MonoBehaviour
{
     private float moveSpeed = 5f;
    private Vector3 moveDirection;
    Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Main_gangrim").transform;
        moveDirection = (player.transform.position - gameObject.transform.position).normalized;
        moveDirection.y = 0;

    }

    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
