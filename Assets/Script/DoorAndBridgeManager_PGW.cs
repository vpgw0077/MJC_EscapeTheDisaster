using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAndBridgeManager_PGW : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform targetObject;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private Transform originPosition;

    [Header("AudioComponent")]
    [SerializeField] private AudioSource theAudioSource;
    [SerializeField] private AudioClip theAudioClip;


    protected IEnumerator TurnOn()
    {
        theAudioSource.PlayOneShot(theAudioClip);
        while (targetObject.position != targetPosition.position)
        {
            targetObject.position = Vector3.MoveTowards(targetObject.position, targetPosition.position, speed * Time.deltaTime);
            yield return null;

        }
    }
    protected IEnumerator TurnOff()
    {
        while (targetObject.position != originPosition.position)
        {
            targetObject.position = Vector3.MoveTowards(targetObject.position, originPosition.position, speed * Time.deltaTime);
            yield return null;

        }
    }
}
