using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    Rigidbody rigid;
    BoxCollider boxCollier;

    private GameObject damageEffect;
    protected PropertySkill property;
    [SerializeField] protected Main_Player player;


    protected int hitNum;
    protected float delay;
    protected bool isDie = false;

    protected override void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollier = GetComponent<BoxCollider>();
    }

    protected override void Start()
    {
        player = GameObject.FindWithTag("Main_gangrim").GetComponent<Main_Player>();
        targetTransform = GameObject.FindWithTag("Main_gangrim").transform;
    }


    protected override void DieMonster()
    {
        if (curHearth < 1)
        {
            gameObject.SetActive(false);
        }
    }

}
