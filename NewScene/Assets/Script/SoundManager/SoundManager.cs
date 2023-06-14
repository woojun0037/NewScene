using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    static public SoundManager instance;

    public AudioSource audioSourcesEffect;
    public AudioSource audioSourceBgm;

    public string[] PlaySoundName;

    public Sound[] effectSounds;
    public Sound[] bgmSounds;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlaySE(AudioClip audio)
    {
        //for(int i = 0; i < effectSounds.Length; i++)
        //{
        //    if(_name == effectSounds[i].name)
        //    {
        //        for (int j = 0; j < audioSourcesEffects.Length; j++)
        //        {
        //            if (!audioSourcesEffects[j].isPlaying)
        //            {
        //                //audioSourcesEffects[j].clip = effectSounds[i].clip;

        //                return;
        //            }
        //        }
        //        Debug.Log("모든 가용 AudioSource가 사용중입니다");
        //        return;
        //    }
        //}
        //Debug.Log(_name + "사운드가 SoundManager에 등록되지 않았습니다.");
        
        audioSourcesEffect.PlayOneShot(audio);
    }

    public void MonsterSE(AudioClip monSound, AudioSource audioSource)
    {
        audioSourcesEffect.PlayOneShot(monSound);
        //audioSource.clip = monSound;
        //audioSource.Play();
    }
    //public void StopAllSE()
    //{
    //    for(int i=0; i < audioSourcesEffects.Length; i++)
    //    {
    //        audioSourcesEffects[i].Stop();
    //    }
    //}

    //public void StopSE(string _name)
    //{
    //    for(int i=0; i < audioSourcesEffects.Length; i++)
    //    {
    //        if (PlaySoundName[i] == _name)
    //        {
    //            audioSourcesEffects[i].Stop();
    //            return;
    //        }
    //    }
    //    Debug.Log("재생중인" + _name + "사운드가 없습니다.");
    //}
}
