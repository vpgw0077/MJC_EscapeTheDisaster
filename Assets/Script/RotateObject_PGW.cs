using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject_PGW : MonoBehaviour, ITrigger_PGW
{
    [SerializeField] private AudioSource theAudioSource;
    [SerializeField] private AudioClip theAudioClip;

    [SerializeField] private ButtonPowerPipe_PGW buttonPower;
    [SerializeField] private Rigidbody targetObject;
    [SerializeField] private float rotSpeed;

    Vector3 rot;
    private bool isActivate = false;

    private void Awake()
    {
        rot = new Vector3(0, rotSpeed, 0);
    }
    /*private void Update()
    {
        if (isActivate)
        {
            RotateObject();

        }

    }*/
    private void FixedUpdate()
    {
        if (isActivate)
        {
        Quaternion deltaRotation = Quaternion.Euler(rot * Time.fixedDeltaTime);
        targetObject.MoveRotation(targetObject.rotation  * deltaRotation);

        }
    }
    private void RotateObject()
    {
        targetObject.transform.Rotate(0, 90 * Time.deltaTime * rotSpeed, 0, Space.World);
    }
    public void Trigger()
    {
        theAudioSource.PlayOneShot(theAudioClip);
        if (buttonPower.IsPowerOn)
        {
            isActivate = !isActivate;
        }
    }

}
