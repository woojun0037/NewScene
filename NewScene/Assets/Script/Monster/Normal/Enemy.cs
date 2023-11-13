using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected PlayerSkill playerSkill;
    [SerializeField] protected float damageToGive;
    [SerializeField] private string Hit1;
    [SerializeField] private string Hit2;

    public int CriticalCount;
    protected bool isBoss;
    public float maxHearth;
    public float curHearth;
    public float afterCurHearth;
    public float MaxDistance;
    public float chasespeed;

    public Image hpImage;
    public Canvas canvas;
    
    public bool DebuffCheck;
    public bool StartAttack;
    public bool getTouch;
    public bool waiting;
    
    public float KnockBackForce;
    protected float KnockBakcTime;

    protected Camera camera;
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
        CanvasMove();
    }

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollier = GetComponent<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        camera = GetComponent<Camera>();

        canvas.worldCamera = camera;
        agent.enabled = false;
        hpImage.fillAmount = curHearth / maxHearth;
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

    protected virtual void CanvasMove()
    {
        if (camera == null) camera = FindObjectOfType<CameraMovemant>().transform.GetChild(0).transform.GetComponent<Camera>();
        canvas.transform.LookAt(canvas.transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
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
     
            if (player.HitState != hitNum)
            {
                player.enemy = this;
                property = player.GetComponent<PropertySkill>();

                hitNum = player.HitState;
                delay = 0.0f;

                if (player.isAttack)
                {
                    HitScript hit;
                    hit = other.GetComponent<HitScript>();

                    curHearth -= hit.damage;

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

                    if (!playerSkill.DarkSkillUse)
                    {
                        if (Gauge.sGauge < 30)
                            Gauge.sGauge += hit.HitGauge;
                    }
                    afterCurHearth = curHearth;

                    hpImage.fillAmount = curHearth / maxHearth;

                    GetDamagedAnimation();
                    HitSound();
                }
            }
        }

        if(other.tag == "skill")
        {
            float _damage = other.GetComponent<SkillHit>().damage;
            curHearth -= _damage;
            hpImage.fillAmount = curHearth / maxHearth;

            if (isBoss) BossHit();
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

            if (isBoss) BossHit();
        }
    }

    public void Ciritical()
    {
        float criticalResult = 0;

        if (criticalResult < 0.05f)
        {
            criticalResult = 0.05f;
        }

        int RnadAccuracy = 100;
        float RandHitRange = criticalResult * RnadAccuracy;
        int Rand = UnityEngine.Random.Range(1, RnadAccuracy+1);

        if(Rand <= RandHitRange)
        {
            HitStop();
        }
    }

    public void HitStop()
    {
        if (waiting)
            return;
        StartCoroutine(WaitCor());
    }

    IEnumerator WaitCor()
    {
        float time = 0;
        Time.timeScale = 0.0f;
        waiting = false;
        while(time < 1 && Time.timeScale < 1f)
        {
            Time.timeScale += Time.deltaTime;
           
            yield return null;
        }
        Time.timeScale = 1.0f;
        waiting = true;
    }

    public IEnumerator DotCheck(float damage = 0)
    {
        float time = 0;
        while(time < 3)
        {
            curHearth -= damage;
            hpImage.fillAmount = curHearth / maxHearth;
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


    protected virtual void BossStart()
    {

    }

    protected virtual void BossHit()
    {

    }
}
