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

    [SerializeField] float RotateSpeed = 10f;
    [SerializeField] float MoveSpeed = 1f;
    [SerializeField] float DashSpeed = 5f;

    private Vector3 mousePos;
    private Vector3 player_Move_Input;
    private Vector3 heading;

    public bool E_skillCheck;
    public bool R_skillCheck;
    public bool F_skillCheck;

    bool isMove = false;
    bool isDelay;
    public bool isAttack = false;
    public bool attackInputOn;

    public int damage;
    public int HP;
    public float MaxDistance = 1.5f;
    public float AttackSpeed = 3f;
    public float addAttackSpeed;

    Queue<int> inputBufferQ = new Queue<int>();

    void Update()
    {
        attackInputOn = Input.GetMouseButtonDown(0);

        AnimatorClipInfo[] clips = Anim.GetCurrentAnimatorClipInfo(0);

        string currAnimName = clips[0].clip.name;

        switch (currAnimName)
        {
            case "Idle":
                if (attackInputOn)
                {
                    inputBufferQ.Enqueue(0);
                }
                if (Anim.GetBool("isAttack0_to_1") == false && inputBufferQ.Count > 0)
                {
                    Anim.SetBool("isAttack0_to_1", true);
                    inputBufferQ.Dequeue();
                }
                break;
            case "ATKmotion1":
                if (attackInputOn)
                {
                    inputBufferQ.Enqueue(0);
                    HitCheck_true_1();
                }
                Anim.SetBool("isAttack0_to_1", false);
                if (Anim.GetBool("isAttack1_to_2") == false && inputBufferQ.Count > 0)
                {
                    Anim.SetBool("isAttack1_to_2", true);
                    inputBufferQ.Dequeue();
                }
                break;
            case "ATKmotion2":
                if (attackInputOn)
                {
                    inputBufferQ.Enqueue(0);
                    HitCheck_true_2();
                }
                Anim.SetBool("isAttack1_to_2", false);
                if (Anim.GetBool("isAttack2_to_3") == false && inputBufferQ.Count > 0)
                {
                    Anim.SetBool("isAttack2_to_3", true);
                    inputBufferQ.Dequeue();
                }
                break;
            case "ATKmotion3":
                HitCheck_true_3();
                Anim.SetBool("isAttack0_to_1", false);
                Anim.SetBool("isAttack1_to_2", false);
                Anim.SetBool("isAttack2_to_3", false);

                inputBufferQ.Clear();
                break;
        }

        if (inputBufferQ.Count > 0)
        {
            isAttack = true;
            Anim.SetBool("isAttack", isAttack);
        }
        else if (inputBufferQ.Count <= 0)
        {
            isAttack = false;
        }

        Skill_E();
        Skill_F();
        Skill_R();
        Tafoon();
        Frozen();
        Thunder();
        Anim.SetFloat("AttackSpeed", AttackSpeed * addAttackSpeed);
    }

    void FixedUpdate()
    {
        Move();
        Dash();
        CalTargetPos();
        AnimationBoolCheck();
    }

    private void CalTargetPos()
    {
        mousePos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
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
            float angle = Mathf.Atan2(heading.z, heading.x) * Mathf.Rad2Deg * -2;

            transform.rotation = Quaternion.Lerp(transform.rotation,
                                 Quaternion.Euler(0, angle, 0), Time.deltaTime * RotateSpeed);

            transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed);
        }
        else
        {
            isMove = false;
        }
    }

    private void Dash()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (isDelay == false)
            {
                isMove = true;
                transform.position = this.transform.position + new Vector3(0, 0, 1) * Time.deltaTime;
                StartCoroutine("DashCool");
                isDelay = true;
            }
        }
    }

    public void Skill_E()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Anim.SetTrigger("Skill");
            E_skillCheck = true;
            WindSkillUI.windGauge += Time.deltaTime;
        }
    }


    public void Skill_R()
    { 
        if (Input.GetKey(KeyCode.R))
        {
            R_skillCheck = true;
            CloudSkillUI.cloudGauge += Time.deltaTime;
        }
    }

    public void Skill_F()
    {
        if (Input.GetKey(KeyCode.F))
        {
            F_skillCheck = true;
            RainSkillUI.rainGauge += Time.deltaTime;
        }
    }

    public void Tafoon()//��ǳ ��ų
    {
        if (E_skillCheck == true && R_skillCheck == true)
        {
            addAttackSpeed = 3f;
            E_skillCheck = false;
            R_skillCheck = false;

            if(E_skillCheck == false || R_skillCheck == false)
            {
                addAttackSpeed = 1f;
            }
        }
    }

    public void Frozen()//���� ��ų
    {
        if(E_skillCheck == true && F_skillCheck)
        {
           
        }
    }

    public void Thunder()//���� ��ų
    {
        if(R_skillCheck == true && F_skillCheck == true)
        {

        }
    }

    IEnumerator DashCool()
    {
        yield return new WaitForSeconds(0.5f);
        isDelay = false;
    }

    private void AnimationBoolCheck()
    {
        Anim.SetBool("isMove", isMove);
    }


    void HitCheck_true_1()
    {
        if (isAttack == true)
        {
            weaponCollider.enabled = true;
        }
        else
        {
            weaponCollider.enabled = false;
        }
    }

    void HitCheck_true_2()
    {
        if (isAttack == true)
        {
            weaponCollider.enabled = true;
        }
        else
        {
            weaponCollider.enabled = false;
        }
    }

    void HitCheck_true_3()
    {
        if (isAttack == true)
        {
            weaponCollider.enabled = true;
        }
        else
        {
            weaponCollider.enabled = false;
        }
    }

}


