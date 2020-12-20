using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn_PGW : MonoBehaviour
{
    public List<Transform> Respawn;
    public int nextRespawn;

    private void Start()
    {
        var group = GameObject.Find("RespawnZoneGroup");
        if(group != null)
        {
            group.GetComponentsInChildren<Transform>(Respawn);
            Respawn.RemoveAt(0);
        }
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //nextRespawn = ++nextRespawn % Respawn.Count;
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            other.transform.position = Respawn[0].position;
        }
    }

}
