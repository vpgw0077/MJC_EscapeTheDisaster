using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBlockZone_PGW : MonoBehaviour
{
    private Interact_PGW theInteract;
    private void Awake()
    {
        theInteract = FindObjectOfType<Interact_PGW>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Respawnable_PGW pickUpObject = other.GetComponent<Respawnable_PGW>();
        if (pickUpObject != null && other.CompareTag("White"))
        {
            if (theInteract.Carrying)
            {
                theInteract.TryDrop();
            }
            pickUpObject.RespawnObject();
        }
    }
}
