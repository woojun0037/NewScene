using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  PropertySkill : MonoBehaviour
{
    Main_Player SkillUse;

    private void Start()
    {
        SkillUse = GetComponent<Main_Player>();
    }

    public void TafoonSkill()
    {
        if(SkillUse.E_skillCheck == true && SkillUse.R_skillCheck == true)
        {

        }
    }

    public void IceSkill()
    {
        //if(SkillUse.)
    }

    public void Thunder()
    {

    }
}
