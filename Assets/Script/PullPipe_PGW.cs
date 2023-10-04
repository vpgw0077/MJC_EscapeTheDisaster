using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullPipe_PGW 
{
    /*[SerializeField] private float rockAirForce;
    private Rigidbody rockRigidbody;

    protected override void OnTriggerEnter(Collider other)
    {

        if (other.transform.CompareTag("Rock"))
        {
            rockRigidbody = other.GetComponent<Rigidbody>();
            blocked = true;

        }
        else
        {
            rb = other.GetComponent<Rigidbody>();
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Rock"))
        {
            blocked = false;
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Rock"))
        {
            rockRigidbody.AddForce(transform.up * rockAirForce, ForceMode.Force);
        }

        if (blocked) return;
        if (rb != null)
        {
            rb.AddForce(transform.up * airForce,ForceMode.Force);

        }
    }*/
}
