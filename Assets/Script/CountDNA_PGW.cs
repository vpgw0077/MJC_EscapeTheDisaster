using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDNA_PGW : MonoBehaviour
{

    private ChangeLampColor_PGW lampColor = null;
    private IUpdateDnaCount_PGW stage = null;

    private readonly int countValue = 1;

    private void Awake()
    {
        stage = GetComponentInParent<IUpdateDnaCount_PGW>();
        lampColor = GetComponent<ChangeLampColor_PGW>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("VirusDNA"))
        {
            stage.UpdateDnaCount(countValue);
            lampColor.ChangeLampColor(lampColor.greenLamp);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("VirusDNA"))
        {
            stage.UpdateDnaCount(-countValue);
            lampColor.ChangeLampColor(lampColor.redLamp);

        }
    }

}
