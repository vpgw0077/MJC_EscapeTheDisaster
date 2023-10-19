using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject_PGW : MonoBehaviour
{
     protected Transform currentPosition;

    [SerializeField] protected float speed;
    [SerializeField] protected Transform targetObject;
    [SerializeField] protected Transform targetPosition;
    [SerializeField] protected Transform originPosition;

    [Header("ButtonAudioComponent")]
    [SerializeField] protected AudioSource theAudioSource;
    [SerializeField] protected AudioClip theAudioClip;
 

    private void Awake()
    {
        currentPosition = originPosition;
    }
    protected virtual IEnumerator TurnOn(Transform targetPosition)
    {
     
        while (targetObject.position != targetPosition.position)
        {
            targetObject.position = Vector3.MoveTowards(targetObject.position, targetPosition.position, speed * Time.deltaTime);
            yield return null;

        }

    }
    protected Transform CheckTargetPosition()
    {
        if (currentPosition == originPosition)
        {
            theAudioSource.PlayOneShot(theAudioClip);
            currentPosition = targetPosition;
            return targetPosition;
        }

        currentPosition = originPosition;
        return originPosition;

    }
}
