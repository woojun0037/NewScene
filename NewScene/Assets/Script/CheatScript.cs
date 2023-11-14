using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatScript : MonoBehaviour
{
    public static CheatScript instance;
    [SerializeField]GameObject Gangrim;
    [SerializeField]public Image HP;
    [SerializeField]public Main_Player player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        Gangrim = GameObject.Find("Main_gangrim2");
    }

    void Update()
    {
        CheatPad();
    }

    public void CheatPad()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            HP.fillAmount = 1;
            player.currentHp = player.HP;
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            HP.fillAmount = 1;
            player.currentHp = player.HP;
            Gangrim.gameObject.SetActive(true);
        }
    }
}
