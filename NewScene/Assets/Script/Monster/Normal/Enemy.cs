using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int damageToGive = 1;

    public float maxHearth;
    public float curHearth;
    public float MaxDistance;
    public float chasespeed;
    public float moveSpeed;

    public bool DebuffCheck;
    public bool StartAttack;

    private float KnockBackForce = 2f;
    private float KnockBakcTime;

    RaycastHit hit;

    Rigidbody rigid;
    BoxCollider boxCollier;

    private GameObject damageEffect;
    PropertySkill property;
    Main_Player player;

    public Transform targetTransform;
    public NavMeshAgent agent = null;

    int hitNum;
    float delay;

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollier = GetComponent<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }

    protected virtual void Start()
    {
        agent.speed = chasespeed;

        targetTransform = GameObject.FindWithTag("Main_gangrim").transform;
        agent.enabled = true;
    }

    void Update()
    {
        DieMonster();
        monsterMove();
        NotDamaged();
    }


    protected virtual void monsterMove() //스폰된 몬스터는 플레이어를 계속 쫒음
    {
        agent.speed = chasespeed;
        //agent.destination = targetTransform.position;
        agent.SetDestination(targetTransform.position);

    }

    protected virtual void DieMonster()
    {
        if (curHearth < 1)
        {
            Destroy(gameObject);
        }
    }

    protected void NotDamaged()
    {
        if(hitNum > 0)
        {
            delay += Time.deltaTime;
            if(delay > 0.1f)
            {
                delay = 0.0f;
                hitNum = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            player = other.GetComponent<HitScript>().Player;
            if(player.HitState != hitNum)
            {
                //print(name);
                player.enemy = this;
                property = player.GetComponent<PropertySkill>();
                hitNum = player.HitState;
                delay = 0.0f;
                if (player.isAttack)
                {
                    if (damageEffect == null)
                    {
                        damageEffect = Instantiate(player.AtkEffect[3], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        damageEffect.transform.position = transform.position;
                    }
                    damageEffect.SetActive(false);
                    damageEffect.SetActive(true);

                    if (property.Debuff == true)
                    {
                        this.chasespeed = 1f;
                        StartCoroutine(GetDebuffCor());
                    }

                    if (property.Stun == true)
                    {
                        this.chasespeed = 0f;
                        StartCoroutine(GetStunCor());
                    }

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
        }

        if (other.gameObject.tag == "Main_gangrim")
        {
            FindObjectOfType<HealthManager>().HurtPlayer(damageToGive);
            Debug.Log("hit to Player");
        }
    }

    IEnumerator GetDebuffCor()
    {
        yield return new WaitForSeconds(5.0f);
        player.E_skillCheck = false;
        player.F_skillCheck = false;
        property.Debuff = false;
        this.chasespeed = 3f;
    }

    IEnumerator GetStunCor()
    {
        yield return new WaitForSeconds(10.0f);
        player.R_skillCheck = false;
        player.F_skillCheck = false;
        property.Stun = false;
        this.chasespeed = 3f;
    }
}
