using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_Script : Enemy
{
    [SerializeField]
    GameObject attacker_Col;
    public ParticleSystem particle_attack;

    private Animator animator;
    [SerializeField]
    float SetY;

    [SerializeField]
    float attackDelaytime;
    public GameObject ragdoll_obj;

    bool isattack;
    bool DontMove;

    //RaycastHit hit;
    //[SerializeField]
    //private float Distance;
    //bool meetplayer;
    float time;
    [SerializeField]
    private float Dist;

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
    public bool animatordie;

    // Update is called once per frame
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
            //Destroy(agent);
            GameObject ThrowRockrigid = Instantiate(ragdoll_obj, transform.position, transform.rotation);

            gameObject.SetActive(false);
            agent.enabled = false;
            //animator.applyRootMotion = false;
            //Destroy(gameObject);
        }
    }

    protected override void monsterMove()
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
        targety.y = SetY;

        //현재 경로에서 목표 지점까지 남아있는 거리
        if (Dist <= 2)
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



    IEnumerator attacker()
    {
        DontMove = true; //공격 중에 이동 정지

        animator.SetBool("Attack", true);

        attacker_Col.SetActive(true);
        yield return new WaitForSeconds(0.9f);
        particle_attack.Play();
        yield return new WaitForSeconds(0.4f);

        attacker_Col.SetActive(false);
        animator.SetBool("Attack", false);
        particle_attack.Stop();

        animator.SetBool("Move", false);
        yield return new WaitForSeconds(0.7f);

        yield return new WaitForSeconds(attackDelaytime);
        isattack = false;
        DontMove = false;

    }

}
