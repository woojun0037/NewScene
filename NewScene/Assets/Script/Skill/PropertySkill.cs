using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertySkill : MonoBehaviour
{
    [SerializeField] private float BuffTime = 6f;
    Main_Player SkillUse;
    public float SpeedUp = 1.2f;

    private void Start()
    {
        SkillUse = GetComponent<Main_Player>();
    }

    private void Update()
    {
        TafoonSkill();
    }

    public void TafoonSkill()
    {
        if (SkillUse.E_skillCheck == true && SkillUse.R_skillCheck == true)
        {
            SkillUse.E_skillCheck = false;
            SkillUse.R_skillCheck = false;

            SkillUse.MoveSpeed +=5f;
            SpeedUp += 1f;

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

    }

    public void Thunder()
    {

    }
}
