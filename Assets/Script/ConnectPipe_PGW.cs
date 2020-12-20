using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectPipe_PGW : MonoBehaviour
{

    public enum ToolType
    {
        Door,
        Bridge
    }

    public ToolType tool;

    public Animator Anim;
    public bool isConnect = false;
    public string PipeSound;
    Interact_PGW ThePickUp;
    BoxCollider theBox;
    private void Start()
    {

        theBox = GetComponent<BoxCollider>();
        ThePickUp = FindObjectOfType<Interact_PGW>();
       
    }

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Pipe")
        {
            Rigidbody Piperb = other.GetComponent<Rigidbody>();
            other.transform.position = theBox.transform.position;
            other.transform.rotation = theBox.transform.rotation;
            Piperb.isKinematic = true;
            Piperb.constraints = RigidbodyConstraints.FreezePosition;
            Piperb.freezeRotation = true;
            isConnect = true;
            SoundManager_PGW.instance.PlaySE(PipeSound);

            if (ThePickUp.carrying)
            {

            ThePickUp.AutoDrop();

            }
            if (tool == ToolType.Door)
            {
                OpenDoor();

            }
            if (tool == ToolType.Bridge)
            {
                BridgeOn();
            }


        }



    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Pipe")
        {
            SoundManager_PGW.instance.PlaySE(PipeSound);
            Rigidbody Piperb = other.GetComponent<Rigidbody>();
            Piperb.constraints = RigidbodyConstraints.None;
            Piperb.isKinematic = false;
            Piperb.freezeRotation = false;
            isConnect = false;
            if (tool == ToolType.Door)
            {
                CloseDoor();
            }
            if (tool == ToolType.Bridge)
            {
                BridgeOff();
            }



        }
    }

    void OpenDoor()
    {
        Anim.SetBool("OpenDoor", true);
    }

    void CloseDoor()
    {
        Anim.SetBool("OpenDoor", false);
    }

    void BridgeOn()
    {
        Anim.SetBool("BridgeOn", true);
    }
    void BridgeOff()
    {
        Anim.SetBool("BridgeOn", false);
    }
}
