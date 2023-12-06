using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCheckPoint_PGW : MonoBehaviour
{
    private CheckPointInfo_PGW checkPointInfo = null;

    private void Awake()
    {
        checkPointInfo = FindObjectOfType<CheckPointInfo_PGW>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkPointInfo.CheckPointUpdate();
            Destroy(gameObject);
        }
    }
}
