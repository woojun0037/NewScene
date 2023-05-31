using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTorandoHit : SkillHit
{
    private PlayerSkill player;

    private void OnEnable()
    {
        if (player == null) player = FindObjectOfType<PlayerSkill>();

        StartCoroutine(Move(player.transform.forward));
    }

    private IEnumerator Move(Vector3 forward)
    {
        float time = 0;
        while(time < 1)
        {
            yield return new WaitForFixedUpdate();
            time += Time.deltaTime;
            this.transform.position += forward * Time.deltaTime * 10f;
        }
        this.gameObject.SetActive(false);

    }
}
