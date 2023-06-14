using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_Script : Enemy
{
    public GameObject RockBullet;
    public GameObject GolemObj;

    [SerializeField] float attackDelaytime;
    [SerializeField] bool miniGolem;
    [SerializeField] ParticleSystem particle_attack;

    private Animator animator;

    bool isattack;
    bool DontMove;
    bool isdie;
    bool isskillattack;

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
        if(GolemObj == null)
        GolemObj = gameObject;

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
            OnDisable();

            deletetime += Time.deltaTime;

            if (deletetime >= 5f)
            {
                GolemObj.gameObject.SetActive(false);
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
                if (!isattack && !isskillattack)
                {
                    timer = 0;
                    transform.LookAt(targety);
                    isattack = true;
                    StartCoroutine("attacker");
                }
            }
            else if (Dist > MaxDistance + 3f && !isattack && !miniGolem)
            {
                timer += Time.deltaTime;
                if (timer > 3)
                {
                    if (!isskillattack)
                    {
                        transform.LookAt(targety);
                        isskillattack = true;
                        StartCoroutine("skillerattacker");
                    }
                }

            }
        }
    }



    IEnumerator attacker()
    {
        DontMove = true;
        animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.95f); //패는 애니 중간
        particle_attack.Play();
        getTouch = false;
        animator.SetBool("isAttack", false);

        yield return new WaitForSeconds(0.9f); //패는 애니 중간
        getTouch = true;
        particle_attack.Stop();
        yield return new WaitForSeconds(attackDelaytime);
        isattack = false;
        timer = 0;
        if (Dist > MaxDistance)
        {
            DontMove = false;
        }

    }

    IEnumerator skillerattacker()
    {

        animator.SetBool("SkillAttack", true);
        getTouch = false; //공격
        DontMove = true;
        yield return new WaitForSeconds(0.95f); //스킬

        Vector3 targetPosition = targetTransform.position;
        Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        Vector3 skillAttackPosition = targetPosition + randomOffset;

        skillAttackPosition.y = targetPosition.y;
        RockBullet.transform.position = skillAttackPosition;

        yield return new WaitForSeconds(0.5f); //스킬

        RockBullet.SetActive(true);

        animator.SetBool("SkillAttack", false);

        //공격 종료
        yield return new WaitForSeconds(attackDelaytime);
        RockBullet.SetActive(false);

        getTouch = true;
        isskillattack = false;
        timer = 0;
        DontMove = false;

    }

    IEnumerator damaged_ani(int random)
    {
        animator.SetInteger("Hit", random);
        yield return new WaitForSeconds(0.6f);
        animator.SetInteger("Hit", 0);
    }
}
