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

    public Rigidbody SeonbiBullet;
    public GameObject ragdoll_obj;
    private float Dist;
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

    // Update is called once per frame
    void Update()
    {
        monsterMove();
        NotDamaged();
        DieMonster();
    }

    protected override void monsterMove()
    {
        Dist = Vector3.Distance(transform.position, targetTransform.transform.position);

        base.monsterMove();

        if (!DontMove) //�����߿��� �̵� ��� ����
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

        //���� ��ο��� ��ǥ �������� �����ִ� �Ÿ�
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

    protected override void DieMonster()
    {
        if (curHearth < 1)
        {
            GameObject ThrowRockrigid = Instantiate(ragdoll_obj, transform.position, transform.rotation);

            gameObject.SetActive(false);
            agent.enabled = false;

        }
    }
    IEnumerator attacker()
    {
        DontMove = true;

        animator.SetBool("Move", false);
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(0.9f);

        Vector3 targety = targetTransform.position;
        targety.y = SetY;

        Vector3 target_ = transform.position;
        target_.y = SetY + 0.5f;

        transform.LookAt(targety);

        //Rigidbody ThrowRockrigid = Instantiate(SeonbiBullet, target_, transform.rotation);

        yield return new WaitForSeconds(1.3f);

        animator.SetBool("Attack", false);

        yield return new WaitForSeconds(0.7f);
        isattack = false;
        DontMove = false;
    }
}