using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Player : MonoBehaviour
{

    [SerializeField] Vector3 MovePlayer;

    [SerializeField] Collider HitBox;
    [SerializeField] Collider WindBox;
    [SerializeField] Collider TafoonBox;

    [SerializeField] GameObject currentATKEffect;
    [SerializeField] float RotateSpeed = 10f;

    private HitScript hit;
    private WindStorm windOn;
    private TafoonSkillHit tafoonSkill;
    private PropertySkill propertySkill;
    private GangrimSKill skillui;

    public Animator Anim;
    public Enemy enemy;
    public MainCamera mainCamera;
    
    private Vector3 mousePos;
    private Vector3 player_Move_Input;
    private Vector3 heading;

    private bool isMove = false;

    public GameObject[] AtkEffect;
    public bool[] isClicks;

    public bool Q_skillCheck;
    public bool E_skillCheck;
    public bool R_skillCheck;

    public bool isAttack = false;
    public bool isWind = false;

    public bool attackInputOn;
    public bool isTafoon;

    public float damage;
    public float HP;

    public float MoveSpeed = 6f;
    public float MaxDistance = 1.5f;
    public float AttackSpeed = 3f;
    public float addAttackSpeed;
    public float Duration;
    public float Magnitude;

    int hitState;
    internal int HitState => hitState;

    private void Awake()
    {
        propertySkill = GetComponent<PropertySkill>();
        hit = HitBox.gameObject.GetComponent<HitScript>();
        windOn = WindBox.gameObject.GetComponent<WindStorm>();
        tafoonSkill = TafoonBox.gameObject.GetComponent<TafoonSkillHit>();
    }

    void Update()
    {
        AttackInput();
        Skill_Q();
        Skill_E();
        Skill_R();
    }

    void FixedUpdate()
    {
        Move();
        CalTargetPos();
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
        windOn.gameObject.SetActive(true);
    }

    public void OffWind()
    {
        windOn.gameObject.SetActive(false);
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
            Anim.SetTrigger("isAttack_1");
        }
        else if (Input.GetMouseButtonDown(0) && isClicks[0] && isClicks[1] && !isClicks[2])
        {
            hitState = 2;
            isAttack = true;
            Anim.SetTrigger("isAttack_2");
        }
        else if (Input.GetMouseButtonDown(0) && isClicks[0] && isClicks[1] && isClicks[2])
        {
            hitState = 3;
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
                if (rayHit.collider.tag == "Platform" || rayHit.collider.tag == "Monster")
                {
                    transform.LookAt(rayHit.point);
                }
            }
        }
    }

    public void PlayerHP(float PlayerHP)
    {
        HP = PlayerHP;
        if (HP < 1)
        {
            Anim.SetTrigger("Dead");
            Destroy(gameObject, 5f);  
        }
    }

    public void ATK_Effect_On(int on_count)
    {
        if (currentATKEffect == null)
        {
            Vector3 dir = transform.position;
            dir.y = 3f;

            currentATKEffect = Instantiate(AtkEffect[on_count],transform);
            currentATKEffect.transform.position = dir;

            //currentATKEffect.transform.position = new veoctransform.position.y;
            //currentATKEffect.transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
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

        if (player_Move_Input != Vector3.zero && !isAttack && !propertySkill.TafoonSpecialMove && !isWind && hitState == 0)
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

    public void Skill_Q()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Anim.SetTrigger("WindSkill");
            //skillui.WindSkillUI();

            Q_skillCheck = true;
            isWind = true;

            StartCoroutine(Skill_Q_Cool());
        }
    }

    public void Skill_E()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CloudSkillUI.cloudGauge += Time.deltaTime;
            E_skillCheck = true;
        }
    }

    public void Skill_R()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RainSkillUI.rainGauge += Time.deltaTime;
            R_skillCheck = !R_skillCheck;
        }
    }

    IEnumerator Skill_Q_Cool()
    {
        isWind = false;
        yield return new WaitForSeconds(2.4f);
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


