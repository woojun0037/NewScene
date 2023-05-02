using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertySkill : MonoBehaviour
{
    [SerializeField] Collider HitBox;
    [SerializeField] private float BuffTime = 6f;
    
    public Main_Player SkillUse;
    public Enemy enemy;
    public GameObject motion;
    public GameMain TafoonHitBox;

    public float SpeedUp = 1.2f;

    public bool TafoonSpecialMove = false;
    public bool TafoonSpecial = false;
    public bool Debuff = false;
    public bool Stun = false;

    void Awake()
    {
        SkillUse = GetComponent<Main_Player>();
    }

    private void Update()
    {
        TafoonSkill();
        IceSkill();
        Thunder();
        TafoonSkillSpecial();
    }

    public void TafoonSkill()
    {
        if (SkillUse.Q_skillCheck == true && SkillUse.E_skillCheck == true)
        {
            motion.gameObject.SetActive(true);
            SkillUse.Q_skillCheck = false;
            SkillUse.E_skillCheck = false;
            TafoonSpecial = true;

            SkillUse.MoveSpeed += 5f;
            SpeedUp += 3f;

            SkillUse.Anim.SetFloat("AttackSpeed_1", SpeedUp);
            SkillUse.Anim.SetFloat("AttackSpeed_2", SpeedUp);
            SkillUse.Anim.SetFloat("AttackSpeed_3", SpeedUp);

            StartCoroutine(TafoonSkillUse());
        }
    }

    public void TafoonSkillSpecial()
    {
        if (Input.GetKeyDown(KeyCode.G) && TafoonSpecial)
        {
            StartCoroutine(TafoonAttack());
        }
    }

    IEnumerator TafoonAttack()
    {
        TafoonSpecial = false;
        TafoonSpecialMove = true;
        SkillUse.Anim.SetTrigger("TafoonSkill");
        yield return new WaitForSeconds(2.4f);
        TafoonSpecialMove = false;
    }

    IEnumerator TafoonSkillUse()
    {
        yield return new WaitForSeconds(BuffTime);
        SkillUse.MoveSpeed = 6f;
        SpeedUp = 1.2f;

        motion.gameObject.SetActive(false);

        SkillUse.Anim.SetFloat("AttackSpeed_1", SpeedUp);
        SkillUse.Anim.SetFloat("AttackSpeed_2", SpeedUp);
        SkillUse.Anim.SetFloat("AttackSpeed_3", SpeedUp);
    }

    public void IceSkill()
    {
        if (SkillUse.Q_skillCheck == true && SkillUse.R_skillCheck == true)
        {
            Debuff = true;
        }
        else
        {
            Debuff = false;
        }
    }


    public void Thunder()
    {
        if (SkillUse.E_skillCheck == true && SkillUse.R_skillCheck)
        {
            Stun = true;
        }
        else
        {
            Stun = false;
        }
    }
}
