using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    protected bool isDie = false;
    public bool isBossStart;
    public DOTweenAnimation bossHP_Dot;

    protected override void BossStart()
    {
        bossHP_Dot.DORestartById("start");
        //transform.DOLocalMove(Vector2.zero, 1f).OnComplete(() =>
        //{
            isBossStart = true;
        //});
    }

    protected override void BossHit()
    {
        bossHP_Dot.DORestartById("hit");
    }

    protected override void CanvasMove()
    {
        
    }

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
