using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float damageToGive = 1;
    
    public float maxHearth;
    public float curHearth;
    public float MaxDistance;
    public float chasespeed;
   
    public bool DebuffCheck;
    public bool StartAttack;

    public float KnockBackForce;
    private float KnockBakcTime;

    Rigidbody rigid;
    BoxCollider boxCollier;

    private GameObject damageEffect;
    PropertySkill property;
    Main_Player player;

    public Transform targetTransform;
    public NavMeshAgent agent = null;
    [SerializeField] protected PlayerSkill playerSkill;
    protected Main_Player Player;

    public bool getTouch;

    int hitNum;
    float delay;
    bool havedelay;

    void Update()
    {
        DieMonster();
        monsterMove();
        NotDamaged();
    }
    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollier = GetComponent<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }

    protected virtual void Start()
    {
        Player = GameObject.FindWithTag("Main_gangrim").GetComponent<Main_Player>();
        targetTransform = GameObject.FindWithTag("Main_gangrim").transform;

        Vector3 targety = targetTransform.position;
        targety.y = transform.position.y;

        transform.LookAt(targety);

        agent.speed = chasespeed;
        agent.enabled = true;
    }

    protected virtual void monsterMove()
    {
        if(agent != null)
        {
            agent.speed = chasespeed;
            agent.destination = targetTransform.position;
            //agent.SetDestination(targetTransform.position);
        }
    }

    protected virtual void DieMonster()
    {
        if (curHearth < 1)
        {
            OnDisable();
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }

    protected void NotDamaged()
    {
        if (hitNum > 0)
        {
            delay += Time.deltaTime;
            if (delay > 0.1f)
            {
                delay = 0.0f;
                hitNum = 0;
            }
        }
    }

    public void OnDisable()
    {
        if (agent != null)
        {
            //agent.ResetPath(); 
            agent.enabled = false;
            agent = null; 
        }
    }

    protected virtual void GetDamagedAnimation(){ }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            player = other.GetComponent<HitScript>().Player;
            playerSkill = other.transform.parent.GetComponent<PlayerSkill>();

            if (player.HitState != hitNum)
            {
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
                    
                    HitScript hit;
                    hit = other.GetComponent<HitScript>();

                    if (!playerSkill.DarkSkillUse)
                    { 
                        if(Gauge.sGauge < 30)
                            Gauge.sGauge += hit.HitGauge;

                        Gauge.Test();
                    }
                    curHearth -= hit.damage;
                    GetDamagedAnimation();
                }
            }
        }

        if(other.tag == "skill")
        {

            curHearth -= 3; //test
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Main_gangrim" && !getTouch)
        {
            if (!havedelay)
            {
                havedelay = true;
                StartCoroutine("AttackDelay");
            }
        }
    }


    IEnumerator GetDebuffCor()
    {
        yield return new WaitForSeconds(5.0f);
        player.Q_skillCheck = false;
        player.R_skillCheck = false;
        property.Debuff = false;
        this.chasespeed = 3f;
    }

    IEnumerator GetStunCor()
    {
        yield return new WaitForSeconds(10.0f);
        player.E_skillCheck = false;
        player.R_skillCheck = false;
        property.Stun = false;
        this.chasespeed = 3f;
    }
    IEnumerator AttackDelay() //플레이어가 받는 공격 딜레이
    {
        Debug.Log("hit to Player");
        FindObjectOfType<HealthManager>().HurtPlayer(damageToGive);
        yield return new WaitForSeconds(1f);
        havedelay = false;
    }
}
