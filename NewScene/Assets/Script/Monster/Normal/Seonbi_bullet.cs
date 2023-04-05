using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seonbi_bullet : MonoBehaviour
{
    [SerializeField]
    Transform PlayerPosition;
    Transform thisposition;

    [SerializeField]
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

    // Start is called before the first frame update
    void Start()
    {
        random = UnityEngine.Random.Range(0, 2); // 0 
        playercollider = false;
        getposition = false;
        rotationonetime = false;

        rigidbodythis = GetComponent<Rigidbody>();

        PlayerPosition = GameObject.FindWithTag("Main_gangrim").transform;
        aattack = PlayerPosition.position;
        //aattack.y = 1;

        thisposition = GetComponent<Transform>();


    }

    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime;
        //오브젝트 회전
        //objrotation = new Vector3(0f, (Mathf.Cos(timer) * 0.5f + 0.5f) * 180f, 0f);
        //transform.rotation = Quaternion.Euler(objrotation);

        //transform.rotation = Quaternion.Euler(0, Mathf.Cos(timer) * 0.5f + 0.5f, 0);


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

        if (transform.position == aattack)
        {
            if (playercollider)
            { //플레이어와 부딪힘
                getposition = true;
            }
            else  //튕기는 거 때문에 aattack 포지션에 계속 있게함
            {
                getposition = false;
                transform.position = aattack;

            }

            if (time >= 1.5f)
            {
              Destroy(gameObject);
            }
        }
        else if(getposition == false)
        {
            //정해진 포지션으로 이동
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
            //플레이어에 피격 매서드 불러옴
            playercollider = true;

            Debug.Log("player collision");
        }
    }


}
