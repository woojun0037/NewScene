using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSound : MonoBehaviour
{

    public static MapSound Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioClip[] forestmapSound;
    private AudioSource[] mapSoundSource;


    private bool isWalking = false;

    private void Start()
    {
        mapSoundSource = new AudioSource[forestmapSound.Length];

        for (int i = 0; i < forestmapSound.Length; i++)
        {
            mapSoundSource[i] = SoundManager.Instance.SFXPlayLoop("Walk" + i, forestmapSound[i]);
            mapSoundSource[i].Play();
        }


    }

    private void Update()
    {

    }

    // ���� �ൿ �Ŀ� ȣ��� �޼���
    private void Attack()
    {
        //SoundManager.Instance.SFXPlay("Attack", attackSound);
    }
}
