using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZoneSetting_PGW : MonoBehaviour
{
    Respawn_PGW theRespawn;

    private void Start()
    {
        theRespawn = FindObjectOfType<Respawn_PGW>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            theRespawn.Respawn.RemoveAt(0);
            //theRespawn.nextRespawn = ++theRespawn.nextRespawn % theRespawn.Respawn.Count;
            Destroy(gameObject);
        }
    }


}
