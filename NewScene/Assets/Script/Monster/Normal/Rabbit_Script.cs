using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_Script : MonoBehaviour
{
    [SerializeField]
    GameObject attacker_Col;
    private Animator animator;
    [SerializeField]
    float SetY;
    bool isattack;
    bool DontMove;
    Transform targettransform;

    [SerializeField]
    float MaxDistance;
    [SerializeField]
    float chasespeed;
    RaycastHit hit;

    bool meetplayer;

    public float monsterhp;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Idle", true);
        targettransform = GameObject.FindWithTag("Main_gangrim").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayspawn = transform.position;
        rayspawn.y = SetY; //레이 바닥에서 부터 쏘게만듬

        Vector3 targety = targettransform.position;
        targety.y = SetY;


        Debug.DrawRay(rayspawn, transform.forward * MaxDistance, Color.blue, 0.05f);

        if (Physics.Raycast(rayspawn, transform.forward, out hit, MaxDistance))
        {
            if (hit.collider.tag == "Main_gangrim") //플레이어에게 레이 닿으면 정지하고 총알 공격
            {
                Debug.Log("Main tag");
                DontMove = true;
                if (!isattack)
                {
                    isattack = true;
                    transform.LookAt(targety);
                    animator.SetBool("Attack", true);
                    StartCoroutine("attacker");

                }
            }

        }
        else
        {
            DontMove = false;
        }

        if (!DontMove)
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Move", true);
            //agent.destination = targety;
            transform.LookAt(targety);
            transform.position = Vector3.MoveTowards(gameObject.transform.position, targety, chasespeed * Time.deltaTime);//3번째 값 돌진 속도
        }
        else
        {
            animator.SetBool("Move", false);
        }

    }

    void MonsterDamage() //플레이어 스크립트에서 불러옴
    {
        Debug.Log("MonsterDamage()");

        if (monsterhp > 1)
        {
            monsterhp--;
        }
        else
        {
            MonsterDie();
        }
        Debug.Log(monsterhp);
    }

    void MonsterDie()
    {
        Destroy(gameObject);
    }

    IEnumerator attacker()
    {
        animator.SetBool("Attack", true);
        attacker_Col.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        attacker_Col.SetActive(false);
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(0.7f);
        isattack = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Main_gangrim")
        {
            //플레이어에 피격 매서드 불러옴
            //playercollider = true;

            Debug.Log("player collision");
        }
    }
}
