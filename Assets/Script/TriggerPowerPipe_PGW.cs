using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPowerPipe_PGW : DoorAndBridgeManager_PGW
{
    [SerializeField] private AudioSource pipeAudioSource;
    [SerializeField] private AudioClip pipeAudioClip;

    private Interact_PGW thePickUp;
    private BoxCollider theBox;

    
    private void Awake()
    {
        theBox = GetComponent<BoxCollider>();
        thePickUp = FindObjectOfType<Interact_PGW>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pipe"))
        {
            if (thePickUp.Carrying)
            {
                thePickUp.AutoDrop();
            }
            Rigidbody Piperb = other.GetComponent<Rigidbody>();
            other.transform.position = theBox.transform.position;
            other.transform.rotation = theBox.transform.rotation;
            Piperb.constraints = RigidbodyConstraints.FreezePosition;
            Piperb.freezeRotation = true;

            pipeAudioSource.PlayOneShot(pipeAudioClip);
            StopAllCoroutines();
            StartCoroutine("TurnOn");

        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pipe"))
        {
            Rigidbody Piperb = other.GetComponent<Rigidbody>();
            Piperb.constraints = RigidbodyConstraints.None;
            Piperb.isKinematic = false;
            Piperb.freezeRotation = false;

            pipeAudioSource.PlayOneShot(pipeAudioClip);
            StopAllCoroutines();
            StartCoroutine("TurnOff");

        }

    }
}
