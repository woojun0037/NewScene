using System.Collections;
using UnityEngine;

public class Rabbit_Script : Enemy
{
    public GameObject ragdoll_obj;
    public GameObject attack_Rabbitbody;

    [SerializeField] float SetY;
    [SerializeField] float attackDelaytime;

    public ParticleSystem particle_attack;
    private Animator animator;

    private bool isattack;
    private bool DontMove;
    private float Dist;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        getTouch = true;
        isattack = false;
        DontMove = false;
        animator = GetComponent<Animator>();
        animator.SetBool("Idle", true);

        Vector3 targety = targetTransform.position;
        targety.y = transform.position.y;

        transform.LookAt(targety);
        attack_Rabbitbody.SetActive(false);
    }

    void Update()
    {
        Dist = Vector3.Distance(transform.position, targetTransform.transform.position);

        DieMonster();
        monsterMove();
        NotDamaged();
        CanvasMove();
    }

    protected override void CanvasMove()
    {

        base.CanvasMove();
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

    protected override void GetDamagedAnimation() {
        int random =1;
        random = UnityEngine.Random.Range(1, 3);
        StartCoroutine(damaged_ani(random));
    }

    protected override void monsterMove()
    {
        if (Player.HP >= 0)
        {
            base.monsterMove();

            if (!DontMove)
            {
                animator.SetBool("Move", true);
                if(agent !=null)
                agent.isStopped = false;
            }
            else
            {
                animator.SetBool("Move", false);
                if (agent != null)
                    agent.isStopped = true;
            }

            Vector3 targety = targetTransform.position;
            targety.y = SetY;

            if (Dist <= 2)
            {
                if (!isattack)
                {
                    transform.LookAt(targety);
                    isattack = true;

                    if (gameObject.activeSelf)
                    {
                        StartCoroutine("attacker");
                    }
                }
            }
        }
    }

    IEnumerator attacker()
    {
        DontMove = true;
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(0.9f);
        //getTouch = false;
        attack_Rabbitbody.SetActive(true);
        particle_attack.Play();
        yield return new WaitForSeconds(0.4f);
        attack_Rabbitbody.SetActive(false);
        //getTouch = true;
        animator.SetBool("Attack", false);
        particle_attack.Stop();
        animator.SetBool("Move", false);

        yield return new WaitForSeconds(attackDelaytime + 0.7f);
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
