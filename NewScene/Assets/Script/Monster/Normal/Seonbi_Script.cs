using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Seonbi_Script : Enemy
{
    private Animator animator;
    [SerializeField] float SetY;

    bool isattack;
    bool DontMove;
    public bool BulletCheck;

    public Rigidbody SeonbiBullet;
    public GameObject ragdoll_obj;

    private float Dist;

    protected override void Awake()
    {
        base.Awake();
        damageToGive = 5;
        isattack = false;
    }

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        
    }
    void Update()
    {
        monsterMove();
        NotDamaged();
        DieMonster();
        CanvasMove();
    }

    protected override void CanvasMove()
    {
        
        base.CanvasMove();
    }

    protected override void monsterMove()
    {

        if(Player.HP > 0)
        {
            Dist = Vector3.Distance(transform.position, targetTransform.transform.position);
            base.monsterMove();

            if (!DontMove)
            {
                animator.SetBool("Move", true);
                agent.isStopped = false;
            }
            else
            {
                animator.SetBool("Move", false);
                agent.isStopped = true;
            }

            Vector3 targety = targetTransform.position;
            targety.y = 0;

            if (Dist <= 5)
            {
                if (!isattack)
                {
                    transform.LookAt(targety);
                    isattack = true;
                    StartCoroutine("attacker");
                }
            }
        }
        else
        {
            animator.SetBool("Move", false);
            animator.SetBool("Attack", false);

            agent.isStopped = true;
        }

    }

    protected override void DieMonster()
    {
        if (curHearth < 1)
        {
            GameObject ThrowRockrigid = Instantiate(ragdoll_obj, transform.position, transform.rotation);

            gameObject.SetActive(false);
            OnDisable();
        }
    }

    protected override void GetDamagedAnimation()
    {
        int random = 1;
        random = UnityEngine.Random.Range(1, 3);
        StartCoroutine(damaged_ani(random));
    }

    IEnumerator attacker()
    {
        DontMove = true;
        animator.SetBool("Move", false);
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(0.9f);

        Vector3 targety = targetTransform.position;
        targety.y = 0;

        Vector3 target_ = transform.position;
        target_.y = transform.position.y;

        transform.LookAt(targety);

        Rigidbody ThrowRockrigid = Instantiate(SeonbiBullet, target_, transform.rotation);
        Seonbi_bullet bullet = ThrowRockrigid.GetComponent<Seonbi_bullet>();
        bullet.SeonbiDamageToGive = damageToGive;
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(1.3f);



        yield return new WaitForSeconds(0.7f);
        isattack = false;
        DontMove = false;
    }

    IEnumerator damaged_ani(int random)
    {
        animator.SetInteger("Hit", random);
        yield return new WaitForSeconds(0.6f);
        animator.SetInteger("Hit", 0);
    }

}
