using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PropertySkill : MonoBehaviour
{
    [SerializeField] Collider HitBox;
    [SerializeField] private float BuffTime = 6f;

    public Transform PlayerPos;
    public Main_Player SkillUse;
    public GangrimSkillUi skillUi;
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
        if(!SkillUse.isAttack) SpecialSkill();
    }

    public void TafoonSkill()
    {
        SkillUse.isAttack = true;
        SkillUse.Anim.SetTrigger("TafoonSkill");
    }

    public void TafoonSkillMotion()
    {
        motion.gameObject.SetActive(true);
        SkillUse.MoveSpeed += 5f;
        SpeedUp += 3f;

        SkillUse.Anim.SetFloat("AttackSpeed_1", SpeedUp);
        SkillUse.Anim.SetFloat("AttackSpeed_2", SpeedUp);
        SkillUse.Anim.SetFloat("AttackSpeed_3", SpeedUp);
        StartCoroutine(TafoonSkillUse());
    }

    public void SpecialSkill()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if ((skillUi.uiKeys[0] == "wind" && skillUi.uiKeys[1] == "tornado") || (skillUi.uiKeys[0] == "tornado" && skillUi.uiKeys[1] == "wind"))
            {
                TafoonSkill();
                skillUi.SkillUiInit();
            }
            else if ((skillUi.uiKeys[0] == "wind" && skillUi.uiKeys[1] == "rain") || (skillUi.uiKeys[0] == "rain" && skillUi.uiKeys[1] == "wind"))
            {
                IceSkill();
                skillUi.SkillUiInit();
            }
        }
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
        SkillUse.isAttack = false;
    }

    public void IceSkill()
    {
        IceSpecial.transform.position = new Vector3(this.transform.position.x, 1, this.transform.position.z);
        IceSpecial.transform.forward = SkillUse.transform.forward;
        IceSpecial.SetActive(true);
        StartCoroutine(IceSetActive());
    }

    private IEnumerator IceSetActive()
    {
        yield return new WaitForSeconds(0.5f);
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
