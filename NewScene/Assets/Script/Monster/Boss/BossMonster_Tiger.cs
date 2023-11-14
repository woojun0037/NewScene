using DG.Tweening;
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
     public GameObject SlashObj;
    private Animator anim;

    private Vector3 tempPos;
    private bool isAttack;
    private bool isMove;
    //private bool isDash;
    private bool StartDist = false;

    public GameObject StoneDoor;
    public GameObject NextStage;
    public EmissionIntensityController emissionController;

    public GameObject effectAttack;

    private float random;
    private float currentrandom;

    protected override void Awake()
    {
        anim = GetComponent<Animator>();
    }
    protected override void Start()
    {
        emissionController = GetComponent<EmissionIntensityController>();


        player = GameObject.FindWithTag("Main_gangrim").GetComponent<Main_Player>();
        BossStart();
    }

    protected override void BossStart()
    {
        base.BossStart();
    }

    protected override void BossHit()
    {
        base.BossHit();
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
                StartDist = true;
                if (Vector3.Distance(targetPos, transform.position) < attackRange / 2)
                {
                    if (!isAttack)
                    {
                        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

                        isAttack = true;
                        anim.SetBool("isWalk", false);
                        CloseAttackChoice();
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
            else
            {
                if (!isAttack && StartDist)
                {
                    isMove = true;
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

            //if (isAttack)
            //{
            //    if (attackCoolTime >= 0)
            //    {
            //        attackCoolTime -= Time.deltaTime;
            //    }
            //    else
            //    {
            //        isAttack = false;
            //        attackCoolTime = 5f;
            //    }
            //}

        }
        else
        {
            anim.SetBool("isWalk", false);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Main_gangrim" && isDash)
    //    {
    //        isDash = false;
    //        player.GetDamage(dashAttackDamage);
    //    }
    //}
    protected override void DieMonster()
    {
        if (curHearth < 1)
        {
            anim.SetBool("isDie", true);
            isDie = true;
            StoneDoor.SetActive(false);
            NextStage.SetActive(true);
        }
    }

    private void Move()
    {
        Vector3 dir = targetPos - transform.position;
        transform.forward = dir;
        transform.position += new Vector3(dir.x, 0f, dir.z) * Time.deltaTime * moveSpeed;
    }

    private void AttackChoice()
    {
        isMove = false;
        random = Random.Range(0, 3);//012

        if (random == currentrandom)
        {
            if (random < 2)
                random++;
            else
                random--;
        }

        currentrandom = random;

        if (currentrandom == 0)
            JumpAttack();
        else if (currentrandom == 1)
            StartCoroutine(DashAndDash());
        else if (currentrandom == 2)
            SlashAttack();
    }

    private void CloseAttackChoice()
    {
        isMove = false;

        random = Random.Range(0, 4);//0123

        if (random == currentrandom)
        {
            if (random < 3)
                random++;
            else
                random--;
        }

        currentrandom = random;


        if (currentrandom == 0)
            JumpAttack();
        else if (currentrandom == 1)
            StartCoroutine(DashAndDash());
        else if (currentrandom == 2)
            SlashAttack();
        else if (currentrandom == 3)
            BaseAttack();
    }


    private void BaseAttack()
    {
        isMove = false;
        StartCoroutine(BaseAttackCor());
    }

    private void SlashAttack()
    {
        StartCoroutine(SlashAttackCor());
    }

    private void JumpAttack()
    {
        StartCoroutine(JumpAttackCor());
    }

    private IEnumerator DashAndDash()
    {
        isAttack = true;
        isMove =false;

        for (int i = 0; i < 2; i++)
        {
            DashAttack();
            yield return new WaitForSeconds(1.8f);
            Vector3 targetPosition = player.transform.position;
            targetPosition.y = transform.position.y;

            transform.LookAt(targetPosition);
        }
        yield return new WaitForSeconds(1f);
        isMove = true;
        isAttack = false;
    }

    private void DashAttack()
    {
        //isDash = true;
        StartCoroutine(DashAttackCor());
        anim.SetTrigger("DashAttack");
    }

    private IEnumerator SlashAttackCor()
    {
        isMove = false;
        isAttack = true;
        //구슬
        emissionController.selectcolor(255, 255, 0);
        emissionController.EmssionMaxMin();
        yield return new WaitForSeconds(0.8f);

        anim.SetTrigger("SlashAttack");

        GameObject spawnSlashObject = Instantiate(SlashObj, transform.position, transform.rotation);

        yield return new WaitForSeconds(4f);
        isMove = true;
        isAttack = false;
    }

    private IEnumerator BaseAttackCor()
    {
        isMove = false;
        isAttack = true;
        //할퀴기
        emissionController.selectcolor(0, 252, 255);
        emissionController.EmssionMaxMin();

        yield return new WaitForSeconds(1f);

        anim.SetTrigger("BaseAttack");

        yield return new WaitForSeconds(0.6f);
        effectAttack.SetActive(true);
        attackcollider.SetActive(true);
        yield return new WaitForSeconds(1f);
        effectAttack.SetActive(false);
        attackcollider.SetActive(false);
        isMove = true;
        isAttack = false;
    }

    private IEnumerator DashAttackCor()
    {
        //대쉬
        emissionController.selectcolor(255, 0, 0);
        emissionController.EmssionMaxMin();


        bool isTarget = false;
        float time = 0.3f;
        tempPos = Vector3.zero;

        if (!isTarget)
        {
            tempPos = new Vector3(targetPos.x, transform.position.y, targetPos.z); 

            isTarget = true;
        }
        yield return new WaitForSeconds(0.5f);

        Vector3 dir = tempPos - transform.position;
        transform.forward = dir;
        yield return new WaitForSeconds(0.5f);
        Instantiate(effects[2], transform.position, Quaternion.identity);
        while (time > 0)
        {
            attackcollider.SetActive(true);
            time -= Time.deltaTime;
            transform.position += new Vector3(dir.x, 0f, dir.z) * Time.deltaTime * 5f; 
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(0.5f);
        attackcollider.SetActive(false);
    }

    private IEnumerator JumpAttackCor()
    {
        isAttack = true;
        //돌맹이
        emissionController.selectcolor(0, 0, 255);
        emissionController.EmssionMaxMin();
        yield return new WaitForSeconds(0.8f);

        anim.SetTrigger("JumpAttack");

        float time = 2f;
        while (time > 0f)
        {
            time -= 0.05f;
            yield return new WaitForSeconds(0.01f);

        }

        yield return new WaitForSeconds(2.5f);

        yield return new WaitForSeconds(3f);
        isAttack = false;
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