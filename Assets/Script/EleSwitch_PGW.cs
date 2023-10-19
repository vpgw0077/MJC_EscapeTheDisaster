using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleSwitch_PGW : MovingObject_PGW, ITrigger_PGW
{
    [Space]
    [SerializeField] private AudioSource elevatorAudioSource;

    [SerializeField] private AudioClip elevatorAudioClip;
    [SerializeField] private AudioClip liftAudioClip;

    [Space]
    [SerializeField] private ButtonPowerPipe_PGW buttonPower;
    [SerializeField] private bool isMoving;



    private void Awake()
    {
        currentPosition = originPosition;
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
            elevatorAudioSource.PlayOneShot(elevatorAudioClip);

        }

    }

}
