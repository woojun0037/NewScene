using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public Transform target; // 따라가는 타겟
    
    public float Speed = 0.1f;
    public float RotationSpeed;
    public float t = 0.1f;

    void Start()
    {

    }

    void FixedUpdate()
    {
        Vector3 a = transform.position;
        Vector3 b = target.position;
        transform.position.Normalize();

        transform.position = Vector3.MoveTowards(a, Vector3.Lerp(a, b, t), Speed);

        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, transform.position);

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, RotationSpeed * Time.deltaTime);
    }
}
