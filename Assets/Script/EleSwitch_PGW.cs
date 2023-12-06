using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleSwitch_PGW : MovingObject_PGW, ITrigger_PGW
{
    [Space]
    [SerializeField] private AudioSource elevatorAudioSource = null;

    [SerializeField] private AudioClip elevatorAudioClip = null;
    [SerializeField] private AudioClip liftAudioClip = null;

    [Space]
    [SerializeField] private ButtonPowerPipe_PGW buttonPower = null;
    [SerializeField] private bool isMoving = false;



    private void Awake()
    {
        currentPosition = originPosition;
        elevatorAudioSource.clip = elevatorAudioClip;
    }


    protected override IEnumerator TurnOn(Transform targetPosition)
    {
        while (targetObject.position != targetPosition.position)
        {
            targetObject.position = Vector3.MoveTowards(targetObject.position, targetPosition.position, speed * Time.deltaTime);
            isMoving = true;
            yield return null;

        }

        elevatorAudioSource.Stop();
        elevatorAudioSource.PlayOneShot(liftAudioClip);
        isMoving = false;

    }
    public void Trigger()
    {
        theAudioSource.PlayOneShot(theAudioClip);
        if (isMoving) return;

        if (buttonPower.IsPowerOn)
        {
            StartCoroutine(TurnOn(CheckTargetPosition()));
            elevatorAudioSource.Play();

        }

    }

}
