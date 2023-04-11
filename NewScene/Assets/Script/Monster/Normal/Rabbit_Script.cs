using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_Script : Enemy
{
    [SerializeField]
    GameObject attacker_Col;
    
    private Animator animator;
    [SerializeField]
    float SetY;

    bool isattack;
    bool DontMove;

    //RaycastHit hit;
    //[SerializeField]
    //private float Distance;
    //bool meetplayer;
    float time;

    bool first_attack = false; //토끼는 에이전트 문제로 바로 공격함 -> 이를 방지

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        isattack = false;
        DontMove = false;
        time = 0;
        animator = GetComponent<Animator>();
        animator.SetBool("Idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        DieMonster();
        monsterMove();
        NotDamaged();
    }


    protected override void monsterMove()
    {

        if (!DontMove)
        {
            animator.SetBool("Move", true);
            base.monsterMove();
        }

        Vector3 targety = targetTransform.position;
        targety.y = SetY;

        //현재 경로에서 목표 지점까지 남아있는 거리
        if (agent.remainingDistance <= 2)
        {
            if(!first_attack)
            {
                first_attack = true;
            }
            else
            {
                Debug.Log("remainingDistance");
                if (!isattack)
                {
                    transform.LookAt(targety);
                    isattack = true;
                    StartCoroutine("attacker");
                }
            }

        }
    }



    IEnumerator attacker()
    {
        DontMove = true; //공격 중에 이동 정지

        animator.SetBool("Attack", true);
        attacker_Col.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        attacker_Col.SetActive(false);
        animator.SetBool("Attack", false);
        animator.SetBool("Move", false);
        yield return new WaitForSeconds(0.7f);

        isattack = false;
        DontMove = false;

    }

}
