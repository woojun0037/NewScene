using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBullet : MonoBehaviour
{
    private Vector3 moveDirection;
    public float moveSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        moveDirection = transform.forward;
        moveDirection.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
