using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeAction_PGW : MonoBehaviour
{

    [SerializeField] private AudioSource pipeAudioSource;
    [SerializeField] private AudioClip pipeAudioClip;

    private Rigidbody rb;
    private Interact_PGW thePickUp;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        thePickUp = FindObjectOfType<Interact_PGW>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PipeZone"))
        {
            if (thePickUp.Carrying)
            {
                thePickUp.TryDrop();
            }
            gameObject.transform.position = other.transform.position;
            gameObject.transform.rotation = other.transform.rotation;
            rb.constraints = RigidbodyConstraints.FreezeAll;

            pipeAudioSource.PlayOneShot(pipeAudioClip);
            ITrigger_PGW pipeTrigger = other.GetComponent<ITrigger_PGW>();
            if (pipeTrigger != null)
            {
                pipeTrigger.Trigger();

            }

        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PipeZone"))
        {
            rb.constraints = RigidbodyConstraints.None;

            pipeAudioSource.PlayOneShot(pipeAudioClip);
            ITrigger_PGW pipeTrigger = other.GetComponent<ITrigger_PGW>();
            if (pipeTrigger != null)
            {
                pipeTrigger.Trigger();

            }

        }

    }
}
