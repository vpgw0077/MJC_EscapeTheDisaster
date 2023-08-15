using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorFloor_PGW : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.SetParent(gameObject.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }

}
