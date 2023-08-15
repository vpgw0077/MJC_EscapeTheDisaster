using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}
public class SoundManager_PGW : MonoBehaviour
{
    public static SoundManager_PGW instance;

    public AudioSource[] audioSourceEffect;

    public Sound[] EffectSounds;

    public string[] playSoundName;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {

        playSoundName = new string[audioSourceEffect.Length];
    }


    public void PlaySE(string _name)
    {
        for (int i = 0; i < EffectSounds.Length; i++)
        {
            if (_name == EffectSounds[i].name)
            {
                for (int j = 0; j < audioSourceEffect.Length; j++)
                {
                    if (!audioSourceEffect[j].isPlaying)
                    {
                        playSoundName[j] = EffectSounds[i].name;
                        audioSourceEffect[j].clip = EffectSounds[i].clip;
                        audioSourceEffect[j].Play();
                        return;
                    }
                }
                return;
            }
        }

    }

    public void StopAllSE()
    {
        for (int i = 0; i < audioSourceEffect.Length; i++)
        {
            audioSourceEffect[i].Stop();
        }
    }

    public void StopSE(string _name)
    {
        for (int i = 0; i < audioSourceEffect.Length; i++)
        {
            if (playSoundName[i] == _name)
            {
                audioSourceEffect[i].Stop();
                return;

            }
        }
    }

   


}
