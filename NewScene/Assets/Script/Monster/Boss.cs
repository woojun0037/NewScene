using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float damageToGive = 1;

    public float maxHearth;
    public float curHearth;
    public float MaxDistance;
    public float chasespeed;

    public float KnockBackForce;
    private float KnockBakcTime;

    Rigidbody rigid;
    BoxCollider boxCollier;

    private GameObject damageEffect;
    protected PropertySkill property;
    protected Main_Player player;

    public Transform targetTransform;

    protected int hitNum;
    protected float delay;
    public bool isDie;

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollier = GetComponent<BoxCollider>();
    }

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Main_gangrim").GetComponent<Main_Player>();

        targetTransform = GameObject.FindWithTag("Main_gangrim").transform;
    }


    protected virtual void DieMonster()
    {
        if (curHearth < 1)
        {
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

    protected virtual void GetDamagedAnimation() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon" && !isDie)
        {
            player = other.GetComponent<HitScript>().Player;

            if (player.HitState != hitNum)
            {
                //player.boss = this;
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

                    Gauge.sGauge += hit.damage;
                    curHearth -= hit.damage;
                    GetDamagedAnimation();
                }
            }
        }

        if (other.tag == "skill")
        {
            HitScript hit;
            hit = other.GetComponent<HitScript>();
            while (hit != null)
                curHearth -= hit.damage;
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
