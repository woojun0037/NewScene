using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster_Tiger : MonoBehaviour
{
    public GameObject MonsterObject; //���� �θ� �޾ƿ�
    public GameObject targetPosition; //playerposition
    public Transform targettransform;
    private Animator animator;

    public float BossPatternTime; //���� ���� �� Ÿ�� ���� 6��

    Vector3 targettrans_every; //Ÿ�� ����
    private Vector3 Rush_Target_Vector3; //���� �� �÷��̾� ��ġ ����

    public float chasespeed; //���� �̵��ӵ�

    public GameObject Boss_Skill_1;
    public Rigidbody Boss_Skill_2; //���� ��ų ��ȯ

    [SerializeField]  //Ȯ�ο� ��������
    private float random;

    //Ÿ�̸�
    float t = 0; //Ÿ�̸�
    float rush_t = 0; //Ÿ�̸�
    float time = 0; float attacktime = 0;
    int i = 0;
    int n = 1;
    int e = 0;
    float timer = 0;
    bool skill1_ = false;

    public float MonsterYPosition = 2f; //y�� ���ִϱ� ���� ����;; //y �� ������ ȸ��X
    float MaxRushCount;     //�ִ� ���� ���� 2~3 ���� ����
    float RushCount;

    [SerializeField]
    float BossHp; //���� ���� ü��
    float BossPattern2HP; //���� ���� ���� ü��


    bool BossMonsterDie = false; //������ ����Ͽ��°�
    bool TargetHere; //�÷��̾ ���� ������ ����(Trigger)�ȿ� ���� //�ȵ����� ���� ����
    bool StartAttack;

    //���� ��������
    bool isrush;//�ִ뵹��Ƚ�� ������ �����
    bool rushfirst;
    bool pattern1_rush;
    bool dorush;//���� �ð�

    [SerializeField] //Ȯ�ο� üũ�� ���� 2������
    bool Boss_2Page_Pattern = false; // true�� 2 ������ ����
    bool have_targeting = false;

    public float MaxDistance = 5f; //Ray ����
    RaycastHit hit;

    bool FirstNearAttack;
    bool onetime = true;
    bool DontMove;
    bool ispattern2;


    public enum BossPattern //���� ��������
    {
        //0 �������� 1 �ٴ� ������� 2 ���������� 3 ȭ���(����)
        Pattern_0 = 0,
        Pattern_1,
        Pattern_2
    }
    public BossPattern bosspattern;

    void Start() //�ʱ�ȭ
    {
        onetime = true;
        BossPattern2HP = BossHp / 2;
        targetPosition = GameObject.FindWithTag("Main_gangrim"); //Player �±׸� ���� ������Ʈ�� ã��
        targettransform = GameObject.FindWithTag("Main_gangrim").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //���� �̵�
        monsterMove();

        //���� ���� ���� //update ���� ��� ����Ǿ�ߵ�(�����̶�)
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

        if (!have_targeting) //�� ������� ����
        {
            targettrans_every = targettransform.position;
            targettrans_every.y = 0f;
            have_targeting = true;
        }

        //***********************************
        //���� ���� ���� ����

        if (!onetime)
        {
            random = UnityEngine.Random.Range(0, 3); // 0 1 2
            BossAttack(random);
            DontMove = true;
            StartCoroutine("Move_Time"); //$ 1 2 ���Ͽ� �ʿ�
            animator.SetBool("isAttack", false);
            onetime = true;
        }
        else
        {
            have_targeting = false;
        }

        //****************************************************
        //���� 2������ ����  // ���� ü�� ��������
        if (BossHp < BossPattern2HP)
        {
            Boss_2Page_Pattern = true;
        }

        //���� ���
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

        //�ϴ� �����ɽ�Ʈ ���� �÷��̾�� ���� ������ �������� �ƴϸ� ������������
        Vector3 spawn = targettransform.position;
        spawn.y = MonsterYPosition; //���� �ٴڿ� �ȹ����� �ڸ� 

        Vector3 rayspawn = transform.position;
        rayspawn.y = 0f; //���� �ٴڿ��� ���� ��Ը���

        //����� �÷��̾ �i�ٰ� �����ɽ�Ʈ�� ������ ������ ���� ����
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
            //�÷��̾� ������
            //������ ����
            //�÷��̾ �� �ִ� ��ǥ���� ��ä�� ����(������ ������Ʈ Ű�� ���°ɷ�)�� 2ȸ �����Ѵ�.
            //�÷��̾ ���� �ȿ� ���� �� ���

            if (attacktime > 2f && attacktime < 5f)
            {
                if (!FirstNearAttack) //�� ���ǹ����� ���� ���� 1ȸ��
                {
                    animator.SetBool("isAttack", true);
                    FirstNearAttack = true;
                    //������ �ִ� ������Ʈ �����ѱ�
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
            //�� ������ n�� ���� ���ϸ�
            if (time > BossPatternTime)
            {
                onetime = false;//�����ʿ� //������������ �Ѿ
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

        //0 �������� 1 �ٴ� ������� 2 ���������� 3 ȭ���(����)
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


    void rush() //���� ���� dorush = false; //0��° ���Ͽ� ���
    {
        if (!rushfirst)  //ó�� �÷��̾� ��ġ �����
        {
            Rush_Target_Vector3 = transform.position; //�� ���ָ� 0.0.0�� �̵��ؼ� �̰� ��.. //������ġ
            // Rush_Target_Vector3 = targettransform.position;
            rushfirst = true;
        }

        t += Time.deltaTime;
        rush_t += Time.deltaTime;

        //ĳ���� ���鼭 �뽬
        Vector3 spawn1 = targettransform.position;
        spawn1.y = MonsterYPosition; //���� �ٴڿ� �ȹ����� �ڸ�
        transform.LookAt(spawn1);

        if (!isrush) //isrush = false �� ������ ����
        {
            if (!Boss_2Page_Pattern)
            {
                MaxRushCount = UnityEngine.Random.Range(1, 3); // 1,2 �� ����
                //Debug.Log("MaxRushCount: " + MaxRushCount);
                isrush = true;

            }
            else //���� 2������ //���� Ƚ�� ����
            {
                MaxRushCount = UnityEngine.Random.Range(2, 4); // 2,3 �� ����
                //Debug.Log("MaxRushCount: " + MaxRushCount);
                isrush = true;
            }
        }

        if (!dorush) //���� �ڵ�
        {

            if (i <= MaxRushCount) //���� ����Ƚ��
            {
                Vector3 spawn = targettransform.position;
                spawn.y = MonsterYPosition; //���� �ٴڿ� �ȹ����� �ڸ�


                if (rush_t > 2f) //���� (�ӹ��� �ð� �� ���� ����)
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

                    Rush_Target_Vector3.z = spawn.z + 5 * n; //���� ++
                    i++;
                    rush_t = 0;
                }

                transform.position = Vector3.MoveTowards(gameObject.transform.position, Rush_Target_Vector3, 30f * Time.deltaTime);//3��° �� ���� �ӵ�

            }
            else
            {
                i = 0;
                isrush = false; //
                dorush = true; //false�� �ٽ� ���� ����
                pattern1_rush = false;
            }

        }

    }

    void FloorDown() //�ٴ� ������� //1��° ����
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
            //���߿� �迭�� ����
            GameObject Skill1 = Instantiate(Boss_Skill_1, spawn, transform.rotation);
            GameObject Skill2 = Instantiate(Boss_Skill_1, spawn1, transform.rotation);
            GameObject Skill3 = Instantiate(Boss_Skill_1, spawn2, transform.rotation);
            GameObject Skill4 = Instantiate(Boss_Skill_1, spawn3, transform.rotation);
            GameObject Skill5 = Instantiate(Boss_Skill_1, spawn4, transform.rotation);
            skill1_ = true;
        }

    }


    bool skill2_;

    void ThrowRock() //���� ������
    {
        Vector3 spawn = transform.position;
        spawn.y = 1f;

        if (!skill2_)
        {
            transform.LookAt(targettransform); //�� ���� ���� ���� + ���� ����
            skill2_ = true;
            StartCoroutine("Rook_Time");
            Rigidbody ThrowRockrigid = Instantiate(Boss_Skill_2, transform.position, transform.rotation);
            ThrowRockrigid.velocity = transform.forward * 20;//  * Time.deltaTime;

        }
    }

    private void OnTriggerEnter(Collider other) //Trigger�� ����
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

    private void OnCollisionEnter(Collision collision) //Collier�� ���� //���� ����
    {
        if (collision.gameObject.tag == "Main_gangrim")
        {
            Debug.Log("Collision to Player");

        }
    }

    void BossDamage() //�÷��̾� �ʿ��� ȣ�� //ȣ�� �� ü�� 1�� ��� ü�� 0�Ͻ� BossMonsterDie
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