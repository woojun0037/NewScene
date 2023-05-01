using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwaner_Near_M : MonoBehaviour //스폰 몬스턴는 죽을때 수치 다시 다 초기화 해줘야함
{
    RaycastHit hit;

    [SerializeField] GameObject targetPosition;
    [SerializeField] Transform targetTransform;

    
    public GameObject MonsterObject; //몬스터 부모 받아옴
    public float chasespeed;  // chase speed; 나중에 Update말고 FixedUpdate일때 수치 다시 변경
    public float movetime; //높이면 endposition자리에 남은 시간만큼 있다 다시 startpositon (높이면 자연스러움)
    public float MaxDistance; //공격 길이
    public float AttackSpeed; //공격 속력

    private bool getposition;
    private bool StartAttack;
    private bool AttackSuccess;

    //몬스터 굴
    public bool patternMonsterMove;//몬스터 왔다갔다 관련 bool

    [SerializeField]
    bool NearMonsterDie = false;
    public float monsterhp;

    [SerializeField]
    float monsterfirsthp;
    public bool spawnermonster;

    [SerializeField]
    private Monster_ObjectPool MonsterOjectPool = null;
    public bool isdead;

    private void Awake()
    {
        //_my_script = GameObject.Find("Script").GetComponent<Oject_pool>();
        MonsterOjectPool = GameObject.FindGameObjectWithTag("Monster_Spawn").GetComponent<Monster_ObjectPool>();

    }

    void Start()
    {
        monsterfirsthp = monsterhp; //몬스터 첫체력 .. 리셋용도
        if (MonsterObject == null)
        {

        }
        targetPosition = GameObject.FindWithTag("Main_gangrim"); //Player 태그를 가진 오브젝트를 찾음
        targetTransform = GameObject.FindWithTag("Main_gangrim").transform;
        //targetTransform = GameObject.FindObjectOfType<PlayerMove>().transform; //PlayerMove 스크립트가 들어가 오브
    }
    void FixedUpdate()
    {
        if (NearMonsterDie != true)
        {
            monsterMove();
            monsterattack();
        }
        else //몬스터 사망
        {
            monsterhp = monsterfirsthp;

            NearMonsterDie = false; //이거 안하면 위에꺼 안받아서 안움직임
        }
    }
    private void Destory()
    {
        // ObjectPool.ReturnObject(MonsterObject);
        NearMonsterDie = false;
    }

    void monsterattack()
    {
        if (StartAttack)
        {
            //Debug.Log("monster start attack");

            if (!AttackSuccess) //근거리 전용
            {
                AttackSuccess = true;
                StartCoroutine("attacktime"); //공속
                                              //피격
                Debug.Log("attack success");
                //플레이어 체력관련 기획 자세히 나오면 sendMessage로 플레이어의 체력을 깍음
            }
        }
    }

    void monsterMove() //스폰된 몬스터는 플레이어를 계속 쫒음
    {

        //현재는 플레이어를 쫒다가 레이케스트에 닿으면 공격 시작으로 설정되어있습니다.
        Debug.DrawRay(transform.position, transform.forward * MaxDistance, Color.blue, 0.01f);
        //변경
        if (Physics.Raycast(transform.position, transform.forward, out hit, MaxDistance))
        {
            if (hit.collider.tag == "Main_gangrim")
            { //이 코드 있어야 몬스터끼리 부딪혔을 때 안멈춤
                StartAttack = true;
            }
        }
        else
        {
            StartAttack = false;
        }

        if (StartAttack) //start attack
        {
            transform.LookAt(targetTransform); //LookAt은 player가 y가면 회전해서 나중에 변경
                                               //몬스터가 움직이지 않음 not move 나중에 패턴에 따라 변경(돌진 등)
        }
        else //player chase
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition.transform.position, chasespeed);
            transform.LookAt(targetTransform);
        }

    }

    void MonsterDie()
    {
        monsterhp = monsterfirsthp;
        //죽으면 다시 풀에 넘겨줌
        MonsterOjectPool.ReturnPool(MonsterObject.gameObject);

        NearMonsterDie = true;
    }

    void MonsterDamage() //플레이어 스크립트에서 불러옴 - SendMessage 사용
    {

        if (monsterhp > 1)
        {
            monsterhp--;
        }
        else
        {
            Debug.Log("monsterhp : " + monsterhp);
            MonsterDie();
        }
        Debug.Log(monsterhp);
    }

    IEnumerator Movepositon()
    {
        yield return new WaitForSeconds(movetime);
        getposition = false;
    }

    IEnumerator attacktime()
    {
        yield return new WaitForSeconds(AttackSpeed);
        AttackSuccess = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Main_gangrim")
        {
            Debug.Log("Collision to Player");
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Main_gangrim")
    //    {

    //    }
    //}

}