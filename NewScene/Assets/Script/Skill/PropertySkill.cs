using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertySkill : MonoBehaviour
{

    [SerializeField] private float BuffTime = 6f;
    [SerializeField] private float DebuffTime = 5f;
    [SerializeField] private float StunTime = 3f;
    
    Main_Player SkillUse;
    Enemy enemy;

    public float SpeedUp = 1.2f;

    public bool Debuff = false;
    public bool Stun = false;

    private void Start()
    {
        SkillUse = GetComponent<Main_Player>();
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        TafoonSkill();
        IceSkill();
        Thunder();
    }

    public void TafoonSkill()
    {
        if (SkillUse.E_skillCheck == true && SkillUse.R_skillCheck == true)
        {
            SkillUse.E_skillCheck = false;
            SkillUse.R_skillCheck = false;

            SkillUse.MoveSpeed += 5f;
            SpeedUp += 2f;

            SkillUse.Anim.SetFloat("AttackSpeed_1", SpeedUp);
            SkillUse.Anim.SetFloat("AttackSpeed_2", SpeedUp);
            SkillUse.Anim.SetFloat("AttackSpeed_3", SpeedUp);

            StartCoroutine(TafoonSkillUse());
        }
    }

    IEnumerator TafoonSkillUse()
    {
        yield return new WaitForSeconds(BuffTime);
        SkillUse.MoveSpeed = 6f;
        SpeedUp = 1.2f;

        SkillUse.Anim.SetFloat("AttackSpeed_1", SpeedUp);
        SkillUse.Anim.SetFloat("AttackSpeed_2", SpeedUp);
        SkillUse.Anim.SetFloat("AttackSpeed_3", SpeedUp);
    }

    public void IceSkill()
    {
        if (SkillUse.E_skillCheck == true && SkillUse.F_skillCheck == true)
        {
            SkillUse.E_skillCheck = false;
            SkillUse.F_skillCheck = false;

            if(SkillUse.isAttack)
            {
               enemy.chasespeed = 1f;
               StartCoroutine(IceSkillUse());
            }
        }
    }

    IEnumerator IceSkillUse()
    {
        yield return new WaitForSeconds(DebuffTime);
        enemy.chasespeed = 3f;
    }

    public void Thunder()
    {
        if (SkillUse.R_skillCheck == true && SkillUse.F_skillCheck)
        {
            SkillUse.R_skillCheck = false;
            SkillUse.F_skillCheck = false;
            Stun = true;
            StunOn();
        }
    }

    public void StunOn()
    {
        enemy.StartAttack = true;
        StartCoroutine(ThunderSkillUse());
    }

    IEnumerator ThunderSkillUse()
    {
        yield return new WaitForSeconds(StunTime);
        Stun = false;
    }
}
