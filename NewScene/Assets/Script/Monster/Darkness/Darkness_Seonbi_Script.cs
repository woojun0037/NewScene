using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Darkness_Seonbi_Script : Seonbi_Script
{
    protected override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    protected override void Start()
    {
        base.Start();
        maxHearth = maxHearth * 10;
        curHearth = curHearth * 10;
    }
    void Update()
    {
        monsterMove();
        NotDamaged();
        DieMonster();
        CanvasMove();
    }

    protected override void CanvasMove()
    {

        base.CanvasMove();
    }
}
