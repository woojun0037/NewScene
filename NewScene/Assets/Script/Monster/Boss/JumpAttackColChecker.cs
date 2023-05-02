using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttackColChecker : MonoBehaviour
{
    private bool isTouch;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Main_gangrim" && !isTouch)
        {
            isTouch = false;
            collision.gameObject.GetComponent<Main_Player>().GetDamage(5);
        }
    }
}
