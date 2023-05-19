using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_Script : Enemy
{

    [SerializeField] float attackDelaytime;

    private Animator animator;

    bool isattack;
    bool DontMove;
    bool isdie;

    private float Dist;
    float deletetime;

    protected override void Awake()
    {
        base.Awake();
        isdie = false;
    }

    protected override void Start()
    {
        base.Start();
        getTouch = true;
        isattack = false;
        DontMove = false;
        animator = GetComponent<Animator>();
        animator.SetBool("Idle", true);
    }

    void Update()
    {
        Dist = Vector3.Distance(transform.position, targetTransform.transform.position);

        DieMonster();
        monsterMove();
        NotDamaged();
    }

    protected override void DieMonster()
    {
        if (curHearth < 1)
        {
            animator.SetBool("isDie", true);
            DontMove = true;
            isdie = true;
            agent.enabled = false;

            deletetime += Time.deltaTime;

            if (deletetime >= 5f)
            {
                gameObject.SetActive(false);
            }
        }
    }
    protected override void GetDamagedAnimation()
    {
        int random = 1;
        random = UnityEngine.Random.Range(1, 3);
        StartCoroutine(damaged_ani(random));
    }

    protected override void monsterMove()
    {
        if (Player.HP >= 0 && isdie != true)
        {
            base.monsterMove();

            if (!DontMove)
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
            targety.y = transform.position.y;

            if (Dist <= MaxDistance)
            {
                if (!isattack)
                {
                    transform.LookAt(targety);
                    isattack = true;
                    StartCoroutine("attacker");
                }
            }else if(Dist > MaxDistance + 2f)
            {
                animator.SetBool("SkillAttack", true);
                DontMove = true;

            }
        }
    }



    IEnumerator attacker()
    {
        DontMove = true;
        animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.95f); //패는 애니 중간
        getTouch = false;
        animator.SetBool("isAttack", false);

        yield return new WaitForSeconds(0.9f); //패는 애니 중간
        getTouch = true;

        yield return new WaitForSeconds(attackDelaytime);
        isattack = false;
        //DontMove = false;

        if (Dist > MaxDistance)
        {
            DontMove = false;
        }

    }

    IEnumerator damaged_ani(int random)
    {
        animator.SetInteger("Hit", random);
        yield return new WaitForSeconds(0.6f);
        animator.SetInteger("Hit", 0);
    }
}
