using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seonbi_bullet : MonoBehaviour
{
    Transform PlayerPosition;
    Transform thisposition;
    Vector3 aattack;

    float time = 0f;

    bool playercollider;

    Vector3 objrotation;
    bool rotationonetime;

    [SerializeField]
    float bulletspeed = 20f;

    Rigidbody rigidbodythis;
    bool getposition; //정해진 도착
    float random;

    void Start()
    {
        random = UnityEngine.Random.Range(0, 2); // 0 
        playercollider = false;
        getposition = false;
        rotationonetime = false;

        rigidbodythis = GetComponent<Rigidbody>();

        PlayerPosition = GameObject.FindWithTag("Main_gangrim").transform;
        aattack = PlayerPosition.position;

        thisposition = GetComponent<Transform>();


    }

    void Update()
    {
        if (time <= 0.5f&& !rotationonetime)
        {
            if(random == 0)
            {
                rigidbodythis.velocity = thisposition.right *10;
            }
            else
            {
                rigidbodythis.velocity = thisposition.right * -10;
            }
            Debug.Log("velocity velocity");

            rotationonetime = true;
        }


        time += Time.deltaTime;

        if (transform.position == aattack && playercollider ==false)
        {
            if (playercollider)
            { 
                getposition = true;
            }
            else  
            {
                getposition = false;
                transform.position = aattack;

            }

            if (time >= 1.5f)
            {
              Destroy(gameObject);
            }
        }
        else if(getposition == false && playercollider == false)
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, aattack, bulletspeed * Time.deltaTime);
        }


        if (time >= 1.5f)
        {
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Main_gangrim")
        {
            playercollider = true;
        }
    }


}
