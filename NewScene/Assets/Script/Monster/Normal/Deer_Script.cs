using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer_Script : Enemy
{
    private Animator animator;

    bool isattack;
    bool DontMove;
    bool useMoveAni =false;

    public GameObject DeerLuncher;
    public GameObject ragdoll_obj;
    public ParticleSystem particle_attack;
    public EmissionIntensityController emissionController;
    public Renderer deerMaterial;
    public GameObject AttackCol;

    private float Dist;
    protected bool isdash = false;

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
                if(!useMoveAni)
                    animator.SetBool("Move", true);
                agent.isStopped = false;
            }
            else
            {
                if (!useMoveAni)
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
        isattack = true;
        DontMove = true;

        yield return new WaitForSeconds(1f);

        int random;
        random = UnityEngine.Random.Range(1, 4);

        animator.SetBool("Move", false);

        animator.SetBool("isAttack", true);
        animator.SetInteger("Attack", random);

        yield return new WaitForSeconds(1f);
        emissionController.setbasecolor(0);
        emissionController.EmssionMaxMin();
        Vector3 target_ = transform.position;
        target_.y = transform.position.y;

        Instantiate(DeerLuncher, target_, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);

        animator.SetBool("isAttack", false);
        animator.SetInteger("Attack", 0);

        yield return new WaitForSeconds(3.5f);
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

    private void randattack()
    {
        int rand = Random.Range(0, 2); //0 1

        if (rand == 0)
        {
            StartCoroutine("attacker");
        }
        else if (rand == 1)
        {
            NewDash();
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
    void NewDash()
    {
        StartCoroutine(newDash());
    }

    IEnumerator newDash()
    {
        useMoveAni = true;
        DontMove = true;
        isattack = true;

        animator.SetBool("Move", false);

        yield return new WaitForSeconds(1f);

        //Rotation
        Vector3 spawn = Player.transform.position;
        spawn.y = transform.position.y + 10f;

        emissionController.setbasecolor(1);
        emissionController.EmssionMaxMin();

        Vector3 targetDirection = (spawn - transform.position).normalized;
        targetDirection.y = 0; // y 축 회전 무시

        Quaternion rotation = Quaternion.LookRotation(targetDirection);
        Quaternion startRotation = transform.rotation;

        float rotationSpeed = 1f;
        float t = 0;

        animator.SetBool("Move", true);

        while (t < 1)
        {
            t += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, rotation, t);
            yield return null;
        }

        animator.SetBool("Move", false);
        yield return new WaitForSeconds(1.5f);//cool

        //Dash
        Vector3 moveDirection = new Vector3(0, 0, 1); // 앞쪽 방향으로 이동 (Z 축) forward X
        moveDirection.Normalize();

        float elapsedTime = 0f;
        float moveSpeed = 20f;

        Vector3 currentmonster = transform.position;
        Vector3 lastPosition = currentmonster;

        animator.SetBool("Dash", true);

        if (particle_attack != null)
            particle_attack.Play();

        AttackCol.SetActive(true);
        // && Vector3.Distance(currentmonster, spawn) - Vector3.Distance(currentmonster, transform.position) >= 1f
        while (elapsedTime < 0.8f && Vector3.Distance(currentmonster, spawn) - Vector3.Distance(lastPosition, transform.position) >= 1f)
        {
            RaycastHit hit;
            Vector3 Hit = transform.position;
            Hit.y = transform.position.y + 2f;

            if (Physics.Raycast(Hit, transform.forward, out hit, 1f))
            {
                if (!hit.collider.CompareTag("Player")) 
                    break;
            }

            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        particle_attack.Stop();
        AttackCol.SetActive(false);
        animator.SetBool("Dash", false);

        yield return new WaitForSeconds(3f);//cool
        isattack = false;
        useMoveAni = false;
        DontMove = false;
    }

}
