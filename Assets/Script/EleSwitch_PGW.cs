using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleSwitch_PGW : MonoBehaviour, ITrigger_PGW
{
    [SerializeField] private Animator anim;
    [SerializeField] private bool isMoving;

    [Header("AudioSource")]
    [SerializeField] private AudioSource buttonAudioSource;
    [SerializeField] private AudioSource elevatorAudioSource;

    [Header("AudioClip")]
    [SerializeField] private AudioClip buttonAudioClip;
    [SerializeField] private AudioClip elevatorAudioClip;
    [SerializeField] private AudioClip liftAudioClip;
 
    public bool isPowerOn = true;

    private void Awake()
    {
        buttonAudioSource.clip = buttonAudioClip;
    }
    private void Update()
    {


        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Up") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f||
            anim.GetCurrentAnimatorStateInfo(0).IsName("Down") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f)
        {
            isMoving = true;
        }

        if (isMoving && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {

            isMoving = false;
            elevatorAudioSource.Stop();
            elevatorAudioSource.clip = liftAudioClip;
            elevatorAudioSource.Play();
        }

    }

    public void Trigger()
    {
        buttonAudioSource.Play();
        if (isMoving) return;

        if (isPowerOn)
        {
            anim.SetTrigger("Generate");
            elevatorAudioSource.clip = elevatorAudioClip;
            elevatorAudioSource.Play();

        }

    }

}
