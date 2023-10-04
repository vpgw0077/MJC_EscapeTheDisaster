using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AscendingAir_PGW : AirController_PGW
{
    private bool blocked;
    private AscendingAirControll_PGW theAir;

    private void Awake()
    {
        theAir = transform.GetComponentInParent<AscendingAirControll_PGW>();
    }

    protected override void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Rock"))
        {
            blocked = true;
            theAir.IncreseHeight();
        }

        else if (other.CompareTag("Player"))
        {
            rb = other.GetComponent<Rigidbody>();

        }

    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rock"))
        {
            blocked = false;
            theAir.DecreseHeight();

        }
    }
    protected override void OnTriggerStay(Collider other)
    {
        if (blocked) return;

        if (other.CompareTag("Player"))
        {
            rb.AddForce(transform.up * airForce,ForceMode.Force);

        }
    }


}
