using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer_Script : Enemy
{
    private Animator animator;

    bool isattack;
    bool DontMove;

    public float attackCoolTime;
    public GameObject DeerLuncher;
    public GameObject ragdoll_obj;
    private float Dist;

    private float roshspeed = 10f;
    private bool isrosh = false;
    protected bool isdash = false;

    public ParticleSystem particle_attack;
    public EmissionIntensityController emissionController;
    [SerializeField]
    Vector3 startingPosition;
    public Renderer deerMaterial;

    protected override void Awake()
    {
        base.Awake();
        isattack = false;
    }

    protected override void Start()
    {
        emissionController = GetComponent<EmissionIntensityController>();
        base.Start();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        monsterMove();
        NotDamaged();
        DieMonster();
        CanvasMove();
        if (isdash)
        {
            rosh();
        }
    }

    protected override void CanvasMove()
    {

        base.CanvasMove();
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

            //현재 경로에서 목표 지점까지 남아있는 거리
            if (Dist <= 5)
            {
                if (!isattack)
                    randattack();
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
            OnDisable();

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

        animator.SetBool("Move", false);

        animator.SetBool("isAttack", true);
        animator.SetInteger("Attack", random);

        yield return new WaitForSeconds(1f);
        emissionController.EmssionMaxMin();
        Vector3 target_ = transform.position;
        target_.y = transform.position.y;

        Instantiate(DeerLuncher, target_, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);

        animator.SetBool("isAttack", false);
        animator.SetInteger("Attack", 0);

        yield return new WaitForSeconds(3f + attackCoolTime);
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


    protected void rosh()
    {
        DontMove = true;

        if (!isrosh)
        {
            if (particle_attack != null)
            {
                particle_attack.Play();
            }
            animator.SetBool("Move", true);
            transform.LookAt(targetTransform);
            startingPosition = targetTransform.position;
            //startingPosition.y = 0;

            isrosh = true;
        }

        if (Vector3.Distance(transform.position, startingPosition) < 1)
        {
            particle_attack.Stop();
            isdash = false;
            isattack = false;
            DontMove = false;
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
            isattack = true;
            isdash = true;
        }
    }

    IEnumerator SmoothLookAtPlayer()
    {
        while (true)
        {
            Vector3 targetDirection = Player.transform.position - transform.position;
            targetDirection.y = 0f; // 상하 방향의 회전을 고려하지 않음

            if (targetDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10);
            }

            yield return null;
        }
    }

}
