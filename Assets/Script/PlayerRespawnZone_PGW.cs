using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnZone_PGW : MonoBehaviour
{
    public List<Transform> respawnZoneList;

    private void Awake()
    {
        var group = GameObject.Find("RespawnZoneGroup");
        if(group != null)
        {
            group.GetComponentsInChildren<Transform>(respawnZoneList);
            respawnZoneList.RemoveAt(0);
        }
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            other.transform.position = respawnZoneList[0].position;
        }
    }

}
