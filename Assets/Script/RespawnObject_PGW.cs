using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObject_PGW : MonoBehaviour
{
    Interact_PGW theInteract;
    private void Start()
    {
        theInteract = FindObjectOfType<Interact_PGW>();
    }
    private void OnTriggerEnter(Collider other)
    {
        PickUpAble_PGW p = other.GetComponent<PickUpAble_PGW>();
        if(p != null)
        {
            if (theInteract.carrying)
            {
                theInteract.AutoDrop();
            }
            p.RespawnZone();
        }
    }

}
 