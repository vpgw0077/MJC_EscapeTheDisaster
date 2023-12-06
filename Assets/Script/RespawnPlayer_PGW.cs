using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer_PGW : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            other.transform.position = CheckPointInfo_PGW.currentCheckPoint.position;
        }
    }
}
