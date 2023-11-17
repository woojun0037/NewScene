using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatScript : MonoBehaviour
{
    public static CheatScript instance;
    [SerializeField] GameObject Gangrim;
    [SerializeField] public Image HP;
    public Main_Player player;
    public HitScript hit;

    CameraMovemant cameSensitivity;

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
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            hit.damage = 100f;
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            cameSensitivity =FindObjectOfType<CameraMovemant>();
            cameSensitivity.sensitivity = 200;
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            cameSensitivity = FindObjectOfType<CameraMovemant>();
            cameSensitivity.sensitivity = 400;
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            cameSensitivity = FindObjectOfType<CameraMovemant>();
            cameSensitivity.sensitivity += 10;
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            cameSensitivity = FindObjectOfType<CameraMovemant>();
            cameSensitivity.sensitivity -= 10;
        }
    }
}