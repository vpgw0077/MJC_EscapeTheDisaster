using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AscendingAir_PGW : MonoBehaviour
{
    [SerializeField] private bool blocked;

    public AirComponent_PGW theAirComponent;


    private void Awake()
    {
        theAirComponent.rayLength = 2f;
        theAirComponent.layerMask = 1 << 11;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(theAirComponent.rayStartPosition.position, transform.up * theAirComponent.hit.distance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(theAirComponent.rayStartPosition.position + transform.up * theAirComponent.hit.distance, 0.5f);
    }
    private void Update()
    {
        if (Physics.SphereCast(theAirComponent.rayStartPosition.position, 0.5f, transform.up ,out theAirComponent.hit, theAirComponent.rayLength, theAirComponent.layerMask, QueryTriggerInteraction.Ignore))
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
                rb.AddForce(transform.up * theAirComponent.airForce + transform.forward * 2.5f, ForceMode.Force);
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Rock") && other.GetComponent<Rigidbody>() != null && !blocked)
        {
            theAirComponent.objectRigidbody.Add(other.GetComponent<Rigidbody>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Rock") && other.GetComponent<Rigidbody>() != null)
        {
            theAirComponent.objectRigidbody.Remove(other.GetComponent<Rigidbody>());
        }


    }
}
