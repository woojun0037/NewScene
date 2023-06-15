using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Script : Enemy
{

    [SerializeField] float attackDelaytime;
    Vector3 currentpos;

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

        currentpos = transform.position;
    }

    void Update()
    {
        Dist = Vector3.Distance(transform.position, targetTransform.transform.position);

        DieMonster();
        monsterMove();
        NotDamaged();
        CanvasMove();
    }

    protected override void CanvasMove()
    {

        base.CanvasMove();
    }

    protected override void DieMonster()
    {
        if (curHearth < 1)
        {
            animator.SetBool("isDie", true);
            DontMove = true;
            isdie = true;
            OnDisable();

            deletetime += Time.deltaTime;

            if (deletetime >= 5f) 
            {
               gameObject.SetActive(false);            
            }
        }
    }
    protected override void GetDamagedAnimation() {
        int random =1;
        random = UnityEngine.Random.Range(1, 3);
        StartCoroutine(damaged_ani(random));
    }

    protected override void monsterMove()
    {
        if (Dist < 7 && Player.HP >= 0 && isdie != true)
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


            if (Dist <= 1)
            {
                if (!isattack)
                {
                    isattack = true;
                    StartCoroutine("attacker");
                }
            }
            else
            {
                getTouch = true;
                animator.SetBool("Attack", false);
            }
        }
        else
        {
            if(agent != null)
            {
                agent.destination = currentpos;
            }
            if (Vector3.Distance(transform.position, currentpos) < 1)
            animator.SetBool("Move", false);
        }
    }


    IEnumerator attacker()
    {
        DontMove = true;
        animator.SetBool("Attack", true);
        getTouch = false;

        yield return new WaitForSeconds(attackDelaytime);
        isattack = false;
        DontMove = false;

    }

    IEnumerator damaged_ani(int random)
    {
        animator.SetInteger("Hit", random);
        yield return new WaitForSeconds(0.6f);
        animator.SetInteger("Hit", 0);
    }
}