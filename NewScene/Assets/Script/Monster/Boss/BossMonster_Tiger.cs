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

    //public GameObject MonsterObject; //몬스터 부모 받아옴
    //public GameObject targetPosition; //playerposition
    //public GameObject Boss_Skill_1;

    //public Transform targettransform;

    //public Rigidbody Boss_Skill_2; //보스 스킬 소환

    //private Vector3 Rush_Target_Vector3; //돌진 중 플레이어 위치 받음
    //private Vector3 targettrans_every; //타켓 고정

    //public float BossPatternTime; //보스 패턴 쿨 타임 보통 6초
    //public float chasespeed; //몬스터 이동속도


    //[SerializeField]  //확인용 지워도됨
    //private float random;

    ////타이머
    //float t = 0; //타이머
    //float rush_t = 0; //타이머
    //float time = 0;

    //[SerializeField]
    //float attacktime = 0;
    //int i = 0;
    //int n = 1;
    //int e = 0;
    //float timer = 0;
    //bool skill1_ = false;

    ////패턴 타이머 - 이 시간동안은 근접공격 불가, 움직이는거 불가
    //float patterntimer;
    ////패턴 타이머 후 딜레이 시간.. 0이면 패턴 후 바로 다음 패턴
    //float patterndelaytimer;

    //public float MonsterYPosition = 2f; //y값 안주니까 땅에 박힘;; //y 값 고정용 회전X
    //float MaxRushCount;     //최대 돌진 갯수 2~3 랜덤 받음
    //float RushCount;

    //[SerializeField]
    //float BossHp; //보스 기초 체력
    //float BossPattern2HP; //보스 다음 패턴 체력


    //bool BossMonsterDie = false; //보스가 사망하였는가
    //bool TargetHere; //플레이어가 보스 몬스터의 범위(Trigger)안에 들어옴 //안들어오면 패턴 공격
    //bool StartAttack;
    //bool isDamage;

    ////보스 돌진관련
    //bool isrush;//최대돌진횟수 랜덤값 변경용
    //bool rushfirst;
    //bool pattern1_rush;
    //bool dorush;//돌진 시간

    //[SerializeField] //확인용 체크시 보스 2페이지
    //bool Boss_2Page_Pattern = false; // true시 2 페이지 시작
    //bool have_targeting = false;

    //public float MaxDistance = 5f; //Ray 길이
    //RaycastHit hit;

    //bool FirstNearAttack;
    //bool onetime = true;
    //bool DontMove;
    //bool ispattern2;


    //public enum BossPattern //보스 공격패턴
    //{
    //    //0 돌진공격 1 바닥 내려찍기 2 바위던지기 3 화살비(보류)
    //    Pattern_0 = 0,
    //    Pattern_1,
    //    Pattern_2
    //}
    //public BossPattern bosspattern;

    //void Awake()
    //{

    //}

    //void Start() //초기화
    //{
    //    onetime = true;
    //    BossPattern2HP = BossHp / 2;
    //    targetPosition = GameObject.FindWithTag("Main_gangrim"); //Player 태그를 가진 오브젝트를 찾음
    //    targettransform = GameObject.FindWithTag("Main_gangrim").transform;
    //    animator = GetComponent<Animator>();
    //    animator.SetBool("isIdle", true);
    //}

    //void Update()
    //{
    //    patterntimer += Time.deltaTime;

    //    //몬스터 이동
    //    monsterMove();

    //    //돌진 공격 조건 //update 동안 계속 실행되어야됨(돌진이라)
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

    //    if (!have_targeting) //돌 내려찍기 공격
    //    {
    //        targettrans_every = targettransform.position;
    //        targettrans_every.y = MonsterYPosition;
    //        have_targeting = true;
    //    }

    //    //***********************************
    //    //보스 랜덤 패턴 시작

    //    if (!onetime)
    //    {
    //        StopAllCoroutines();
    //        animator.SetBool("isJumpAttack", false); //...
    //        random = UnityEngine.Random.Range(0, 3); // 0 1 2
    //        BossAttack(random);
    //        DontMove = true;
    //        StartCoroutine("Move_Time"); //$ 1 2 패턴에 필요
    //        animator.SetBool("isWalk", false);
    //        onetime = true;
    //    }
    //    else
    //    {
    //        have_targeting = false;
    //    }

    //    //****************************************************
    //    //보스 2페이지 시작  // 보스 체력 절반이하
    //    if (BossHp < BossPattern2HP)
    //    {
    //        Boss_2Page_Pattern = true;
    //    }

    //    //보스 사망
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

    //    //일단 레이케스트 쏴서 플레이어랑 레이 닿으면 근접공격 아니면 보스공격패턴
    //    Vector3 spawn = targettransform.position;
    //    spawn.y = MonsterYPosition; //몬스터 바닥에 안박히는 자리 

    //    Vector3 rayspawn = transform.position;
    //    rayspawn.y = MonsterYPosition; //레이 바닥에서 부터 쏘게만듬

    //    //현재는 플레이어를 쫒다가 레이케스트에 닿으면 할퀴기 공격 시작
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
    //        //플레이어 데미지
    //        //할퀴기 공격
    //        //플레이어가 서 있는 좌표에서 부채꼴 범위(데미지 오브젝트 키고 끄는걸로)로 2회 공격한다.
    //        //플레이어가 범위 안에 있을 시 사용

    //        if (attacktime > 2f && attacktime < 5f)
    //        {
    //            if (!FirstNearAttack) //이 조건문으로 몬스터 공격 1회씩
    //            {
    //                time = 0;
    //                StartCoroutine(BackIdle(2));

    //                //animator.SetBool("isAttack", true);
    //                FirstNearAttack = true;
    //                //데미지 주는 오브젝트 껐다켜기
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
    //        //이 공격을 n초 동안 못하면
    //        if (time > BossPatternTime)
    //        {
    //            //animator.SetBool("isAttack", false);
    //            onetime = false;//조절필요 //보스패턴으로 넘어감
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

    //    //0 돌진공격 1 바닥 내려찍기 2 바위던지기
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


    //void rush() //돌진 공격 dorush = false; //0번째 패턴에 사용
    //{

    //    if (!rushfirst)  //처음 플레이어 위치 잡아줌
    //    {
    //        Vector3 spawn0 = targettransform.position;
    //        spawn0.y = MonsterYPosition; //몬스터 바닥에 안박히는 자리
    //        transform.LookAt(spawn0);
    //        animator.SetBool("isChargeattack_Set", true);
    //        Rush_Target_Vector3 = transform.position; //값 안주면 0.0.0로 이동해서 이거 줌.. //시작위치
    //                                                  // Rush_Target_Vector3 = targettransform.position;
    //        rushfirst = true;
    //    }

    //    t += Time.deltaTime;
    //    rush_t += Time.deltaTime;

    //    //캐릭터 보면서 대쉬
    //    Vector3 spawn1 = targettransform.position;
    //    spawn1.y = MonsterYPosition; //몬스터 바닥에 안박히는 자리
    //                                 // transform.LookAt(spawn1);


    //    if (!isrush) //isrush = false 시 랜덤값 변경
    //    {
    //        if (!Boss_2Page_Pattern)
    //        {
    //            MaxRushCount = UnityEngine.Random.Range(1, 3); // 1,2 중 랜덤
    //                                                           //Debug.Log("MaxRushCount: " + MaxRushCount);
    //            isrush = true;

    //        }
    //        else //보스 2페이지 //돌진 횟수 증가
    //        {
    //            MaxRushCount = UnityEngine.Random.Range(2, 4); // 2,3 중 랜덤
    //                                                           //Debug.Log("MaxRushCount: " + MaxRushCount);
    //            isrush = true;
    //        }
    //    }

    //    if (!dorush) //돌진 코드
    //    {

    //        if (i <= MaxRushCount) //연속 돌진횟수
    //        {
    //            //계속 돌아감
    //            //animator.SetBool("isChargeattack_Set", true);
    //            Vector3 spawn = targettransform.position;
    //            spawn.y = MonsterYPosition; //몬스터 바닥에 안박히는 자리


    //            if (rush_t > 2f) //길이 (머무는 시간 및 가는 길이)
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

    //                Rush_Target_Vector3.z = spawn.z + 5 * n; //돌진 ++
    //                i++;
    //                rush_t = 0;
    //            }

    //            transform.position = Vector3.MoveTowards(gameObject.transform.position, Rush_Target_Vector3, 30f * Time.deltaTime);//3번째 값 돌진 속도
    //                                                                                                                               //animator.SetBool("isChargeattack_Go", true);
    //        }
    //        else //대쉬 종료
    //        {
    //            animator.SetBool("isChargeattack_Set", false);
    //            i = 0;
    //            isrush = false; //
    //            dorush = true; //false면 다시 돌진 가능

    //            StopCoroutine(BackIdle(0));
    //            animator.SetBool("isChargeattack_Go", false);
    //            pattern1_rush = false;
    //        }

    //    }

    //}

    //void FloorDown() //바닥 내려찍기 //1번째 패턴
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
    //        //나중에 배열로 정리
    //        GameObject Skill1 = Instantiate(Boss_Skill_1, spawn, transform.rotation);
    //        GameObject Skill2 = Instantiate(Boss_Skill_1, spawn1, transform.rotation);
    //        GameObject Skill3 = Instantiate(Boss_Skill_1, spawn2, transform.rotation);
    //        GameObject Skill4 = Instantiate(Boss_Skill_1, spawn3, transform.rotation);
    //        GameObject Skill5 = Instantiate(Boss_Skill_1, spawn4, transform.rotation);
    //        skill1_ = true;
    //    }

    //}


    //bool skill2_;

    //void ThrowRock() //바위 던지기
    //{
    //    animator.SetBool("isWalk", false);
    //    Vector3 spawn = targettransform.position;
    //    spawn.y = MonsterYPosition;

    //    if (!skill2_)
    //    {
    //        transform.LookAt(spawn); //그 방향 보고 던짐 + 방향 고정
    //        skill2_ = true;
    //        StartCoroutine("Rook_Time");
    //        Rigidbody ThrowRockrigid = Instantiate(Boss_Skill_2, transform.position, transform.rotation);
    //        ThrowRockrigid.velocity = transform.forward * 20;//  * Time.deltaTime;

    //    }
    //}

    //private void OnTriggerEnter(Collider other) //Trigger에 닿음
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

    //private void OnCollisionEnter(Collision collision) //Collier에 닿음 //공격 관련
    //{
    //    if (collision.gameObject.tag == "Main_gangrim")
    //    {
    //        Debug.Log("Collision to Player");

    //    }
    //}

    //void BossDamage() //플레이어 쪽에서 호출 //호출 시 체력 1씩 깍고 체력 0일시 BossMonsterDie
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
    //    else if (i == 1)//패턴 1 애니메이션
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


