using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Player : MonoBehaviour
{
    public static Main_Player instance;
    public Enemy enemy;

    [Header("Player 정보")]
    [SerializeField] Vector3 MovePlayer;
    [SerializeField] Collider HitBox;
    [SerializeField] Collider TafoonBox;
    [SerializeField] GameObject currentATKEffect;
    public HealthManager heal;
    public HitScript hit;
    public Animator Anim;

    [Header("Sound")]
    [SerializeField] private string AttackSound1;
    [SerializeField] private string AttackSound2;//사운드

    [Header("Skill")]
    public TafoonSkillHit tafoonSkill;
    public PlayerSkill skill;
    public PropertySkill propertySkill;
    public GameObject windHitBox;
    public GameObject[] AtkEffect;

    [Header("cam")]
    public CameraMovemant cam;

    [Header("BoolCheck")]
    public bool[] isClicks;
    public bool isMove = false;
    public bool Q_skillCheck;
    public bool E_skillCheck;
    public bool R_skillCheck;
    public bool attackInputOn;
    public bool isAttack = false;
    public bool isTafoon;

    [Header("PlayerStats")]
    public float damage;
    public float HP;
    public float MoveSpeed = 6f;
    public float BackStep = 2f;
    public float MaxDistance = 1.5f;
    public float AttackSpeed = 3f;
    public float addAttackSpeed;
    public float Duration;
    public float Magnitude;

    private Vector3 dir;
    private int hitState;
    internal int HitState => hitState;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        Move();
        AttackInput();
        AnimationBoolCheck();
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

        Anim.ResetTrigger("isAttack_1");
        Anim.ResetTrigger("isAttack_2");
        Anim.ResetTrigger("isAttack_3");
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
            SoundManager.instance.PlaySE(AttackSound1);
        }
        else if (Input.GetMouseButtonDown(0) && isClicks[0] && isClicks[1] && !isClicks[2])
        {
            hitState = 2;
            isAttack = true;
            string Attack2 = "isAttack_2";
            Anim.SetTrigger(Attack2);
            SoundManager.instance.PlaySE(AttackSound2);
        }
        else if (Input.GetMouseButtonDown(0) && isClicks[0] && isClicks[1] && isClicks[2])
        {
            hitState = 3;
            isAttack = true;
            string Attack3 = "isAttack_3";
            Anim.SetTrigger(Attack3);
            SoundManager.instance.PlaySE(AttackSound1);
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
        if (!isAttack)
        {
            string Horizontal = "Horizontal";
            float moveDirX = Input.GetAxisRaw(Horizontal);

            string Vertical = "Vertical";
            float moveDirZ = Input.GetAxisRaw(Vertical);
            
            transform.Translate(MoveSpeed * Time.deltaTime * new Vector3(moveDirX, 0f, moveDirZ).normalized);

            if(moveDirZ == 1 || moveDirZ == -1 || moveDirX == 1 || moveDirX == -1)
            {
                isMove = true;
                
            }
            else
            {
                isMove = false;
            }
        }
    }

   
    private void Around()
    {
        
        Vector3 playerRotate = UtillScript.Scale(cam.transform.forward, new Vector3(1, 0, 1));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * cam.smoothness);
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
