using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Player : MonoBehaviour
{
    [SerializeField] Vector3 MovePlayer;
    [SerializeField] Rigidbody Rigid;
    [SerializeField] Animator Anim;
    [SerializeField] Collider weaponCollider;

    [SerializeField] float RotateSpeed = 10f;
    [SerializeField] float MoveSpeed = 1f;
    [SerializeField] float DashSpeed = 5f;

    private Vector3 mousePos;
    private Vector3 Dir = Vector3.zero;
    private Vector3 player_Move_Input;
    private Vector3 heading;

    bool isMove = false;
    bool isDelay;
    public bool isAttack = false;
    public bool attackInputOn;

    public int damage;
    public int HP;
    public float MaxDistance = 1.5f;
    public float AttackSpeed = 0.5f;

    Queue<int> inputBufferQ = new Queue<int>();

    void Awake()
    {
        Rigid = this.GetComponent<Rigidbody>();
        
    }

    void Start()
    {
        
    }

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

        Skill();

    }
    void FixedUpdate()
    {
        Move();
        CalTargetPos();
        AnimationBoolCheck();
    }

    private void CalTargetPos()
    {
        mousePos = Input.mousePosition;

        if (Input.GetMouseButton(0))
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
        //사망 애니메이션 추가
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

    public void Skill()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Anim.SetTrigger("Skill");
            WindSkillUI.windGauge += Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.R))
        {
            Anim.SetTrigger("Skill");
            CloudSkillUI.cloudGauge += Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.F))
        {
            Anim.SetTrigger("Skill");
            RainSkillUI.rainGauge += Time.deltaTime;
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


