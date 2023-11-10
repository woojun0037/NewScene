using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seonbi_bullet : MonoBehaviour
{
    private Transform PlayerPosition;
    Vector3 forward;

    public float SeonbiDamageToGive;
    public float bulletspeed;
    
    private float time = 0f;

     
    void Start()
    {
        PlayerPosition = GameObject.FindWithTag("Main_gangrim").transform;
        forward = PlayerPosition.position - this.transform.position;
    }


    void Update()
    {   
        transform.position += forward.normalized * bulletspeed * Time.deltaTime;
        time += Time.deltaTime;

        if (time >= 1.5f)
        {
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Main_gangrim")
        {
            FindObjectOfType<HealthManager>().HurtPlayer(SeonbiDamageToGive);
        }
    }


}
