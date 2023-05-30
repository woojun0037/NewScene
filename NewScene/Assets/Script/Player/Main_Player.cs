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

    [SerializeField] private WindStorm windOn;

    private TafoonSkillHit tafoonSkill;
    private PropertySkill propertySkill;
    private HitScript hit;

    public HealthManager heal;
    public PlayerSkill skill;
    public Animator Anim;
    public Enemy enemy;
    public CameraMovemant cam;

    private Vector3 mousePos;

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
    public float BackStep = 2f;
    public float MaxDistance = 1.5f;
    public float AttackSpeed = 3f;

    public float addAttackSpeed;
    public float Duration;
    public float Magnitude;

    private int hitState;
    internal int HitState => hitState;

    private void Awake()
    {
        propertySkill = GetComponent<PropertySkill>(); //GetComponent보단 public으로 
        hit = HitBox.gameObject.GetComponent<HitScript>();
        tafoonSkill = TafoonBox.gameObject.GetComponent<TafoonSkillHit>();
        cam = FindObjectOfType<CameraMovemant>();
    }

    void Update()
    {
        CalTargetPos();
        AttackInput();
        Around();
        Move();
        Skill_E();
        Skill_R();
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

    private void CalTargetPos()
    {
        mousePos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            mousePos.z = Camera.main.transform.position.z;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

            string Platform = "Platform";
            int layerMask = 1 << LayerMask.NameToLayer(Platform);

            if (Physics.Raycast(ray, out RaycastHit rayHit, Mathf.Infinity, layerMask))
            {
                if (rayHit.collider.tag == Platform)
                {
                    transform.LookAt(rayHit.point);
                }
                if (rayHit.collider.tag == Platform && propertySkill.Stun == true)
                {
                    transform.LookAt(rayHit.point);
                    propertySkill.ThunderSkillSpecial(rayHit.point);
                }
            }
        }
    }

    public void PlayerHP(float PlayerHP)
    {
        HP = PlayerHP;
        if (HP < 1)
        {
            string Dead = "Dead";
            Anim.SetTrigger(Dead);
        }
    }

    public void PlayerDead()
    {
        this.gameObject.SetActive(false);
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

        //Vector3 moveHorizontal = transform.right * moveDirX;
        //Vector3 moveVertical = transform.forward * moveDirZ;

        if (moveDirX == 1 && moveDirZ == 1)
        {
            transform.Translate((Vector3.right + Vector3.forward) * Time.deltaTime * MoveSpeed);
            isMove = true;
            AnimationBoolCheck();
        }
        else if (moveDirX == 1 && moveDirZ == -1)
        {
            transform.Translate((Vector3.right + Vector3.back) * Time.deltaTime * MoveSpeed);
            isMove = true;
            AnimationBoolCheck();
        }
        else if (moveDirX == -1 && moveDirZ == 1)
        {
            transform.Translate((Vector3.left + Vector3.forward) * Time.deltaTime * MoveSpeed);
            isMove = true;
            AnimationBoolCheck();
        }
        else if (moveDirX == -1 && moveDirZ == -1)
        {
            transform.Translate((Vector3.left + Vector3.back) * Time.deltaTime * MoveSpeed);
            isMove = true;
            AnimationBoolCheck();
        }
        else if (moveDirZ == 1)
        {
            transform.Translate(UtillScript.Forward * Time.deltaTime * MoveSpeed);//vector는 걍 다 new다, 좌표가 매번 업데이트 되기 때문에 미리 만들어진거면 뚝뚝 끊김
            isMove = true;
            AnimationBoolCheck();
        }
        else if(moveDirZ == -1)
        {
            transform.Translate(UtillScript.Back * Time.deltaTime * MoveSpeed);
            isMove = true;
            AnimationBoolCheck();
        }
        else if(moveDirX == 1)
        {
            transform.Translate(UtillScript.Right * Time.deltaTime * MoveSpeed);
            isMove = true;
            AnimationBoolCheck();
        }
        else if(moveDirX == -1)
        {
            transform.Translate(UtillScript.Left * Time.deltaTime * MoveSpeed);
            isMove = true;
            AnimationBoolCheck();
        }
        else
        {
            isMove = false;
            AnimationBoolCheck();
        }
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
