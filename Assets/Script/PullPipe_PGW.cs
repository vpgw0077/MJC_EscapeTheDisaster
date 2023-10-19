using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullPipe_PGW : MonoBehaviour
{
    [SerializeField] private AirComponent_PGW theAirComponent;

    [SerializeField] private float rockAirForce;
    [SerializeField] private float rayLength;

    [SerializeField] private Transform rayStartPosition;
    [SerializeField] private LayerMask layerMask;


    private RaycastHit hit;
    private bool blocked;
    private Rigidbody rockRigidbody;

    private void Awake()
    {
        rayLength = 7f;
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(rayStartPosition.position, transform.right * rayLength, Color.blue);
    }
    private void Update()
    {
        if (Physics.Raycast(rayStartPosition.position, transform.right, out hit, rayLength, layerMask))
        {
            if (hit.transform.CompareTag("Rock"))
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
