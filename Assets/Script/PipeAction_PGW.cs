using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PipeAction_PGW : MonoBehaviour
{

    [SerializeField] private AudioSource pipeAudioSource = null;
    [SerializeField] private AudioClip pipeAudioClip = null;

    private Rigidbody rb;
    private Interact_PGW dropObject;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        dropObject = FindObjectOfType<Interact_PGW>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PipeZone"))
        {

            if (dropObject.Carrying && ReferenceEquals(dropObject.CarriedObject, gameObject))
            {
                dropObject.DropObject();

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
