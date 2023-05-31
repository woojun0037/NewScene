using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertySkill : MonoBehaviour
{
    [SerializeField] Collider HitBox;
    [SerializeField] private float BuffTime = 6f;

    public Transform PlayerPos;
    public Main_Player SkillUse;
    public Enemy enemy;
    public GameObject icepos;
    public GameObject motion;
    public GameObject IceSpecial;
    public GameObject TurnderSpecial;

    public float SpeedUp = 1.2f;

    public bool TafoonSpecialMove = false;
    public bool TafoonSpecial = false;
    public bool Debuff = false;
    public bool Stun = false;

    private void Update()
    {
        IceSkill();
        Thunder();
        TafoonSkillSpecial();
        IceSkillSpecial();
    }

    public void TafoonSkill()
    {
        motion.gameObject.SetActive(true);
        SkillUse.Q_skillCheck = false;
        SkillUse.E_skillCheck = false;
        TafoonSpecial = false;

        SkillUse.MoveSpeed += 5f;
        SpeedUp += 3f;

        SkillUse.Anim.SetFloat("AttackSpeed_1", SpeedUp);
        SkillUse.Anim.SetFloat("AttackSpeed_2", SpeedUp);
        SkillUse.Anim.SetFloat("AttackSpeed_3", SpeedUp);

        StartCoroutine(TafoonSkillUse());
    }

    public void TafoonSkillSpecial()
    {
        if (SkillUse.Q_skillCheck == true && SkillUse.E_skillCheck == true)
        {
            TafoonSpecial = true;
            if (Input.GetKeyDown(KeyCode.F) && TafoonSpecial)
            {
                StartCoroutine(TafoonAttack());
                TafoonSkill();
            }
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

    public void IceSkillSpecial()
    {
        if (Input.GetKeyDown(KeyCode.G) && Debuff == true)
        {
            Debuff = false;
            IceSpecial.transform.position = icepos.transform.position;
            IceSpecial.transform.rotation = icepos.transform.rotation;
            IceSpecial.SetActive(true);
            StartCoroutine(IceSetActive());
        }
    }

    private IEnumerator IceSetActive()
    {
        yield return new WaitForSeconds(1.0f);
        IceSpecial.SetActive(false);
    }

    public void Thunder()
    {
        if (SkillUse.E_skillCheck == true && SkillUse.R_skillCheck == true)
        {
            Stun = true;
            SkillUse.E_skillCheck = false;
            SkillUse.R_skillCheck = false;
            StartCoroutine(ThunderCor());
        }
    }

    IEnumerator ThunderCor()
    {
        yield return new WaitForSeconds(3f);
        Stun = false;
        TurnderSpecial.SetActive(false);
    }

    public void ThunderSkillSpecial(Vector3 transform)
    {
        TurnderSpecial.transform.position = transform;
        TurnderSpecial.SetActive(false);
        TurnderSpecial.SetActive(true);
    }
}
