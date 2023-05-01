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

    private GameObject damageEffect;
    public GameObject attackcollider;

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
        NotDamaged();
        DieMonster();

        targetPos = player.transform.position;

        if (Vector3.Distance(targetPos, transform.position) < attackRange) //������ ĳ������ �Ÿ��� attackRange ���� �۰� �������� �ƴ� ��
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

        if (Vector3.Distance(targetPos, transform.position) < 3) //����
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Main_gangrim" && isDash)
        {
            isDash = false;
            player.GetDamage(dashAttackDamage);
        }
    }
    protected override void DieMonster()
    {
        if (curHearth < 1)
        {
            gameObject.SetActive(false);
            anim.SetBool("isDie", true);
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
        int rand = Random.Range(0, 2);
        if (rand == 0)
            JumpAttack();
        else if (rand == 1)
            DashAttack();

    }

    private void BaseAttack()
    {
        isMove = false;
        player.GetDamage(baseAttackDamage);
        StartCoroutine(BaseAttackCor());
        anim.SetTrigger("BaseAttack");
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

    private IEnumerator BaseAttackCor()
    {

        yield return new WaitForSeconds(1.8f);
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
        GameObject effect = Instantiate(effects[2], transform.position, Quaternion.identity);
        while (time > 0)
        {
            time -= Time.deltaTime;
            transform.position += dir * Time.deltaTime * 5f;
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(0.5f);

        isMove = true;
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

        yield return new WaitForSeconds(1f);
        isMove = true;
    }

    public void EffectSpawn(int index)
    {
        GameObject temp = Instantiate(effects[index], new Vector3(transform.position.x, 3f, transform.position.z), Quaternion.identity);
        if (index == 1)
        {
            Vector3 dir = tempPos - transform.position;
            temp.transform.forward = dir * -1;
        }
    }
}







    ////Ch
    //private GameObject damageEffect;
    //private Animator animator;
    //public SkinnedMeshRenderer mat;

    //public GameObject MonsterObject; //���� �θ� �޾ƿ�
    //public GameObject targetPosition; //playerposition
    //public GameObject Boss_Skill_1;

    //public Transform targettransform;

    //public Rigidbody Boss_Skill_2; //���� ��ų ��ȯ

    //private Vector3 Rush_Target_Vector3; //���� �� �÷��̾� ��ġ ����
    //private Vector3 targettrans_every; //Ÿ�� ����

    //public float BossPatternTime; //���� ���� �� Ÿ�� ���� 6��
    //public float chasespeed; //���� �̵��ӵ�


    //[SerializeField]  //Ȯ�ο� ��������
    //private float random;

    ////Ÿ�̸�
    //float t = 0; //Ÿ�̸�
    //float rush_t = 0; //Ÿ�̸�
    //float time = 0;

    //[SerializeField]
    //float attacktime = 0;
    //int i = 0;
    //int n = 1;
    //int e = 0;
    //float timer = 0;
    //bool skill1_ = false;

    ////���� Ÿ�̸� - �� �ð������� �������� �Ұ�, �����̴°� �Ұ�
    //float patterntimer;
    ////���� Ÿ�̸� �� ������ �ð�.. 0�̸� ���� �� �ٷ� ���� ����
    //float patterndelaytimer;

    //public float MonsterYPosition = 2f; //y�� ���ִϱ� ���� ����;; //y �� ������ ȸ��X
    //float MaxRushCount;     //�ִ� ���� ���� 2~3 ���� ����
    //float RushCount;

    //[SerializeField]
    //float BossHp; //���� ���� ü��
    //float BossPattern2HP; //���� ���� ���� ü��


    //bool BossMonsterDie = false; //������ ����Ͽ��°�
    //bool TargetHere; //�÷��̾ ���� ������ ����(Trigger)�ȿ� ���� //�ȵ����� ���� ����
    //bool StartAttack;
    //bool isDamage;

    ////���� ��������
    //bool isrush;//�ִ뵹��Ƚ�� ������ �����
    //bool rushfirst;
    //bool pattern1_rush;
    //bool dorush;//���� �ð�

    //[SerializeField] //Ȯ�ο� üũ�� ���� 2������
    //bool Boss_2Page_Pattern = false; // true�� 2 ������ ����
    //bool have_targeting = false;

    //public float MaxDistance = 5f; //Ray ����
    //RaycastHit hit;

    //bool FirstNearAttack;
    //bool onetime = true;
    //bool DontMove;
    //bool ispattern2;


    //public enum BossPattern //���� ��������
    //{
    //    //0 �������� 1 �ٴ� ������� 2 ���������� 3 ȭ���(����)
    //    Pattern_0 = 0,
    //    Pattern_1,
    //    Pattern_2
    //}
    //public BossPattern bosspattern;

    //void Awake()
    //{

    //}

    //void Start() //�ʱ�ȭ
    //{
    //    onetime = true;
    //    BossPattern2HP = BossHp / 2;
    //    targetPosition = GameObject.FindWithTag("Main_gangrim"); //Player �±׸� ���� ������Ʈ�� ã��
    //    targettransform = GameObject.FindWithTag("Main_gangrim").transform;
    //    animator = GetComponent<Animator>();
    //    animator.SetBool("isIdle", true);
    //}

    //void Update()
    //{
    //    patterntimer += Time.deltaTime;

    //    //���� �̵�
    //    monsterMove();

    //    //���� ���� ���� //update ���� ��� ����Ǿ�ߵ�(�����̶�)
    //    if (pattern1_rush)
    //    {
    //        Debug.Log("pattern1_rush");
    //        rush();
    //    }
    //    else
    //    {
    //        dorush = false;
    //        rushfirst = false;

    //        animator.SetBool("isChargeattack_Set", false);
    //        animator.SetBool("isChargeattack_Go", false);
    //    }

    //    if (ispattern2)
    //    {
    //        animator.SetBool("isWalk", false);
    //        timer += Time.deltaTime;

    //        if (e < 5)
    //        {
    //            if (timer > 1f)
    //            {
    //                timer = 0;
    //                e++;
    //                ThrowRock();
    //            }
    //        }
    //        else
    //        {
    //            ispattern2 = false;
    //            e = 0;
    //        }
    //    }

    //    if (!have_targeting) //�� ������� ����
    //    {
    //        targettrans_every = targettransform.position;
    //        targettrans_every.y = MonsterYPosition;
    //        have_targeting = true;
    //    }

    //    //***********************************
    //    //���� ���� ���� ����

    //    if (!onetime)
    //    {
    //        StopAllCoroutines();
    //        animator.SetBool("isJumpAttack", false); //...
    //        random = UnityEngine.Random.Range(0, 3); // 0 1 2
    //        BossAttack(random);
    //        DontMove = true;
    //        StartCoroutine("Move_Time"); //$ 1 2 ���Ͽ� �ʿ�
    //        animator.SetBool("isWalk", false);
    //        onetime = true;
    //    }
    //    else
    //    {
    //        have_targeting = false;
    //    }

    //    //****************************************************
    //    //���� 2������ ����  // ���� ü�� ��������
    //    if (BossHp < BossPattern2HP)
    //    {
    //        Boss_2Page_Pattern = true;
    //    }

    //    //���� ���
    //    if (BossMonsterDie == true)
    //    {
    //        Debug.Log("BossMonsterDie");
    //        animator.SetBool("isDie", true);
    //        //Destroy(MonsterObject);
    //    }

    //}

    //void monsterMove()
    //{
    //    time += Time.deltaTime;
    //    attacktime += Time.deltaTime;

    //    //�ϴ� �����ɽ�Ʈ ���� �÷��̾�� ���� ������ �������� �ƴϸ� ������������
    //    Vector3 spawn = targettransform.position;
    //    spawn.y = MonsterYPosition; //���� �ٴڿ� �ȹ����� �ڸ� 

    //    Vector3 rayspawn = transform.position;
    //    rayspawn.y = MonsterYPosition; //���� �ٴڿ��� ���� ��Ը���

    //    //����� �÷��̾ �i�ٰ� �����ɽ�Ʈ�� ������ ������ ���� ����
    //    Debug.DrawRay(rayspawn, transform.forward * MaxDistance, Color.blue, 0.05f);

    //    if (Physics.Raycast(rayspawn, transform.forward, out hit, MaxDistance))
    //    {
    //        if (hit.collider.tag == "Main_gangrim")
    //        {
    //            animator.SetBool("isWalk", false);
    //            StartAttack = true;
    //        }

    //    }
    //    else
    //    {
    //        attacktime = 0;
    //        StartAttack = false;
    //    }

    //    if (StartAttack)
    //    {
    //        animator.SetBool("isWalk", false);
    //        transform.LookAt(spawn);
    //        //�÷��̾� ������
    //        //������ ����
    //        //�÷��̾ �� �ִ� ��ǥ���� ��ä�� ����(������ ������Ʈ Ű�� ���°ɷ�)�� 2ȸ �����Ѵ�.
    //        //�÷��̾ ���� �ȿ� ���� �� ���

    //        if (attacktime > 2f && attacktime < 5f)
    //        {
    //            if (!FirstNearAttack) //�� ���ǹ����� ���� ���� 1ȸ��
    //            {
    //                time = 0;
    //                StartCoroutine(BackIdle(2));

    //                //animator.SetBool("isAttack", true);
    //                FirstNearAttack = true;
    //                //������ �ִ� ������Ʈ �����ѱ�
    //                //BossAttacker.SetActive(true);
    //            }

    //        }
    //        else if (attacktime > 5f)
    //        {
    //            //animator.SetBool("isAttack", false);
    //            attacktime = 0;
    //            FirstNearAttack = false;
    //        }
    //    }
    //    else
    //    {
    //        //�� ������ n�� ���� ���ϸ�
    //        if (time > BossPatternTime)
    //        {
    //            //animator.SetBool("isAttack", false);
    //            onetime = false;//�����ʿ� //������������ �Ѿ
    //            time = 0;
    //        }

    //        if (!DontMove)
    //        {
    //            //player chase
    //            animator.SetBool("isJumpAttack", false);
    //            animator.SetBool("isWalk", true);
    //            transform.position = Vector3.MoveTowards(gameObject.transform.position, spawn, chasespeed * Time.deltaTime);
    //            transform.LookAt(spawn);
    //        }
    //        else
    //        {
    //            animator.SetBool("isWalk", false);
    //        }

    //    }
    //}

    //void BossAttack(float random)
    //{

    //    if (random == 0)
    //    {
    //        bosspattern = BossPattern.Pattern_0;
    //    }
    //    else if (random == 1)
    //    {
    //        bosspattern = BossPattern.Pattern_1;
    //    }
    //    else if (random == 2)
    //    {
    //        bosspattern = BossPattern.Pattern_2;
    //    }

    //    //0 �������� 1 �ٴ� ������� 2 ����������
    //    switch (bosspattern)
    //    {
    //        case BossPattern.Pattern_0:

    //            pattern1_rush = true;
    //            animator.SetBool("isWalk", false);
    //            //Debug.Log("Boss Monster pattern0");

    //            break;

    //        case BossPattern.Pattern_1:

    //            FloorDown();
    //            animator.SetBool("isWalk", false);
    //            //animator.SetBool("isJumpAttack", true);
    //            Debug.Log("Pattern_1");
    //            skill1_ = false;



    //            break;
    //        case BossPattern.Pattern_2:
    //            animator.SetBool("isWalk", false);
    //            ispattern2 = true;

    //            break;

    //        default:
    //            Debug.Log("????");
    //            break;

    //    }

    //}


    //void rush() //���� ���� dorush = false; //0��° ���Ͽ� ���
    //{

    //    if (!rushfirst)  //ó�� �÷��̾� ��ġ �����
    //    {
    //        Vector3 spawn0 = targettransform.position;
    //        spawn0.y = MonsterYPosition; //���� �ٴڿ� �ȹ����� �ڸ�
    //        transform.LookAt(spawn0);
    //        animator.SetBool("isChargeattack_Set", true);
    //        Rush_Target_Vector3 = transform.position; //�� ���ָ� 0.0.0�� �̵��ؼ� �̰� ��.. //������ġ
    //                                                  // Rush_Target_Vector3 = targettransform.position;
    //        rushfirst = true;
    //    }

    //    t += Time.deltaTime;
    //    rush_t += Time.deltaTime;

    //    //ĳ���� ���鼭 �뽬
    //    Vector3 spawn1 = targettransform.position;
    //    spawn1.y = MonsterYPosition; //���� �ٴڿ� �ȹ����� �ڸ�
    //                                 // transform.LookAt(spawn1);


    //    if (!isrush) //isrush = false �� ������ ����
    //    {
    //        if (!Boss_2Page_Pattern)
    //        {
    //            MaxRushCount = UnityEngine.Random.Range(1, 3); // 1,2 �� ����
    //                                                           //Debug.Log("MaxRushCount: " + MaxRushCount);
    //            isrush = true;

    //        }
    //        else //���� 2������ //���� Ƚ�� ����
    //        {
    //            MaxRushCount = UnityEngine.Random.Range(2, 4); // 2,3 �� ����
    //                                                           //Debug.Log("MaxRushCount: " + MaxRushCount);
    //            isrush = true;
    //        }
    //    }

    //    if (!dorush) //���� �ڵ�
    //    {

    //        if (i <= MaxRushCount) //���� ����Ƚ��
    //        {
    //            //��� ���ư�
    //            //animator.SetBool("isChargeattack_Set", true);
    //            Vector3 spawn = targettransform.position;
    //            spawn.y = MonsterYPosition; //���� �ٴڿ� �ȹ����� �ڸ�


    //            if (rush_t > 2f) //���� (�ӹ��� �ð� �� ���� ����)
    //            {
    //                StartCoroutine(BackIdle(0));
    //                //animator.SetBool("isChargeattack_Go", false);
    //                Rush_Target_Vector3 = spawn;
    //                transform.LookAt(spawn);
    //                if (transform.position.z > spawn.z)
    //                {
    //                    n = -1;
    //                }
    //                else
    //                {
    //                    n = 1;
    //                }

    //                Rush_Target_Vector3.z = spawn.z + 5 * n; //���� ++
    //                i++;
    //                rush_t = 0;
    //            }

    //            transform.position = Vector3.MoveTowards(gameObject.transform.position, Rush_Target_Vector3, 30f * Time.deltaTime);//3��° �� ���� �ӵ�
    //                                                                                                                               //animator.SetBool("isChargeattack_Go", true);
    //        }
    //        else //�뽬 ����
    //        {
    //            animator.SetBool("isChargeattack_Set", false);
    //            i = 0;
    //            isrush = false; //
    //            dorush = true; //false�� �ٽ� ���� ����

    //            StopCoroutine(BackIdle(0));
    //            animator.SetBool("isChargeattack_Go", false);
    //            pattern1_rush = false;
    //        }

    //    }

    //}

    //void FloorDown() //�ٴ� ������� //1��° ����
    //{
    //    StartCoroutine(BackIdle(1));
    //    //animator.SetBool("isWalk", false);

    //    Vector3 spawn = targettrans_every;
    //    spawn.z = UnityEngine.Random.Range(2, 10) + targettrans_every.z;
    //    spawn.y = MonsterYPosition;

    //    Vector3 spawn1 = targettrans_every;
    //    spawn1.z = UnityEngine.Random.Range(-2, -10) + targettrans_every.z;
    //    spawn1.y = MonsterYPosition;

    //    Vector3 spawn2 = targettrans_every;
    //    spawn2.z = UnityEngine.Random.Range(4, 10) + targettrans_every.z;
    //    spawn2.x = UnityEngine.Random.Range(-4, -10) + targettrans_every.x;
    //    spawn2.y = MonsterYPosition;

    //    Vector3 spawn3 = targettrans_every;
    //    spawn3.x = UnityEngine.Random.Range(2, 10) + targettrans_every.x;
    //    spawn3.y = MonsterYPosition;

    //    Vector3 spawn4 = targettrans_every;
    //    spawn4.x = UnityEngine.Random.Range(-2, -10) + +targettrans_every.x;
    //    spawn4.y = MonsterYPosition;

    //    if (!skill1_)
    //    {
    //        //animator.SetBool("isWalk", false);
    //        //animator.SetBool("isJumpAttack", false); //...
    //        //���߿� �迭�� ����
    //        GameObject Skill1 = Instantiate(Boss_Skill_1, spawn, transform.rotation);
    //        GameObject Skill2 = Instantiate(Boss_Skill_1, spawn1, transform.rotation);
    //        GameObject Skill3 = Instantiate(Boss_Skill_1, spawn2, transform.rotation);
    //        GameObject Skill4 = Instantiate(Boss_Skill_1, spawn3, transform.rotation);
    //        GameObject Skill5 = Instantiate(Boss_Skill_1, spawn4, transform.rotation);
    //        skill1_ = true;
    //    }

    //}


    //bool skill2_;

    //void ThrowRock() //���� ������
    //{
    //    animator.SetBool("isWalk", false);
    //    Vector3 spawn = targettransform.position;
    //    spawn.y = MonsterYPosition;

    //    if (!skill2_)
    //    {
    //        transform.LookAt(spawn); //�� ���� ���� ���� + ���� ����
    //        skill2_ = true;
    //        StartCoroutine("Rook_Time");
    //        Rigidbody ThrowRockrigid = Instantiate(Boss_Skill_2, transform.position, transform.rotation);
    //        ThrowRockrigid.velocity = transform.forward * 20;//  * Time.deltaTime;

    //    }
    //}

    //private void OnTriggerEnter(Collider other) //Trigger�� ����
    //{
    //    if (other.gameObject.tag == "Main_gangrim")
    //    {
    //        //Debug.Log("Collision  Trigger to Player");
    //        TargetHere = true;
    //    }

    //    if (other.tag == "Weapon")
    //    {
    //        Main_Player player = other.GetComponent<HitScript>().Player;
    //        if(player.isAttack)
    //        {
    //            HitScript Hit = other.GetComponent<HitScript>();
    //            BossHp -= Hit.damage;

    //            if(damageEffect == null)
    //            {
    //               damageEffect = Instantiate(player.AtkEffect[3], new Vector3(transform.position.x, 5f, transform.position.z), Quaternion.identity);
    //            }
    //            else
    //            {
    //               damageEffect.transform.position = new Vector3(transform.position.x, 5f, transform.position.z);
    //            }
    //            damageEffect.transform.localScale = new Vector3(5,5,5);
    //            damageEffect.SetActive(false);
    //            damageEffect.SetActive(true);
    //            StartCoroutine(OnDamge());
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Main_gangrim")
    //    {
    //        TargetHere = false;
    //    }
    //}

    //private void OnCollisionEnter(Collision collision) //Collier�� ���� //���� ����
    //{
    //    if (collision.gameObject.tag == "Main_gangrim")
    //    {
    //        Debug.Log("Collision to Player");

    //    }
    //}

    //void BossDamage() //�÷��̾� �ʿ��� ȣ�� //ȣ�� �� ü�� 1�� ��� ü�� 0�Ͻ� BossMonsterDie
    //{
    //    Debug.Log("MonsterDamage()");

    //    if (BossHp > 1)
    //    {
    //        BossHp--;
    //    }
    //    else
    //    {

    //        BossMonsterDie = true;
    //        //MonsterDie();
    //    }

    //}

    //IEnumerator OnDamge()
    //{
    //    mat.GetComponent<SkinnedMeshRenderer>().material.color = Color.red;
    //    yield return new WaitForSeconds(0.1f);

    //    if (BossHp > 0)
    //    {
    //        mat.GetComponent<SkinnedMeshRenderer>().material.color = Color.white;
    //    }
    //    else
    //    {
    //        mat.GetComponent<SkinnedMeshRenderer>().material.color = Color.gray;
    //        Destroy(gameObject, 10);
    //    }
    //}

    //IEnumerator Move_Time()
    //{
    //    yield return new WaitForSeconds(BossPatternTime - 1f); //3f
    //    DontMove = false;
    //}

    //IEnumerator Rook_Time()
    //{
    //    skill2_ = false;
    //    yield return new WaitForSeconds(1f);
    //}


    //IEnumerator BackIdle(int i)
    //{
    //    if (i == 0)
    //    {
    //        Debug.Log("BackIdle -- 0");

    //        //yield return new WaitForSeconds(0.5f);
    //        animator.SetBool("isChargeattack_Go", true);
    //        yield return new WaitForSeconds(1.2f);
    //        animator.SetBool("isChargeattack_Go", false);

    //        //animator.SetBool("isChargeattack_Set", false);

    //    }
    //    else if (i == 1)//���� 1 �ִϸ��̼�
    //    {
    //        Debug.Log("BackIdle");
    //        animator.SetBool("isJumpAttack", true);
    //        yield return new WaitForSeconds(2.3f);
    //        animator.SetBool("isJumpAttack", false);
    //    }
    //    else if (i == 2)
    //    {
    //        Debug.Log("BackIdle");
    //        animator.SetBool("isAttack", true);
    //        yield return new WaitForSeconds(1.6f);
    //        animator.SetBool("isAttack", false);
    //    }
    //    //isJumpAttack
    //    //isAttack
    //}


