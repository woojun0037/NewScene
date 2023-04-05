using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHearth;
    public float curHearth;
  
    public int damageToGive = 1;

    public float MaxDistance;
    public float chasespeed;
    public float BackSpeed = -10;

    public float moveSpeed;

    public float KnockBackForce;
    public float KnockBakcTime;

    bool StartAttack;
    bool isHit;

    RaycastHit hit;

    Rigidbody rigid;
    BoxCollider boxCollier;
    Main_Player player;

    private GameObject damageEffect;

    [SerializeField] GameObject targetPosition;
    [SerializeField] Transform targetTransform;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollier = GetComponent<BoxCollider>();

    }

    void Start()
    {
        targetPosition = GameObject.FindWithTag("Main_gangrim"); //Player 태그를 가진 오브젝트를 찾음
        targetTransform = GameObject.FindWithTag("Main_gangrim").transform;
    }

    void Update()
    {
        DieMonster();
        monsterMove();
    }

    void monsterMove() //스폰된 몬스터는 플레이어를 계속 쫒음
    {

        //현재는 플레이어를 쫒다가 레이케스트에 닿으면 공격 시작으로 설정되어있습니다.
        Debug.DrawRay(transform.position, transform.forward * MaxDistance, Color.blue, 0.01f);
        //변경
        if (Physics.Raycast(transform.position, transform.forward, out hit, MaxDistance))
        {
            if (hit.collider.tag == "Main_gangrim")
            {
                StartAttack = true;
            }
        }
        else
        {
            StartAttack = false;
        }

        if(targetTransform == null)
        {
            return;
        }

        if (StartAttack) //start attack
        {
            transform.LookAt(targetTransform); //LookAt은 player가 y가면 회전해서 나중에 변경
                                               //몬스터가 움직이지 않음 not move 나중에 패턴에 따라 변경(돌진 등)
        }
        else //player chase
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition.transform.position, chasespeed * Time.deltaTime);
            transform.LookAt(targetTransform);
        }

    }

    void DieMonster()
    {
        if (curHearth < 1)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            
            Main_Player player = other.GetComponent<HitScript>().Player;
            if(player.isAttack)
            {
                if (damageEffect == null)
                    damageEffect = Instantiate(player.AtkEffect[3], transform.position, Quaternion.identity);
                else
                    damageEffect.transform.position = transform.position;
                damageEffect.SetActive(false);
                damageEffect.SetActive(true);

                isHit = true;
                Vector3 reactVec = transform.position - other.transform.position;
                reactVec = reactVec.normalized;
                reactVec += Vector3.back;
                rigid.AddForce(reactVec * KnockBackForce, ForceMode.Impulse);

                HitScript hit;
                hit = other.GetComponent<HitScript>();

                Gauge.sGauge += hit.damage;
                curHearth -= hit.damage;

                Debug.Log("Weapon: " + curHearth);
            }
        }

        if (other.gameObject.tag == "Main_gangrim")
        {
            FindObjectOfType<HealthManager>().HurtPlayer(damageToGive);
            Debug.Log("hit to Player");
        }
    }

}
