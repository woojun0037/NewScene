using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Player : MonoBehaviour
{

    [SerializeField] Vector3 MovePlayer;
    [SerializeField] Collider weaponCollider;
    [SerializeField] GameObject currentATKEffect;

    [SerializeField] float RotateSpeed = 10f;
    [SerializeField] float DashSpeed = 5f;

    private HitScript hit;
    public Animator Anim;
    public Enemy enemy;
    private Vector3 mousePos;
    private Vector3 player_Move_Input;
    private Vector3 heading;

    private bool isMove = false;

    public GameObject[] AtkEffect;

    public bool E_skillCheck;
    public bool R_skillCheck;
    public bool F_skillCheck;

    public bool[] isClicks;

    public bool isAttack = false;
    public bool attackInputOn;

    public int damage;
    public int HP;

    public float MoveSpeed = 6f;
    public float MaxDistance = 1.5f;
    public float AttackSpeed = 3f;
    public float addAttackSpeed;

    private void Awake()
    {
        hit = weaponCollider.gameObject.GetComponent<HitScript>();
    }

    void Update()
    {
        Skill_E();
        Skill_F();
        Skill_R();
    }

    void FixedUpdate()
    {
        Move();
        AttackInput();
        CalTargetPos();
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
    }

    private void AttackInput()
    { 
        if (Input.GetMouseButtonDown(0) && !PlayerSkill.Instance.isSkillOn && isClicks[0] && !isClicks[1] && !isClicks[2])
        {
            isAttack = true;
            Anim.SetTrigger("isAttack_1");
        }
        if (Input.GetMouseButtonDown(0) && !PlayerSkill.Instance.isSkillOn && isClicks[0] && isClicks[1] && !isClicks[2])
        {
            isAttack = true;
            Anim.SetTrigger("isAttack_2");
        }
        if (Input.GetMouseButtonDown(0) && !PlayerSkill.Instance.isSkillOn && isClicks[0] && isClicks[1] && isClicks[2])
        {
            isAttack = true;
            Anim.SetTrigger("isAttack_3");
        }
    }

    private void CalTargetPos()
    {
        mousePos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            mousePos.z = Camera.main.transform.position.z;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

            if (Physics.Raycast(ray, out RaycastHit rayHit))
            {
                if (rayHit.collider.tag == "Platform")
                {
                    transform.LookAt(rayHit.point);
                }
            }
        }
    }

    public void PlayerHP(int PlayerHP)
    {
        HP = PlayerHP;
        if (HP < 0)
        {
            Destroy(gameObject);
        }
        //사망 애니메이션 추가
    }

    public void ATK_Effect_On(int on_count)
    {
        if (currentATKEffect == null)
        {
            currentATKEffect = Instantiate(AtkEffect[on_count], transform.position, AtkEffect[on_count].transform.rotation);
        }

    }

    public void ATK_Effect_Off()
    {
        Destroy(currentATKEffect);
    }

    private void Move()
    {
        // 입력값을 Vector3에 저장
        player_Move_Input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        player_Move_Input.Normalize();

        // 카메라의 Forward를 가져옴
        heading = Camera.main.transform.forward;
        heading.y = 0;
        heading.Normalize();
        heading = heading - player_Move_Input;

        if (player_Move_Input != Vector3.zero && !isAttack)
        {
            isMove = true;
            AnimationBoolCheck();
            float angle = Mathf.Atan2(heading.z, heading.x) * Mathf.Rad2Deg * -2;

            transform.rotation = Quaternion.Lerp(transform.rotation,
                                 Quaternion.Euler(0, angle, 0), Time.deltaTime * RotateSpeed);
            transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed);
        }
        else
        {
            isMove = false;

            AnimationBoolCheck();
        }
    }

    public void Skill_E()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
           Anim.SetTrigger("Skill");
           WindSkillUI.windGauge += Time.deltaTime;
           E_skillCheck = true;
        }
    }

    public void Skill_R()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
           CloudSkillUI.cloudGauge += Time.deltaTime;
           R_skillCheck = true;
        }
    }

    public void Skill_F()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
           RainSkillUI.rainGauge += Time.deltaTime;
           F_skillCheck = !F_skillCheck;
        }
    }

    public void GetDamage(float damage)
    {
        Debug.Log("Get Damage" + damage);

    }

    private void AnimationBoolCheck()
    {
        Anim.SetBool("isMove", isMove);
    }
}


