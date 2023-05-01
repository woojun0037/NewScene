using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwaner_Near_M : MonoBehaviour //���� ���ϴ� ������ ��ġ �ٽ� �� �ʱ�ȭ �������
{
    RaycastHit hit;

    [SerializeField] GameObject targetPosition;
    [SerializeField] Transform targetTransform;

    
    public GameObject MonsterObject; //���� �θ� �޾ƿ�
    public float chasespeed;  // chase speed; ���߿� Update���� FixedUpdate�϶� ��ġ �ٽ� ����
    public float movetime; //���̸� endposition�ڸ��� ���� �ð���ŭ �ִ� �ٽ� startpositon (���̸� �ڿ�������)
    public float MaxDistance; //���� ����
    public float AttackSpeed; //���� �ӷ�

    private bool getposition;
    private bool StartAttack;
    private bool AttackSuccess;

    //���� ��
    public bool patternMonsterMove;//���� �Դٰ��� ���� bool

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
        monsterfirsthp = monsterhp; //���� ùü�� .. ���¿뵵
        if (MonsterObject == null)
        {

        }
        targetPosition = GameObject.FindWithTag("Main_gangrim"); //Player �±׸� ���� ������Ʈ�� ã��
        targetTransform = GameObject.FindWithTag("Main_gangrim").transform;
        //targetTransform = GameObject.FindObjectOfType<PlayerMove>().transform; //PlayerMove ��ũ��Ʈ�� �� ����
    }
    void FixedUpdate()
    {
        if (NearMonsterDie != true)
        {
            monsterMove();
            monsterattack();
        }
        else //���� ���
        {
            monsterhp = monsterfirsthp;

            NearMonsterDie = false; //�̰� ���ϸ� ������ �ȹ޾Ƽ� �ȿ�����
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

            if (!AttackSuccess) //�ٰŸ� ����
            {
                AttackSuccess = true;
                StartCoroutine("attacktime"); //����
                                              //�ǰ�
                Debug.Log("attack success");
                //�÷��̾� ü�°��� ��ȹ �ڼ��� ������ sendMessage�� �÷��̾��� ü���� ����
            }
        }
    }

    void monsterMove() //������ ���ʹ� �÷��̾ ��� �i��
    {

        //����� �÷��̾ �i�ٰ� �����ɽ�Ʈ�� ������ ���� �������� �����Ǿ��ֽ��ϴ�.
        Debug.DrawRay(transform.position, transform.forward * MaxDistance, Color.blue, 0.01f);
        //����
        if (Physics.Raycast(transform.position, transform.forward, out hit, MaxDistance))
        {
            if (hit.collider.tag == "Main_gangrim")
            { //�� �ڵ� �־�� ���ͳ��� �ε����� �� �ȸ���
                StartAttack = true;
            }
        }
        else
        {
            StartAttack = false;
        }

        if (StartAttack) //start attack
        {
            transform.LookAt(targetTransform); //LookAt�� player�� y���� ȸ���ؼ� ���߿� ����
                                               //���Ͱ� �������� ���� not move ���߿� ���Ͽ� ���� ����(���� ��)
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
        //������ �ٽ� Ǯ�� �Ѱ���
        MonsterOjectPool.ReturnPool(MonsterObject.gameObject);

        NearMonsterDie = true;
    }

    void MonsterDamage() //�÷��̾� ��ũ��Ʈ���� �ҷ��� - SendMessage ���
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