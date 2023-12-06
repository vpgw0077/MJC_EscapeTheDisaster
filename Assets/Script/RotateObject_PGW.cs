using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject_PGW : MonoBehaviour, ITrigger_PGW
{
    [SerializeField] private AudioSource theAudioSource = null;
    [SerializeField] private AudioClip theAudioClip = null;
    [Header("ObjectAudioComponent")]
    [SerializeField] private AudioSource targetAudioSource = null;
    [SerializeField] private AudioClip targetAudioClip = null;
    [Space]

    [SerializeField] private ButtonPowerPipe_PGW buttonPower = null;
    [SerializeField] private Rigidbody targetObject = null;
    [SerializeField] private float rotSpeed = 0;

    private Vector3 rot;
    private bool isActivate = false;

    private void Awake()
    {
        rot = new Vector3(0, rotSpeed, 0);
        targetAudioSource.clip = targetAudioClip;
    }
    private void Update()
    {
        if (!isActivate)
        {
            targetAudioSource.Stop();
        }
    }
    private void FixedUpdate()
    {
        if (isActivate)
        {
            Quaternion deltaRotation = Quaternion.Euler(rot * Time.fixedDeltaTime);
            targetObject.MoveRotation(targetObject.rotation * deltaRotation);

        }
    }
    public void Trigger()
    {
        theAudioSource.PlayOneShot(theAudioClip);
        if (buttonPower.IsPowerOn)
        {
            targetAudioSource.Play();
            isActivate = !isActivate;
        }
    }

}
