using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Player : MonoBehaviour
{
    [SerializeField] Vector3 MovePlayer;
    [SerializeField] Collider HitBox;
    [SerializeField] Collider TafoonBox;
    [SerializeField] GameObject currentATKEffect;

    public TafoonSkillHit tafoonSkill;
    public PropertySkill propertySkill;
    public HitScript hit;

    public HealthManager heal;
    public PlayerSkill skill;
    public Animator Anim;
    public Enemy enemy;
    public CameraMovemant cam;

    private Vector3 mousePos;
    private Vector3 player_Move_Input;

    private bool isMove = false;
    private bool isBack = false;

    public GameObject windHitBox;
    public GameObject[] AtkEffect;
    public bool[] isClicks;

    public bool Q_skillCheck;
    public bool E_skillCheck;
    public bool R_skillCheck;

    public bool attackInputOn;

    public bool isAttack = false;
    public bool isDash = true;
    public bool isWind = false;
    public bool isTafoon;


    public float damage;
    public float HP;
    public float MoveSpeed = 6f;
    public float BackStep = 2f;
    public float MaxDistance = 1.5f;
    public float AttackSpeed = 3f;

    public float addAttackSpeed;
    public float Duration;
    public float Magnitude;

    private int hitState;
    internal int HitState => hitState;

    void Update()
    {
        //CalTargetPos();
        Move();
        AttackInput();
        Dash();
        AnimationBoolCheck();
        Skill_E();
        Skill_R();
        Around();
    }

    public void OnWeapon()
    {
        hit.gameObject.SetActive(true);
    }

    public void OffWeapon()
    {
        hit.gameObject.SetActive(false);
    }

    public void OnWind()
    {
        windHitBox.gameObject.SetActive(true);
    }

    public void OffWind()
    {
        windHitBox.gameObject.SetActive(false);
    }

    public void TafoonON()
    {
        tafoonSkill.gameObject.SetActive(true);
    }

    public void TafoonOFF()
    {
        tafoonSkill.gameObject.SetActive(false);
    }

    public void SetAnimCheck(int count)
    {
        isClicks[count] = true;
    }

    public void GetAnimCheck()
    {
        isAttack = false;
        isClicks[0] = true;
        isClicks[1] = false;
        isClicks[2] = false;
        hitState = 0;
    }

    private void AttackInput()
    {

        if (Input.GetMouseButtonDown(0) && isClicks[0] && !isClicks[1] && !isClicks[2])
        {
            hitState = 1;
            isAttack = true;
            string Attack1 = "isAttack_1";
            Anim.SetTrigger(Attack1);
        }
        else if (Input.GetMouseButtonDown(0) && isClicks[0] && isClicks[1] && !isClicks[2])
        {
            hitState = 2;
            isAttack = true;
            string Attack2 = "isAttack_2";
            Anim.SetTrigger(Attack2);
        }
        else if (Input.GetMouseButtonDown(0) && isClicks[0] && isClicks[1] && isClicks[2])
        {
            hitState = 3;
            isAttack = true;
            string Attack3 = "isAttack_3";
            Anim.SetTrigger(Attack3);
        }
    }

    public void PlayerHP(float PlayerHP)
    {
        HP = PlayerHP;
        if (HP < 1)
        {
            string Dead = "Dead";
            Anim.SetTrigger(Dead);
            this.gameObject.SetActive(false);
        }
    }

    public void ATK_Effect_On(int on_count)
    {
        if (currentATKEffect == null)
        {
            Vector3 dir = transform.position;
            dir.y = transform.position.y + 1.5f;

            currentATKEffect = AtkEffect[on_count];
            currentATKEffect.transform.position = dir;
            currentATKEffect.SetActive(true);
        }
    }

    public void ATK_Effect_Off()
    {
        currentATKEffect.SetActive(false);
        currentATKEffect = null;
    }

    private void Move()
    {
        string Horizontal = "Horizontal";
        float moveDirX = Input.GetAxisRaw(Horizontal);

        string Vertical = "Vertical";
        float moveDirZ = Input.GetAxisRaw(Vertical);

        if (moveDirX == 1 && moveDirZ == 1)
        {
            transform.Translate((Vector3.right + Vector3.forward) * Time.deltaTime * MoveSpeed);
            isMove = true;
        }
        else if (moveDirX == 1 && moveDirZ == -1)
        {
            transform.Translate((Vector3.right + Vector3.back) * Time.deltaTime * MoveSpeed);
            isMove = true;
        }
        else if (moveDirX == -1 && moveDirZ == 1)
        {
            transform.Translate((Vector3.left + Vector3.forward) * Time.deltaTime * MoveSpeed);
            isMove = true;
        }
        else if (moveDirX == -1 && moveDirZ == -1)
        {
            transform.Translate((Vector3.left + Vector3.back) * Time.deltaTime * MoveSpeed);
            isMove = true;
        }
        else if (moveDirZ == 1)
        {
            transform.Translate(UtillScript.Forward * Time.deltaTime * MoveSpeed);
            isMove = true;
        }
        else if (moveDirZ == -1)
        {
            transform.Translate(UtillScript.Back * Time.deltaTime * MoveSpeed);
            isMove = true;
        }
        else if (moveDirX == 1)
        {
            transform.Translate(UtillScript.Right * Time.deltaTime * MoveSpeed);
            isMove = true;
        }
        else if (moveDirX == -1)
        {
            transform.Translate(UtillScript.Left * Time.deltaTime * MoveSpeed);
            isMove = true;
        }
        else
        {
            isMove = false;
        }
    }

    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Anim.SetTrigger("isDash");
            isDash = false;
            StartCoroutine(DashCor());
        }
    }

    IEnumerator DashCor()
    {
        isDash = true;
        yield return new WaitForSeconds(1f);
    }

    private void Around()
    {
        Vector3 playerRotate = UtillScript.Scale(cam.transform.forward, new Vector3(1, 0, 1));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * cam.smoothness);
    }

    public void Skill_Q()
    {
        string windSkill = "WindSkill";
        Anim.SetTrigger(windSkill);
        Q_skillCheck = true;
    }

    public void Skill_E()
    {
        if (Input.GetKey(KeyCode.E))
        {
            E_skillCheck = true;
        }
    }

    public void Skill_R()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            R_skillCheck = true;
        }
    }

    public void GetDamage(float damage)
    {
        Debug.Log("Get Damage" + damage);
    }

    private void AnimationBoolCheck()
    {
        string Move = "isMove";
        Anim.SetBool(Move, isMove);
    }
}
