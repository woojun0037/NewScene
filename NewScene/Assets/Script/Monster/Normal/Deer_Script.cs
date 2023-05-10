using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer_Script : Enemy
{
    private Animator animator;

    [SerializeField]
    float SetY;
    RaycastHit hit;

    bool isattack;
    bool DontMove;

    public GameObject DeerLuncher;
    public GameObject ragdoll_obj;
    private float Dist;
    float time = 0;
    private float spawntime = 2;
    private float current_rosh = 0;

    private float roshspeed = 10f;
    private bool isrosh = false;
    protected bool isdash = false;

    public ParticleSystem particle_attack;

    Vector3 startingPosition;

    protected override void Awake()
    {
        base.Awake();
        isattack = false;
    }

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    public bool one;
    // Update is called once per frame
    void Update()
    {
        monsterMove();
        NotDamaged();
        DieMonster();

        if (isdash)
        {
            rosh();
        }
    }

    protected override void monsterMove()
    {
        if (Player.HP >= 0)
        {
            Dist = Vector3.Distance(transform.position, targetTransform.transform.position);

            base.monsterMove();

            if (!DontMove) //공격중에는 이동 기능 정지
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
            targety.y = SetY;

            //현재 경로에서 목표 지점까지 남아있는 거리
            if (Dist <= 5)
            {
                if (!isattack)
                {
                    randattack();
                    //transform.LookAt(targety);
                    //isattack = true;
                    //StartCoroutine("attacker");
                }
            }
        }
        else
        {
            animator.SetBool("Move", false);
            animator.SetBool("isAttack", false);

            agent.isStopped = true;
        }
    }

    protected override void DieMonster()
    {
        if (curHearth < 1)
        {
            GameObject ThrowRockrigid = Instantiate(ragdoll_obj, transform.position, transform.rotation);

            gameObject.SetActive(false);
            agent.enabled = false;

        }
    }
    protected override void GetDamagedAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("isHit", true);

            int random;
            random = UnityEngine.Random.Range(1, 3);
            StartCoroutine(damaged_ani(random));
        }

    }

    IEnumerator attacker()
    {
        DontMove = true;
        int random;
        random = UnityEngine.Random.Range(1, 4);

        Vector3 targety = targetTransform.position;
        targety.y = SetY;

        animator.SetBool("Move", false);
        //transform.LookAt(targety);

        animator.SetBool("isAttack", true);
        animator.SetInteger("Attack", random);

        yield return new WaitForSeconds(1f);

        Vector3 target_ = transform.position;
        target_.y = SetY + 0.5f;

        Instantiate(DeerLuncher, target_, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        //transform.LookAt(targety);


        yield return new WaitForSeconds(1f);

        animator.SetBool("isAttack", false);
        animator.SetInteger("Attack", 0);

        yield return new WaitForSeconds(3f);
        isattack = false;
        DontMove = false;
    }

    IEnumerator damaged_ani(int random)
    {
        animator.SetInteger("Hit", random);
        yield return new WaitForSeconds(0.6f);
        animator.SetBool("isHit", false);
        animator.SetInteger("Hit", 0);
    }

    IEnumerator rotation_C()
    {
        //Vector3 targetDirection = (targetTransform.position - transform.position).normalized;
        //Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 100f * Time.deltaTime);

        animator.SetBool("isAttack", true);
        animator.SetInteger("Attack", 1);
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(targetTransform.position - transform.position);
        transform.rotation = Quaternion.Lerp(startRotation, targetRotation, 10f * Time.deltaTime);
        yield return new WaitForSeconds(3f);
        animator.SetBool("isAttack", false);
        animator.SetInteger("Attack", 0);
    }

    protected void rosh()
    {
        if (!isrosh)
        {
            if (particle_attack != null)
            {
                particle_attack.Play();
            }
            animator.SetBool("Move", true);
            transform.LookAt(targetTransform);
            startingPosition = targetTransform.position;
            isrosh = true;
        }

        if (transform.position == startingPosition) // 돌진 완료
        {
            particle_attack.Stop();
            isdash = false;
            //isrosh = false;
            animator.SetBool("Move", false);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPosition, roshspeed * Time.deltaTime);
        }

    }

    void reset()
    {
        isrosh = false;
    }

    private void randattack()
    {
        reset();
        int rand = Random.Range(0, 2); //0 1

        if (rand == 0)
        {
            isattack = true;
            StartCoroutine("attacker");
        }
        else if (rand == 1)
        {
            isdash = true;
        }
    }
}
