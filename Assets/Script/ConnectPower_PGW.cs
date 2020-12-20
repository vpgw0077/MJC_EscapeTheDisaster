using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectPower_PGW : MonoBehaviour
{
    public GameObject Button;
    public bool isConnect = false;
    public string PipeSound;
    Interact_PGW ThePickUp;
    BoxCollider theBox;
    private void Start()
    {
        theBox = GetComponent<BoxCollider>();
        ThePickUp = GameObject.FindGameObjectWithTag("Player").GetComponent<Interact_PGW>();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Pipe")
        {
            SoundManager_PGW.instance.PlaySE(PipeSound);
            Button.GetComponent<DoorSwitch_PGW>().isPowerOn = true;
            isConnect = true;
            if (isConnect)
            {
            ThePickUp.AutoDrop();
            Rigidbody Piperb = other.GetComponent<Rigidbody>();
            other.transform.position = theBox.transform.position;
            other.transform.rotation = theBox.transform.rotation;
            Piperb.isKinematic = true;
            Piperb.constraints = RigidbodyConstraints.FreezePosition;
            Piperb.freezeRotation = true;           
            Button.GetComponent<DoorSwitch_PGW>().enabled = true;
            }

        }



    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Pipe")
        {
            SoundManager_PGW.instance.PlaySE(PipeSound);
            Button.GetComponent<DoorSwitch_PGW>().isPowerOn = false;
            isConnect = false;
            if (!isConnect)
            {

            Rigidbody Piperb = other.GetComponent<Rigidbody>();
            Piperb.constraints = RigidbodyConstraints.None;
            Piperb.isKinematic = false;
            Piperb.freezeRotation = false;
            Button.GetComponent<DoorSwitch_PGW>().enabled = false;

            }
        }
    }


}
