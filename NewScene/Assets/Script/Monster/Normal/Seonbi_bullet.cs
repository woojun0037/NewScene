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
        //if (time <= 0.5f&& !rotationonetime)
        //{
        //    if(random == 0)
        //    {
        //        rigidbodythis.velocity = thisposition.right *10;
        //    }
        //    else
        //    {
        //        rigidbodythis.velocity = thisposition.right * -10;
        //    }
        //    rotationonetime = true;
        //}


        //time += Time.deltaTime;

        //if (transform.position == aattack && playercollider ==false)
        //{
        //    if (playercollider)
        //    { 
        //        getposition = true;
        //    }
        //    else  
        //    {
        //        getposition = false;
        //        transform.position = aattack;

        //    }

        //    if (time >= 1.5f)
        //    {
        //      Destroy(gameObject);
        //    }
        //}
        //else if(getposition == false && playercollider == false)
        //{
        //    transform.position = Vector3.MoveTowards(gameObject.transform.position, aattack, bulletspeed * Time.deltaTime);
        //}

        transform.position += forward.normalized * bulletspeed * Time.deltaTime;


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
