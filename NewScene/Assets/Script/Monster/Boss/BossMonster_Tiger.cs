using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster_Tiger : MonoBehaviour
{
    public GameObject MonsterObject; //몬스터 부모 받아옴
    public GameObject targetPosition; //playerposition
    public Transform targettransform;
    private Animator animator;

    public float BossPatternTime; //보스 패턴 쿨 타임 보통 6초

    Vector3 targettrans_every; //타켓 고정
    private Vector3 Rush_Target_Vector3; //돌진 중 플레이어 위치 받음

    public float chasespeed; //몬스터 이동속도

    public GameObject Boss_Skill_1;
    public Rigidbody Boss_Skill_2; //보스 스킬 소환

    [SerializeField]  //확인용 지워도됨
    private float random;

    //타이머
    float t = 0; //타이머
    float rush_t = 0; //타이머
    float time = 0; float attacktime = 0;
    int i = 0;
    int n = 1;
    int e = 0;
    float timer = 0;
    bool skill1_ = false;

    public float MonsterYPosition = 2f; //y값 안주니까 땅에 박힘;; //y 값 고정용 회전X
    float MaxRushCount;     //최대 돌진 갯수 2~3 랜덤 받음
    float RushCount;

    [SerializeField]
    float BossHp; //보스 기초 체력
    float BossPattern2HP; //보스 다음 패턴 체력


    bool BossMonsterDie = false; //보스가 사망하였는가
    bool TargetHere; //플레이어가 보스 몬스터의 범위(Trigger)안에 들어옴 //안들어오면 패턴 공격
    bool StartAttack;

    //보스 돌진관련
    bool isrush;//최대돌진횟수 랜덤값 변경용
    bool rushfirst;
    bool pattern1_rush;
    bool dorush;//돌진 시간

    [SerializeField] //확인용 체크시 보스 2페이지
    bool Boss_2Page_Pattern = false; // true시 2 페이지 시작
    bool have_targeting = false;

    public float MaxDistance = 5f; //Ray 길이
    RaycastHit hit;

    bool FirstNearAttack;
    bool onetime = true;
    bool DontMove;
    bool ispattern2;


    public enum BossPattern //보스 공격패턴
    {
        //0 돌진공격 1 바닥 내려찍기 2 바위던지기 3 화살비(보류)
        Pattern_0 = 0,
        Pattern_1,
        Pattern_2
    }
    public BossPattern bosspattern;

    void Start() //초기화
    {
        onetime = true;
        BossPattern2HP = BossHp / 2;
        targetPosition = GameObject.FindWithTag("Main_gangrim"); //Player 태그를 가진 오브젝트를 찾음
        targettransform = GameObject.FindWithTag("Main_gangrim").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //몬스터 이동
        monsterMove();

        //돌진 공격 조건 //update 동안 계속 실행되어야됨(돌진이라)
        if (pattern1_rush)
        {
            rush();
        }
        else
        {
            dorush = false;
            rushfirst = false;
        }

        if (ispattern2)
        {
            animator.SetBool("isWalk", false);
            timer += Time.deltaTime;

            if (e < 5)
            {
                if (timer > 1f)
                {
                    timer = 0;
                    e++;
                    ThrowRock();
                }
            }
            else
            {
                ispattern2 = false;
                e = 0;
            }
        }

        if (!have_targeting) //돌 내려찍기 공격
        {
            targettrans_every = targettransform.position;
            targettrans_every.y = 0f;
            have_targeting = true;
        }

        //***********************************
        //보스 랜덤 패턴 시작

        if (!onetime)
        {
            random = UnityEngine.Random.Range(0, 3); // 0 1 2
            BossAttack(random);
            DontMove = true;
            StartCoroutine("Move_Time"); //$ 1 2 패턴에 필요
            animator.SetBool("isAttack", false);
            onetime = true;
        }
        else
        {
            have_targeting = false;
        }

        //****************************************************
        //보스 2페이지 시작  // 보스 체력 절반이하
        if (BossHp < BossPattern2HP)
        {
            Boss_2Page_Pattern = true;
        }

        //보스 사망
        if (BossMonsterDie == true)
        {
            Debug.Log("BossMonsterDie");
            animator.SetBool("isDie", true);
            //Destroy(MonsterObject);
        }

    }

    void monsterMove()
    {
        time += Time.deltaTime;
        attacktime += Time.deltaTime;

        //일단 레이케스트 쏴서 플레이어랑 레이 닿으면 근접공격 아니면 보스공격패턴
        Vector3 spawn = targettransform.position;
        spawn.y = MonsterYPosition; //몬스터 바닥에 안박히는 자리 

        Vector3 rayspawn = transform.position;
        rayspawn.y = 0f; //레이 바닥에서 부터 쏘게만듬

        //현재는 플레이어를 쫒다가 레이케스트에 닿으면 할퀴기 공격 시작
        Debug.DrawRay(rayspawn, transform.forward * MaxDistance, Color.blue, 0.05f);

        if (Physics.Raycast(rayspawn, transform.forward, out hit, MaxDistance))
        {
            animator.SetBool("isWalk", false);
            StartAttack = true;
        }
        else
        {
            attacktime = 0;
            StartAttack = false;
        }

        if (StartAttack)
        {
            animator.SetBool("isWalk", false);
            transform.LookAt(spawn);
            //플레이어 데미지
            //할퀴기 공격
            //플레이어가 서 있는 좌표에서 부채꼴 범위(데미지 오브젝트 키고 끄는걸로)로 2회 공격한다.
            //플레이어가 범위 안에 있을 시 사용

            if (attacktime > 2f && attacktime < 5f)
            {
                if (!FirstNearAttack) //이 조건문으로 몬스터 공격 1회씩
                {
                    animator.SetBool("isAttack", true);
                    FirstNearAttack = true;
                    //데미지 주는 오브젝트 껐다켜기
                    //BossAttacker.SetActive(true);
                }

            }
            else if (attacktime > 5f)
            {
                animator.SetBool("isAttack", false);
                attacktime = 0;
                FirstNearAttack = false;
            }
        }
        else
        {
            //이 공격을 n초 동안 못하면
            if (time > BossPatternTime)
            {
                onetime = false;//조절필요 //보스패턴으로 넘어감
                time = 0;
            }

            if (!DontMove)
            {
                //player chase
                animator.SetBool("isJumpAttack", false);
                animator.SetBool("isWalk", true);
                transform.position = Vector3.MoveTowards(gameObject.transform.position, spawn, chasespeed * Time.deltaTime);
                transform.LookAt(spawn);
            }
            else
            {
                animator.SetBool("isWalk", false);
            }

        }
    }

    void BossAttack(float random)
    {

        if (random == 0)
        {
            bosspattern = BossPattern.Pattern_0;
        }
        else if (random == 1)
        {
            bosspattern = BossPattern.Pattern_1;
        }
        else if (random == 2)
        {
            bosspattern = BossPattern.Pattern_2;
        }

        //0 돌진공격 1 바닥 내려찍기 2 바위던지기 3 화살비(보류)
        switch (bosspattern)
        {
            case BossPattern.Pattern_0:

                pattern1_rush = true;
                animator.SetBool("isWalk", false);
                //Debug.Log("Boss Monster pattern0");

                break;

            case BossPattern.Pattern_1:

                FloorDown();
                animator.SetBool("isWalk", false);
                animator.SetBool("isJumpAttack", true);
                skill1_ = false;



                break;
            case BossPattern.Pattern_2:
                animator.SetBool("isWalk", false);
                ispattern2 = true;

                break;

            default:

                break;

        }

    }


    void rush() //돌진 공격 dorush = false; //0번째 패턴에 사용
    {
        if (!rushfirst)  //처음 플레이어 위치 잡아줌
        {
            Rush_Target_Vector3 = transform.position; //값 안주면 0.0.0로 이동해서 이거 줌.. //시작위치
            // Rush_Target_Vector3 = targettransform.position;
            rushfirst = true;
        }

        t += Time.deltaTime;
        rush_t += Time.deltaTime;

        //캐릭터 보면서 대쉬
        Vector3 spawn1 = targettransform.position;
        spawn1.y = MonsterYPosition; //몬스터 바닥에 안박히는 자리
        transform.LookAt(spawn1);

        if (!isrush) //isrush = false 시 랜덤값 변경
        {
            if (!Boss_2Page_Pattern)
            {
                MaxRushCount = UnityEngine.Random.Range(1, 3); // 1,2 중 랜덤
                //Debug.Log("MaxRushCount: " + MaxRushCount);
                isrush = true;

            }
            else //보스 2페이지 //돌진 횟수 증가
            {
                MaxRushCount = UnityEngine.Random.Range(2, 4); // 2,3 중 랜덤
                //Debug.Log("MaxRushCount: " + MaxRushCount);
                isrush = true;
            }
        }

        if (!dorush) //돌진 코드
        {

            if (i <= MaxRushCount) //연속 돌진횟수
            {
                Vector3 spawn = targettransform.position;
                spawn.y = MonsterYPosition; //몬스터 바닥에 안박히는 자리


                if (rush_t > 2f) //길이 (머무는 시간 및 가는 길이)
                {
                    Rush_Target_Vector3 = spawn;
                    if (transform.position.z > spawn1.z)
                    {
                        n = -1;
                    }
                    else
                    {
                        n = 1;
                    }

                    Rush_Target_Vector3.z = spawn.z + 5 * n; //돌진 ++
                    i++;
                    rush_t = 0;
                }

                transform.position = Vector3.MoveTowards(gameObject.transform.position, Rush_Target_Vector3, 30f * Time.deltaTime);//3번째 값 돌진 속도

            }
            else
            {
                i = 0;
                isrush = false; //
                dorush = true; //false면 다시 돌진 가능
                pattern1_rush = false;
            }

        }

    }

    void FloorDown() //바닥 내려찍기 //1번째 패턴
    {
        animator.SetBool("isWalk", false);

        Vector3 spawn = targettrans_every;
        spawn.z = UnityEngine.Random.Range(2, 10) + targettrans_every.z;

        Vector3 spawn1 = targettrans_every;
        spawn1.z = UnityEngine.Random.Range(-2, -10) + targettrans_every.z;

        Vector3 spawn2 = targettrans_every;
        spawn2.z = UnityEngine.Random.Range(4, 10) + targettrans_every.z;
        spawn2.x = UnityEngine.Random.Range(-4, -10) + targettrans_every.x;

        Vector3 spawn3 = targettrans_every;
        spawn3.x = UnityEngine.Random.Range(2, 10) + targettrans_every.x;

        Vector3 spawn4 = targettrans_every;
        spawn4.x = UnityEngine.Random.Range(-2, -10) + +targettrans_every.x;

        if (!skill1_)
        {
            animator.SetBool("isWalk", false);
            //나중에 배열로 정리
            GameObject Skill1 = Instantiate(Boss_Skill_1, spawn, transform.rotation);
            GameObject Skill2 = Instantiate(Boss_Skill_1, spawn1, transform.rotation);
            GameObject Skill3 = Instantiate(Boss_Skill_1, spawn2, transform.rotation);
            GameObject Skill4 = Instantiate(Boss_Skill_1, spawn3, transform.rotation);
            GameObject Skill5 = Instantiate(Boss_Skill_1, spawn4, transform.rotation);
            skill1_ = true;
        }

    }


    bool skill2_;

    void ThrowRock() //바위 던지기
    {
        Vector3 spawn = transform.position;
        spawn.y = 1f;

        if (!skill2_)
        {
            transform.LookAt(targettransform); //그 방향 보고 던짐 + 방향 고정
            skill2_ = true;
            StartCoroutine("Rook_Time");
            Rigidbody ThrowRockrigid = Instantiate(Boss_Skill_2, transform.position, transform.rotation);
            ThrowRockrigid.velocity = transform.forward * 20;//  * Time.deltaTime;

        }
    }

    private void OnTriggerEnter(Collider other) //Trigger에 닿음
    {
        if (other.gameObject.tag == "Main_gangrim")
        {
            //Debug.Log("Collision  Trigger to Player");
            TargetHere = true;
        }

        if (other.tag == "Weapon")
        {
            HitScript Hit = other.GetComponent<HitScript>();
            BossHp -= Hit.damage;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Main_gangrim")
        {
            TargetHere = false;
        }
    }

    private void OnCollisionEnter(Collision collision) //Collier에 닿음 //공격 관련
    {
        if (collision.gameObject.tag == "Main_gangrim")
        {
            Debug.Log("Collision to Player");

        }
    }

    void BossDamage() //플레이어 쪽에서 호출 //호출 시 체력 1씩 깍고 체력 0일시 BossMonsterDie
    {
        Debug.Log("MonsterDamage()");

        if (BossHp > 1)
        {
            BossHp--;
        }
        else
        {

            BossMonsterDie = true;
            //MonsterDie();
        }

    }

    IEnumerator Move_Time()
    {
        yield return new WaitForSeconds(BossPatternTime - 1f); //3f
        DontMove = false;
    }

    IEnumerator Rook_Time()
    {
        skill2_ = false;
        yield return new WaitForSeconds(1f);
    }
}