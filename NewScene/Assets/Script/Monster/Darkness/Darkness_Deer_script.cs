using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness_Deer_script : Deer_Script
{

    // Start is called before the first frame update
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

        if (isdash)
        {
            rosh();
        }
    }

}
