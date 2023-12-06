using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullPipe_PGW : MonoBehaviour
{
    [SerializeField] private AirComponent_PGW theAirComponent;
    [SerializeField] private float rockAirForce = 0f;
    [SerializeField] private bool blocked;

    private Rigidbody rockRigidbody;

    private void Awake()
    {
        theAirComponent.rayLength = 3f;
    }

    private void Update()
    {
        if (Physics.Raycast(theAirComponent.rayStartPosition.position, transform.right, out theAirComponent.hit, theAirComponent.rayLength, -1, QueryTriggerInteraction.Ignore))
        {
            if (theAirComponent.hit.transform.CompareTag("Rock"))
            {
                blocked = true;
            }
        }
        else
        {
            blocked = false;
        }

    }
    private void FixedUpdate()
    {

        if (theAirComponent.objectRigidbody.Count != 0 && !blocked)
        {
            foreach (Rigidbody rb in theAirComponent.objectRigidbody)
            {
                rb.AddForce((transform.position - rb.position).normalized * theAirComponent.airForce, ForceMode.Force);
            }
        }

        if (rockRigidbody != null)
        {
            rockRigidbody.AddForce((transform.position - rockRigidbody.position).normalized * rockAirForce, ForceMode.Force);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (blocked) return;

        if (other.CompareTag("Rock"))
        {
            rockRigidbody = other.GetComponent<Rigidbody>();
            rockRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }
        else if (!other.CompareTag("Rock") && other.GetComponent<Rigidbody>() != null)
        {
            theAirComponent.objectRigidbody.Add(other.GetComponent<Rigidbody>());

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rock"))
        {
            rockRigidbody.constraints = RigidbodyConstraints.None;
            rockRigidbody = null;
        }
        else if (!other.CompareTag("Rock") && other.GetComponent<Rigidbody>() != null)
        {
            theAirComponent.objectRigidbody.Remove(other.GetComponent<Rigidbody>());
        }
    }
}
