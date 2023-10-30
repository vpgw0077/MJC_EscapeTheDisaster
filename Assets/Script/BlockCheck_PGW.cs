using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCheck_PGW : MonoBehaviour
{
    private AscendingAirControll_PGW theAir;

    private void Awake()
    {
        theAir = GetComponentInParent<AscendingAirControll_PGW>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rock"))
        {
            theAir.IncreseHeight();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rock"))
        {
            theAir.DecreseHeight();
        }
    }


}
