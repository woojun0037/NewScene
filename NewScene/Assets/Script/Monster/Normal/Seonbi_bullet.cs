using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seonbi_bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbodythis;
    private Transform PlayerPosition;
    private Transform thisposition;
    Seonbi_Script seonbi;

    Vector3 aattack;
    Vector3 objrotation;

    public float SeonbiDamageToGive;
    public float bulletspeed;
    
    private float random;
    private float time = 0f;

    private bool rotationonetime;
    private bool playercollider;
    private bool getposition; //정해진 도착

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
            FindObjectOfType<HealthManager>().HurtPlayer(SeonbiDamageToGive);
        }
    }


}
