using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer_Script : Enemy
{
    private Animator animator;

    [SerializeField]
    float SetY;
    RaycastHit hit;

    bool isattack;
    bool DontMove;

    public Rigidbody DeerAttacker;
    public GameObject ragdoll_obj;
    private float Dist;
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
        Dist = Vector3.Distance(transform.position, targetTransform.transform.position);

        base.monsterMove();

        if (!DontMove) //공격중에는 이동 기능 정지
        {
            animator.SetBool("Move", true);
            agent.isStopped = false;
        }
        else
        {
            animator.SetBool("Move", false);
            agent.isStopped = true;
        }


        Vector3 targety = targetTransform.position;
        targety.y = SetY;

        //현재 경로에서 목표 지점까지 남아있는 거리
        if (Dist <= 5)
        {
            if (!isattack)
            {
                //transform.LookAt(targety);
                isattack = true;
                StartCoroutine("attacker");
            }

        }

    }

    protected override void DieMonster()
    {
        if (curHearth < 1)
        {
            GameObject ThrowRockrigid = Instantiate(ragdoll_obj, transform.position, transform.rotation);

            gameObject.SetActive(false);
            agent.enabled = false;

        }
    }
    protected override void GetDamagedAnimation()
    {
        animator.SetBool("isHit", true);
        int random;
        random = UnityEngine.Random.Range(1, 3);
        StartCoroutine(damaged_ani(random));
    }

    IEnumerator attacker()
    {
        DontMove = true;
        int random;
        random = UnityEngine.Random.Range(1, 4);

        Vector3 targety = targetTransform.position;
        targety.y = SetY;

        animator.SetBool("Move", false);
        transform.LookAt(targety);

        animator.SetBool("isAttack", true);
        animator.SetInteger("Attack", random);

        yield return new WaitForSeconds(1f);

        //번개 발사
        //Vector3 target_ = transform.position;
        //target_.y = SetY + 0.5f;

        //Rigidbody ThrowRockrigid = Instantiate(DeerAttacker, target_, transform.rotation);

        animator.SetBool("isAttack", false);
        animator.SetInteger("Attack", 0);
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(2f);
        isattack = false;
        DontMove = false;
    }

    IEnumerator damaged_ani(int random)
    {
        animator.SetInteger("Hit", random);
        yield return new WaitForSeconds(0.6f);
        animator.SetBool("isHit", false);
        animator.SetInteger("Hit", 0);
    }
}
