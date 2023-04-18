using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertySkill : MonoBehaviour
{
    [SerializeField] Collider HitBox;
    [SerializeField] private float BuffTime = 6f;
    [SerializeField] private float DebuffTime = 5f;
    [SerializeField] private float StunTime = 3f;

    private HitScript hit;
    public Main_Player SkillUse;
    public Enemy enemy;
    public GameObject motion;
    public float SpeedUp = 1.2f;

    public bool Tafoon = false;
    public bool TafoonOn = false;
    public bool Debuff = false;
    public bool Stun = false;

    void Awake()
    {
        SkillUse = GetComponent<Main_Player>();
        hit = HitBox.gameObject.GetComponent<HitScript>();
    }

    private void Update()
    {
        TafoonSkill();
        IceSkill();
        Thunder();
    }

    public void TafoonSkill()
    {
        if (SkillUse.Q_skillCheck == true && SkillUse.E_skillCheck == true)
        {
            Tafoon = true;
            motion.gameObject.SetActive(true);
            SkillUse.Q_skillCheck = false;
            SkillUse.E_skillCheck = false;

            SkillUse.MoveSpeed += 5f;
            SpeedUp += 3f;

            SkillUse.Anim.SetFloat("AttackSpeed_1", SpeedUp);
            SkillUse.Anim.SetFloat("AttackSpeed_2", SpeedUp);
            SkillUse.Anim.SetFloat("AttackSpeed_3", SpeedUp);

            if (Input.GetKeyDown(KeyCode.G) && Tafoon)
            {
                TafoonOn = true;
                SkillUse.Anim.SetTrigger("TafoonSkill");
            }
            StartCoroutine(TafoonSkillUse());
        }
    }

    IEnumerator TafoonSkillUse()
    {
        yield return new WaitForSeconds(BuffTime);
        TafoonOn = false;
        Tafoon = false;
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
