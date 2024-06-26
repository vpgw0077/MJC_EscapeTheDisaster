using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBlockCheck_PGW : MonoBehaviour
{
    private IAirForceControll_PGW theAir;

    private readonly int positiveCoefficient = 1;
    private readonly int negativeCoefficient = -1;

    private void Awake()
    {
        theAir = GetComponentInParent<IAirForceControll_PGW>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rock"))
        {
            theAir.ChangeAirForce(positiveCoefficient);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rock"))
        {
            theAir.ChangeAirForce(negativeCoefficient);
        }
    }


}
