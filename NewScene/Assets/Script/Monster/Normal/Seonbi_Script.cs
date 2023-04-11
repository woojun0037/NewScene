using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Seonbi_Script : Enemy
{
    //private NavMeshAgent agent = null;
    private Animator animator;

    [SerializeField]
    float SetY;
    RaycastHit hit;

    bool isattack;
    bool DontMove;

    public Rigidbody SeonbiBullet; 

    public float monsterhp;
    protected override void Awake()
    {
        base.Awake();
        isattack = false;
    }

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        monsterMove();
        NotDamaged();
        DieMonster();
    }

    protected override void monsterMove()
    {
        animator.SetBool("Move", true);
        base.monsterMove();


        Vector3 targety = targetTransform.position;
        targety.y = SetY;

        //현재 경로에서 목표 지점까지 남아있는 거리
        if (agent.remainingDistance <= 5)
        {
            if (!isattack)
            {
                transform.LookAt(targety);
                isattack = true;
                StartCoroutine("attacker");

            }

        }

    }


    IEnumerator attacker()
    {
        Vector3 target_ = transform.position;
        target_.y = SetY+0.5f;

        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(0.9f);
        Rigidbody ThrowRockrigid = Instantiate(SeonbiBullet, target_, transform.rotation);
        yield return new WaitForSeconds(1.3f);
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(0.7f);
        isattack = false;
    }

}
