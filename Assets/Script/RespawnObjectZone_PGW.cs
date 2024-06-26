using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObjectZone_PGW : MonoBehaviour
{
    private Interact_PGW theInteract;
    private void Awake()
    {
        theInteract = FindObjectOfType<Interact_PGW>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Respawnable_PGW pickUpObject = other.GetComponent<Respawnable_PGW>();
        if(pickUpObject != null)
        {
            if (ReferenceEquals(theInteract.CarriedObject, pickUpObject.gameObject))
            {
                theInteract.DropObject();
            }
            pickUpObject.RespawnObject();
        }
    }

}
 