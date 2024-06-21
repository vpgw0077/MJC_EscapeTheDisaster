using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSound_PGW : MonoBehaviour
{
    [Header("AudioComponent")]
    [SerializeField] private AudioSource audioPlayer = null;
    [SerializeField] private AudioClip[] walkClip = null;

    private float walkAudioTimer = 0f;
    private float walkAudioPeriod = 0.6f;

    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
    }
    public void PlayWalkSound()
    {

        walkAudioTimer -= Time.deltaTime;

        if (walkAudioTimer <= 0)
        {
            int index = Random.Range(0, walkClip.Length);
            audioPlayer.PlayOneShot(walkClip[index]);
            walkAudioTimer = walkAudioPeriod;
        }


    }
}
