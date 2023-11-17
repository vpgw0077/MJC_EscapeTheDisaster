using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmPlayer_PGW : MonoBehaviour
{
    [SerializeField] private AudioClip[] Bgms = null;

    private BgmPlayer_PGW instance = null;
    private AudioSource theAudioSource = null;
    private int bgmIndex = 0;
    private void Awake()
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

        theAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayBgm();
    }
    private void Update()
    {
        CheckBgmEnd();
    }
    private void CheckBgmEnd()
    {
        if (!theAudioSource.isPlaying)
        {
            PlayBgm();
        }

    }
    private void PlayBgm()
    {

        if (bgmIndex > Bgms.Length) bgmIndex = 0;
        theAudioSource.PlayOneShot(Bgms[bgmIndex]);
        bgmIndex++;

    }
}
