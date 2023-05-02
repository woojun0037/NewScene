using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_Script : Enemy
{
    [SerializeField] GameObject attacker_Col;
    public GameObject ragdoll_obj;

    [SerializeField] float SetY;
    [SerializeField] float attackDelaytime;

    public ParticleSystem particle_attack;
    private Animator animator;

    bool isattack;
    bool DontMove;

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
        animator = GetComponent<Animator>();
        animator.SetBool("Idle", true);
    }
    public bool animatordie;

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
            GameObject ThrowRockrigid = Instantiate(ragdoll_obj, transform.position, transform.rotation);
            agent.enabled = false;
            gameObject.SetActive(false);
        }
    }

    protected override void GetDamagedAnimation() {
        int random =1;
        random = UnityEngine.Random.Range(1, 3);
        StartCoroutine(damaged_ani(random));
    }

    protected override void monsterMove()
    {
        if (Player.HP >= 0)
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

            if (Dist <= 2)
            {
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
        DontMove = true;
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

    IEnumerator damaged_ani(int random)
    {
        animator.SetInteger("Hit", random);
        yield return new WaitForSeconds(0.6f);
        animator.SetInteger("Hit", 0);
    }
}
