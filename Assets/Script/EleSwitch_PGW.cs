using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleSwitch_PGW : MonoBehaviour, ITrigger_PGW
{
    private Transform currentPosition;

    [SerializeField] private Transform targetObject;
    [SerializeField] private Transform upperPosition;
    [SerializeField] private Transform downPosition;

    [SerializeField] private float speed;
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
        currentPosition = downPosition;
    }

    private IEnumerator ElevatorOn(Transform targetPosition)
    {
        while (targetObject.position != targetPosition.position)
        {
            targetObject.position = Vector3.MoveTowards(targetObject.position, targetPosition.position, speed * Time.deltaTime);
            isMoving = true;
            yield return null;

        }

        elevatorAudioSource.Stop();
        elevatorAudioSource.clip = liftAudioClip;
        elevatorAudioSource.Play();
        isMoving = false;

    }

    private Transform CheckTargetPosition()
    {
        if (currentPosition == downPosition)
        {
            currentPosition = upperPosition;
            return upperPosition;
        }

        currentPosition = downPosition;
        return downPosition;

    }
    public void Trigger()
    {
        buttonAudioSource.Play();
        if (isMoving) return;

        if (isPowerOn)
        {
            StartCoroutine(ElevatorOn(CheckTargetPosition()));
            elevatorAudioSource.clip = elevatorAudioClip;
            elevatorAudioSource.Play();

        }

    }

}
