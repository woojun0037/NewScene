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
    public float currentHp;

    public float rotateSpeed = 5f;
    public float MoveSpeed = 6f;
    public float BackStep = 2f;
    public float MaxDistance = 1.5f;
    public float AttackSpeed = 3f;
    public float addAttackSpeed;
    public float Duration;
    public float Magnitude;

    private Vector3 dir = Vector3.zero;
    private Vector3 player_Move_Input;
    private Vector3 heading;
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

    private void Start()
    {
        currentHp = HP;
    }

    void Update()
    {
        Move();
        PlayerHP();
        AttackInput();
        AnimationBoolCheck();
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
        isAttack = false;
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

        if (Input.GetMouseButtonDown(0) && isClicks[0] && !isClicks[1] && !isClicks[2] && !isAttack)
        {
            hitState = 1;
            isAttack = true;
            string Attack1 = "isAttack_1";
            Anim.SetTrigger(Attack1);
            SoundManager.instance.PlaySE(SoundManager.instance.effectSounds[0].clip);
        }
        else if (Input.GetMouseButtonDown(0) && isClicks[0] && isClicks[1] && !isClicks[2] && !isAttack)
        {
            hitState = 2;
            isAttack = true;
            string Attack2 = "isAttack_2";
            Anim.SetTrigger(Attack2);
            SoundManager.instance.PlaySE(SoundManager.instance.effectSounds[1].clip);
        }
        else if (Input.GetMouseButtonDown(0) && isClicks[0] && isClicks[1] && isClicks[2] && !isAttack)
        {
            hitState = 3;
            isAttack = true;
            string Attack3 = "isAttack_3";
            Anim.SetTrigger(Attack3);
            SoundManager.instance.PlaySE(SoundManager.instance.effectSounds[2].clip);
        }
        else if(!isClicks[0] && !isClicks[1] && !isClicks[2])
        {
            isAttack = false;
        }
    }

    public void PlayerHP()
    {
        if (currentHp < 1)
        {
            StartCoroutine(DeadCor());
        }
    }

    IEnumerator DeadCor()
    {
        string Dead = "Dead";
        Anim.SetTrigger(Dead);
        yield return new WaitForSeconds(0.01f);
        float curAnimationTime = Anim.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(curAnimationTime);
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

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, angle, 0), Time.deltaTime * rotateSpeed);

            transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed);
        }
        else
        {
            isMove = false;
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
