using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected PlayerSkill playerSkill;
    [SerializeField] protected float damageToGive;
    [SerializeField] private string Hit1;
    [SerializeField] private string Hit2;

    public int CriticalCount;

    public float maxHearth;
    public float curHearth;
    public float MaxDistance;
    public float chasespeed;
   
    public bool DebuffCheck;
    public bool StartAttack;
    public bool getTouch;
    public bool waiting;

    public float KnockBackForce;
    protected float KnockBakcTime;

    protected Main_Player Player;
    protected Rigidbody rigid;
    protected BoxCollider boxCollier;

    protected GameObject damageEffect;
    protected PropertySkill property;
    protected Main_Player player;
    protected AudioSource audioSource;
    public NavMeshAgent agent = null;
    public Transform targetTransform;
    public GameObject skillHitEffect;

    protected int hitNum;
    protected float delay;
    protected bool havedelay;

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
        audioSource = GetComponent<AudioSource>();
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

    protected virtual void HitSound()
    {
        SoundManager.instance.MonsterSE(SoundManager.instance.effectSounds[3].clip, audioSource);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            player = other.GetComponent<HitScript>().Player;
            playerSkill = other.transform.parent.GetComponent<PlayerSkill>();
            HitSound();
            
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
            float _damage = other.GetComponent<SkillHit>().damage;
            curHearth -= _damage;

            GameObject hitEffect = Instantiate(skillHitEffect, transform.position, Quaternion.identity);
            hitEffect.transform.position = this.transform.position;
            hitEffect.SetActive(true);
        }

        if(other.tag == "dotSkill")
        {
            float _damage = other.GetComponent<SkillHit>().damage;
            StartCoroutine(DotCheck(_damage));
            GameObject hitEffect = Instantiate(skillHitEffect, transform.position, Quaternion.identity);
            hitEffect.transform.position = this.transform.position;
            hitEffect.SetActive(true);
        }
    }


    public void HitStop(float duration)
    {
        if (waiting)
            return;
        Time.timeScale = 0.0f;
        StartCoroutine(WaitCor(duration));
    }

    IEnumerator WaitCor(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        waiting = false;
    }
    private IEnumerator DotCheck(float damage = 0)
    {
        float time = 0;
        while(time < 3)
        {
            curHearth -= damage;
            yield return new WaitForSeconds(1f);
            time++;
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
}
