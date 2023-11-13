using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Main_Player player;
    public Image HPbar;

    bool isDamage = false;
    
    public void HurtPlayer(float damage)
    {
        if (!isDamage)
        {
            player.currentHp -= damage;
            HPbar.fillAmount = player.currentHp / player.HP;
            isDamage = true;
            StartCoroutine(HitPlayerCor());
        }
        isDamage = false;
    }

    IEnumerator HitPlayerCor()
    {
        yield return new WaitForSeconds(1.5f);
        isDamage = false;
    }
}
