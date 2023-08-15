using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPipe_PGW : AirController_PGW
{
    protected override void OnTriggerEnter(Collider other)
    {

        if (other.transform.CompareTag("Rock"))
        {
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
        else
        {
            rb = null;
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        if (blocked) return;

        if (rb != null)
        {
            rb.AddForce(transform.up * airForce,ForceMode.Force);

        }
    }

}
