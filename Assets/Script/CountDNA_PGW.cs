using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDNA_PGW : MonoBehaviour
{
    StageClear_PGW Stage;

    private void Start()
    {
        Stage = GetComponentInParent<StageClear_PGW>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "VirusDNA")
        {
            Stage.dnaCount += 1;
            Stage.CheckDna();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "VirusDNA")
        {
            Stage.dnaCount -= 1;
            if (Stage.isOpen)
            {
                Stage.CloseDoor();
            }
        }
    }

}
