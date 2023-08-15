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
        PickUpAbleObject_PGW pickUpObject = other.GetComponent<PickUpAbleObject_PGW>();
        if(pickUpObject != null)
        {
            if (ReferenceEquals(theInteract.CarriedObject, pickUpObject.gameObject))
            {
                theInteract.AutoDrop();
            }
            pickUpObject.RespawnObject();
        }
    }

}
 