using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDNA_PGW : MonoBehaviour
{
    private StageClear_PGW stage;

    private void Awake()
    {
        stage = GetComponentInParent<StageClear_PGW>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("VirusDNA"))
        {
            stage.IncreaseCount();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("VirusDNA"))
        {
            stage.DecreaseCount();

        }
    }

}
