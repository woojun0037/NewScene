using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Asura : Boss
{
    private Animator anim;

    protected override void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected override void Start()
    {
        player = GameObject.FindWithTag("Main_gangrim").GetComponent<Main_Player>();
        BossStart();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
