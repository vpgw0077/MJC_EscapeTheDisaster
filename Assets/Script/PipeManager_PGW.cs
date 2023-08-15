using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager_PGW : DoorAndBridgeManager_PGW
{

    [SerializeField] private string pipeSound;

    private Interact_PGW thePickUp;
    private BoxCollider theBox;
    private void Awake()
    {
        theBox = GetComponent<BoxCollider>();
        thePickUp = FindObjectOfType<Interact_PGW>();
    }

    private void ConnectPipe(Collider other)
    {
        if (other.CompareTag("Pipe"))
        {
            if (thePickUp.Carrying)
            {
                thePickUp.AutoDrop();
            }
            Rigidbody Piperb = other.GetComponent<Rigidbody>();
            other.transform.position = theBox.transform.position;
            other.transform.rotation = theBox.transform.rotation;
            Piperb.isKinematic = true;
            Piperb.constraints = RigidbodyConstraints.FreezePosition;
            Piperb.freezeRotation = true;
            SoundManager_PGW.instance.PlaySE(pipeSound);

        }
    }

    private void DisConnectPipe(Collider other)
    {
        if (other.CompareTag("Pipe"))
        {
            Rigidbody Piperb = other.GetComponent<Rigidbody>();
            Piperb.constraints = RigidbodyConstraints.None;
            Piperb.isKinematic = false;
            Piperb.freezeRotation = false;
            SoundManager_PGW.instance.PlaySE(pipeSound);

        }
    }
}
