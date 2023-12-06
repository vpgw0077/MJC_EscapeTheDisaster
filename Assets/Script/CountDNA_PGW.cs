using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDNA_PGW : MonoBehaviour
{
    [SerializeField] private MeshRenderer doorLamp = null;
    [SerializeField] private Material redLamp = null;
    [SerializeField] private Material greenLamp = null;


    private StageClear_PGW stage;

    private void Awake()
    {
        stage = GetComponentInParent<StageClear_PGW>();
        doorLamp.material = redLamp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("VirusDNA"))
        {
            doorLamp.material = greenLamp;
            stage.IncreaseCount();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("VirusDNA"))
        {
            doorLamp.material = redLamp;
            stage.DecreaseCount();

        }
    }

}
