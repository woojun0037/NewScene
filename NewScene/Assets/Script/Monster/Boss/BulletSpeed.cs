using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpeed : MonoBehaviour
{
    public float Speed;
   
    private void FixedUpdate()
    {
        transform.Translate(Vector3.down * Time.deltaTime * Speed, Space.World);
    }
}
