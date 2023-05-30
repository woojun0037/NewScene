using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster_Tiger : Boss
{
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private GameObject[] effects;

    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackCoolTime;
    [SerializeField] private float baseAttackDamage;
    [SerializeField] private float dashAttackDamage;

    public SkinnedMeshRenderer mat;
    public GameObject attackcollider;
    public GameObject throwRockObj;

    private Animator anim;

    private Vector3 tempPos;
    private bool isAttack;
    private bool isMove;
    private bool isDash;

    protected override void Awake()
    {
        anim = GetComponent<Animator>();
    }
    protected override void Start()
    {
        player = GameObject.FindWithTag("Main_gangrim").GetComponent<Main_Player>();
    }

    private void Update()
    {
        if (player.HP >= 0 && !isDie)
        {
            NotDamaged();
            DieMonster();

            targetPos = player.transform.position;

            if (Vector3.Distance(targetPos, transform.position) < attackRange) //보스와 캐릭터의 거리가 attackRange 보다 작고 공격중이 아닐 때
            {
                if (Vector3.Distance(targetPos, transform.position) < attackRange / 2)
                {
                    if (!isAttack)
                    {
                        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

                        isAttack = true;
                        anim.SetBool("isWalk", false);
                        BaseAttack();
                    }
                }
                else
                {
                    if (!isAttack)
                    {
                        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

                        isAttack = true;
                        anim.SetBool("isWalk", false);
                        AttackChoice();
                    }
                }

            }

            if (Vector3.Distance(targetPos, transform.position) < 3) //멈춤
            {
                isMove = false;
                anim.SetBool("isWalk", false);

                if (!isAttack)
                {
                    transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

                    isAttack = true;
                    anim.SetBool("isWalk", false);
                    BaseAttack();
                }
            }

            if (isMove)
            {
                anim.SetBool("isWalk", isMove);
                Move();
            }

            if (isAttack)
            {
                if (attackCoolTime >= 0)
                {
                    attackCoolTime -= Time.deltaTime;
                }
                else
                {
                    isAttack = false;
                    attackCoolTime = 5f;
                }
            }

        }
        else
        {
            anim.SetBool("isWalk", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Main_gangrim" && isDash)
        {
            isDash = false;
            player.GetDamage(dashAttackDamage);
        }
    }
    protected override void DieMonster()
    {
        if (curHearth < 1)
        {
            anim.SetBool("isDie", true);
            isDie = true;
        }
    }

    private void Move()
    {
        Vector3 dir = targetPos - transform.position;
        transform.forward = dir;
        transform.position += dir * Time.deltaTime * moveSpeed;
    }

    private void AttackChoice()
    {
        isMove = false;
        int rand = Random.Range(0, 3);
        if (rand == 0)
            JumpAttack();
        else if (rand == 1)
            StartCoroutine(DashAndDash());
        else if (rand == 2)
            RockAttack();
    }

    private void BaseAttack()
    {
        isMove = false;
        player.GetDamage(baseAttackDamage);
        StartCoroutine(BaseAttackCor());
        anim.SetTrigger("BaseAttack");
    }

    private void SlashAttack()
    {
        anim.SetTrigger("SlashAttack");
    }
    private void DashAttack()
    {
        isDash = true;
        StartCoroutine(DashAttackCor());
        anim.SetTrigger("DashAttack");
    }

    private void JumpAttack()
    {
        StartCoroutine(JumpAttackCor());
        anim.SetTrigger("JumpAttack");
    }

    private void RockAttack()
    {
        StartCoroutine(ThrowRockCor());
        //anim.SetTrigger("ThrowRock");
    }

    void ThrowRock() //바위 던지기
    {
        Vector3 spawn = player.transform.position;
        spawn.y = transform.position.y;
        transform.LookAt(spawn); //그 방향 보고 던짐 + 방향 고정

        Vector3 rockpos = transform.position;
        rockpos.y = transform.position.y +5f;
        GameObject ThrowRockrigid = Instantiate(throwRockObj, rockpos, transform.rotation);
    }

    private IEnumerator ThrowRockCor()
    {
        isMove = false;
        for (int i = 0; i < 5; i++)
        {
            ThrowRock();
            yield return new WaitForSeconds(0.7f);
        }
        isMove = true;
    }


    private IEnumerator DashAndDash()
    {
        for (int i = 0; i < 2; i++)
        {
            DashAttack();
            yield return new WaitForSeconds(1.8f);
            transform.LookAt(player.transform);
        }
        isMove = true;

    }

    private IEnumerator BaseAttackCor()
    {
        attackcollider.SetActive(true);
        yield return new WaitForSeconds(1.8f);
        attackcollider.SetActive(false);
        isMove = true;
    }

    private IEnumerator DashAttackCor()
    {
        bool isTarget = false;
        float time = 0.3f;
        tempPos = Vector3.zero;

        if (!isTarget)
        {
            tempPos = targetPos;
            isTarget = true;
        }
        yield return new WaitForSeconds(0.5f);

        Vector3 dir = tempPos - transform.position;
        transform.forward = dir;
        yield return new WaitForSeconds(0.5f);
        Instantiate(effects[2], transform.position, Quaternion.identity);
        while (time > 0)
        {
            time -= Time.deltaTime;
            transform.position += dir * Time.deltaTime * 5f;
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(0.5f);

        //isMove = true;
        isDash = false;
    }

    private IEnumerator JumpAttackCor()
    {
        float time = 2f;
        while (time > 0f)
        {
            time -= 0.05f;
            yield return new WaitForSeconds(0.01f);
            //if (time > 1f)
            //    transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
            //else
            //    transform.position = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
        }

        yield return new WaitForSeconds(2.5f);
        isMove = true;
    }

    public void EffectSpawn(int index)
    {
        GameObject temp = Instantiate(effects[index], new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
        if (index == 1)
        {
            Vector3 dir = tempPos - transform.position;
            temp.transform.forward = dir * -1;
        }
    }
}