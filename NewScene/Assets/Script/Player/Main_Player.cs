using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Player : MonoBehaviour
{
    //PlayerSkill skillcall;
    WindStorm iswindstorm;

    [SerializeField] Vector3 MovePlayer;
    [SerializeField] Animator Anim;
    [SerializeField] Collider weaponCollider;

    private HitScript hit;

    [SerializeField] float RotateSpeed = 10f;
    [SerializeField] float MoveSpeed = 1f;
    [SerializeField] float DashSpeed = 5f;
    [SerializeField] GameObject currentATKEffect;
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

    public float MaxDistance = 1.5f;
    public float AttackSpeed = 3f;
    public float addAttackSpeed;

    Queue<int> inputBufferQ = new Queue<int>();


    private void Awake()
    {
        
        hit = weaponCollider.gameObject.GetComponent<HitScript>();
    }

    void Update()
    {
        //attackInputOn = Input.GetMouseButtonDown(0);

        //AnimatorClipInfo[] clips = Anim.GetCurrentAnimatorClipInfo(0);

        //string currAnimName = clips[0].clip.name;

        //switch (currAnimName)
        //{
        //    case "Idle":
        //        if (attackInputOn)
        //        {
        //            inputBufferQ.Enqueue(0);
        //        }

        //        if (Anim.GetBool("isAttack0_to_1") == false && inputBufferQ.Count > 0)
        //        {
        //            Anim.SetBool("isAttack0_to_1", true);
        //            inputBufferQ.Dequeue();
        //        }
        //        break;

        //    case "ATKmotion1":
        //        if (attackInputOn)
        //        {
        //            inputBufferQ.Enqueue(0);
        //        }
        //        Anim.SetBool("isAttack0_to_1", false);
        //        if (Anim.GetBool("isAttack1_to_2") == false && inputBufferQ.Count > 0)
        //        {
        //            Anim.SetBool("isAttack1_to_2", true);
        //            inputBufferQ.Dequeue();
        //        }
        //        break;

        //    case "ATKmotion2":
        //        if (attackInputOn)
        //        {
        //            inputBufferQ.Enqueue(0);
        //        }

        //        Anim.SetBool("isAttack1_to_2", false);
        //        if (Anim.GetBool("isAttack2_to_3") == false && inputBufferQ.Count > 0)
        //        {
        //            Anim.SetBool("isAttack2_to_3", true);
        //            inputBufferQ.Dequeue();
        //        }
        //        break;

        //    case "ATKmotion3":
        //        Anim.SetBool("isAttack0_to_1", false);
        //        Anim.SetBool("isAttack1_to_2", false);
        //        Anim.SetBool("isAttack2_to_3", false);

        //        inputBufferQ.Clear();
        //        break;
        //}

        //if (inputBufferQ.Count > 0)
        //{
        //    isAttack = true;
        //    Anim.SetBool("isAttack", isAttack);
        //}

        //else if (inputBufferQ.Count <= 0)
        //{
        //    isAttack = false;
        //}
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

        Skill_E();
        Skill_F();
        Skill_R();
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

    void FixedUpdate()
    {
        Move();
        CalTargetPos();
        //AnimationBoolCheck();
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
        //��� �ִϸ��̼� �߰�
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
        // �Է°��� Vector3�� ����
        player_Move_Input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        player_Move_Input.Normalize();

        // ī�޶��� Forward�� ������
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
        if (Input.GetKey(KeyCode.E))
        {
            Anim.SetTrigger("Skill");
            WindSkillUI.windGauge += Time.deltaTime;
            E_skillCheck = true;
        }
    }


    public void Skill_R()
    {
        if (Input.GetKey(KeyCode.R))
        {
            CloudSkillUI.cloudGauge += Time.deltaTime;
            R_skillCheck = true;
        }
    }

    public void Skill_F()
    {
        if (Input.GetKey(KeyCode.F))
        {

            RainSkillUI.rainGauge += Time.deltaTime;
            F_skillCheck = true;
        }
    }


    private void AnimationBoolCheck()
    {
        //Anim.SetBool("isAttack", isAttack);
        Anim.SetBool("isMove", isMove);
    }
}


