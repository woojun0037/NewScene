using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster_Tiger : MonoBehaviour
{
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private Main_Player player;
    [SerializeField] private GameObject[] effects;

    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackCoolTime;
    [SerializeField] private float baseAttackDamage;
    [SerializeField] private float dashAttackDamage;

    public SkinnedMeshRenderer mat;

    private GameObject damageEffect;
    private Animator anim;

    private Vector3 tempPos;
    private bool isAttack;
    private bool isMove;
    private bool isDash;

    private void Awake()
    {
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        targetPos = player.transform.position;

        if (Vector3.Distance(targetPos, transform.position) < attackRange) //보스와 캐릭터의 거리가 attackRange 보다 작고 공격중이 아닐 때
        {
            if (Vector3.Distance(targetPos, transform.position) < attackRange / 2)
            {
                if (!isAttack)
                {
                    isAttack = true;
                    anim.SetBool("isWalk", false);
                    BaseAttack();
                }
            }
            else
            {
                if (!isAttack)
                {
                    isAttack = true;
                    anim.SetBool("isWalk", false);
                    AttackChoice();
                }
            }

        }

        if (isMove)
        {
            anim.SetBool("isWalk", isMove);
            Move();
        }

        if (isAttack)
        {
            if (attackCoolTime >= 0)
            {
                attackCoolTime -= Time.deltaTime;
            }
            else
            {
                isAttack = false;
                attackCoolTime = 5f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Main_gangrim" && isDash)
        {
            isDash = false;
            player.GetDamage(dashAttackDamage);
        }
    }

    private void Move()
    {
        Vector3 dir = targetPos - transform.position;
        transform.forward = dir;
        transform.position += dir * Time.deltaTime * moveSpeed;
    }

    private void AttackChoice()
    {
        isMove = false;
        int rand = Random.Range(0, 2);
        if (rand == 0)
            JumpAttack();
        else if (rand == 1)
            DashAttack();

    }

    private void BaseAttack()
    {
        isMove = false;
        player.GetDamage(baseAttackDamage);
        StartCoroutine(BaseAttackCor());
        anim.SetTrigger("BaseAttack");
    }

    private void DashAttack()
    {
        isDash = true;
        StartCoroutine(DashAttackCor());
        anim.SetTrigger("DashAttack");
    }

    private void JumpAttack()
    {
        StartCoroutine(JumpAttackCor());
        anim.SetTrigger("JumpAttack");
    }

    private IEnumerator BaseAttackCor()
    {

        yield return new WaitForSeconds(1.8f);
        isMove = true;
    }

    private IEnumerator DashAttackCor()
    {
        bool isTarget = false;
        float time = 0.3f;
        tempPos = Vector3.zero;

        if (!isTarget)
        {
            tempPos = targetPos;
            isTarget = true;
        }

        Vector3 dir = tempPos - transform.position;
        transform.forward = dir;
        yield return new WaitForSeconds(0.5f);
        GameObject effect = Instantiate(effects[2], transform.position, Quaternion.identity);
        while (time > 0)
        {
            time -= Time.deltaTime;
            transform.position += dir * Time.deltaTime * 5f;
            yield return new WaitForFixedUpdate();
        }


        yield return new WaitForSeconds(0.5f);
        isMove = true;
        isDash = false;
    }

    private IEnumerator JumpAttackCor()
    {
        float time = 2f;
        while (time > 0f)
        {
            time -= 0.05f;
            yield return new WaitForSeconds(0.01f);
            if (time > 1f)
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
        }

        yield return new WaitForSeconds(1f);
        isMove = true;
    }

    public void EffectSpawn(int index)
    {
        GameObject temp = Instantiate(effects[index], new Vector3(transform.position.x, 3f, transform.position.z), Quaternion.identity);
        if (index == 1)
        {
            Vector3 dir = tempPos - transform.position;
            temp.transform.forward = dir * -1;
        }
    }
}