using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffectData : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private GameObject[] jumpAttack_effect;

    [SerializeField] private GameObject[] jumpAttack_range;
    [SerializeField] private GameObject[] jumpEffect;

    [SerializeField] private Vector3[] range_transform;

    [SerializeField] float MaxRockYpos = 30;
    [SerializeField] float MinRockYpos = 0;

    private void Awake()
    {
        if(index == 0)
        {
            for (int i = 0; i < jumpAttack_range.Length; i++)
            {
                range_transform[i] = jumpAttack_range[i].transform.position;
            }


            StartCoroutine(JumpAttackRange());
        }
        else if(index == 1)
        {
            StartCoroutine(DashAttackRange());
        }
        
        
    }

    private IEnumerator DashAttackRange()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    private IEnumerator JumpAttackRange()
    {
        for(int i = 0; i < range_transform.Length; i++)
        {
            jumpAttack_range[i].SetActive(true);
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < range_transform.Length; i++)
        {
            jumpAttack_range[i].SetActive(false);
            yield return new WaitForSeconds(0.03f);
        }

        
        StartCoroutine(JumpAttack());

    }

    private IEnumerator JumpAttack()
    {
        float MaxRockYpos = 30;
        while(MaxRockYpos > MinRockYpos)
        {
            MaxRockYpos -= 0.2f;
            yield return new WaitForSeconds(0.005f);
            for(int i = 0; i < jumpAttack_effect.Length; i++)
            {
                jumpAttack_effect[i].SetActive(true);
                jumpAttack_effect[i].transform.position = new Vector3(range_transform[i].x, MaxRockYpos, range_transform[i].z);
            }
        }

        for (int i = 0; i < jumpAttack_effect.Length; i++)
        {
            jumpEffect[i].transform.position = new Vector3(jumpAttack_effect[i].transform.position.x, 1f, jumpAttack_effect[i].transform.position.z);
            jumpEffect[i].SetActive(true);
        }
        yield return new WaitForSeconds(1f);

        Destroy(this.gameObject);
    }
}
