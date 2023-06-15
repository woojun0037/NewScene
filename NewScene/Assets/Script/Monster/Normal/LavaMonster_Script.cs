using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaMonster_Script : Enemy
{
    public GameObject Bullet;
    public GameObject MonterObj;

    [SerializeField] float attackDelaytime;
    [SerializeField] ParticleSystem particle_attack;

    private Animator animator;

    bool isattack;
    bool DontMove;
    bool isdie;
    bool isrotation;

    private float Dist;
    float deletetime;
    float timer;

    protected override void Awake()
    {
        base.Awake();
        isdie = false;
    }

    protected override void Start()
    {
        base.Start();
        if (MonterObj == null)
            MonterObj = gameObject;

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
                MonterObj.gameObject.SetActive(false);
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

            monsterrotation();

            if (Dist <= MaxDistance)
            {
                if (!isattack)
                {
                    timer = 0;
                    isattack = true;
                    StartCoroutine("attacker");
                }
            }
        }
    }

    void monsterrotation()
    {
        if (!isrotation)
        {
            Vector3 targety = targetTransform.position;
            targety.y = transform.position.y;

            transform.LookAt(targety);
        }

    }

    IEnumerator attacker()
    {
        animator.SetBool("isAttackReady", true);
        getTouch = false;
        DontMove = true;
        particle_attack.Play();
        yield return new WaitForSeconds(2.5f); //공격 준비

        animator.SetBool("isAttack", true);

        yield return new WaitForSeconds(0.2f); //패는 애니 중간
        isrotation = true;
        Vector3 attackpos = transform.position;
        attackpos.y = transform.position.y + 3f;
        //attackpos.x = transform.position.x + 2f;
        //attackpos.z = transform.position.z + 2f;

        GameObject spawnSlashObject = Instantiate(Bullet, attackpos, transform.rotation);
        animator.SetBool("isAttack", false);

        yield return new WaitForSeconds(0.9f); //패는 애니 중간
        getTouch = true;

        particle_attack.Stop();

        yield return new WaitForSeconds(attackDelaytime);
        animator.SetBool("isAttackReady", false);
        isattack = false;
        DontMove = false;
        isrotation = false;
    }


    IEnumerator damaged_ani(int random)
    {
        animator.SetInteger("Hit", random);
        yield return new WaitForSeconds(0.6f);
        animator.SetInteger("Hit", 0);
    }


    private IEnumerator AttackCor()
    {
        DontMove = true;
        GameObject spawnSlashObject = Instantiate(Bullet, transform.position, transform.rotation);

        yield return new WaitForSeconds(5f);
    }
}
