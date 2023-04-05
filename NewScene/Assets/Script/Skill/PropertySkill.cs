using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  PropertySkill : MonoBehaviour
{
    Main_Player SkillUse;
    public float SpeedUp;

    private bool TafoonSkillOn;
    private bool TafoonSkillOff;

    private void Start()
    {
        SkillUse = GetComponent<Main_Player>();
    }

    public void TafoonSkill()
    {
        if(SkillUse.E_skillCheck == true && SkillUse.R_skillCheck == true)
        {
            StartCoroutine(TafoonSpecial());
        }
    }

    IEnumerator TafoonSpecial()
    {
        SkillUse.MoveSpeed += 5;
        SkillUse.Anim.SetFloat("AttackSpeed_1", SpeedUp + 2f);
        SkillUse.Anim.SetFloat("AttackSpeed_2", SpeedUp + 2f);
        SkillUse.Anim.SetFloat("AttackSpeed_3", SpeedUp + 2f);
        yield return new WaitForSeconds(5f);
    }

    public void IceSkill()
    {
        
    }

    public void Thunder()
    {

    }
}
