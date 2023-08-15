using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZoneSetting_PGW : MonoBehaviour
{
    private PlayerRespawnZone_PGW theRespawn;

    private void Awake()
    {
        theRespawn = FindObjectOfType<PlayerRespawnZone_PGW>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            theRespawn.respawnZoneList.RemoveAt(0);
            //theRespawn.nextRespawn = ++theRespawn.nextRespawn % theRespawn.Respawn.Count;
            Destroy(gameObject);
        }
    }


}
