using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAir_PGW : MonoBehaviour
{
    public float AirForce;
    public bool Blocked;

    public bool MaxCount;
    public int CurrentCount = 10000;
    public int MinCount;


    bool CanFly;
    Rigidbody rb;
    AirControll_PGW theAir;
    // Start is called before the first frame update
    private void Start()
    {

        theAir = this.gameObject.transform.parent.gameObject.GetComponentInParent<AirControll_PGW>();
        // rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Rock")
        {

            PlayerFly(false);
            Blocked = true;
            theAir.BlockCount++;
            theAir.IncreseHeight();

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Rock")
        {
            Blocked = false;
            theAir.BlockCount--;
            theAir.DecreseHeight();



        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
            if (Blocked == false)
            {
                PlayerFly(true);
                if (CanFly)
                {
                    rb.AddForce(Vector3.up * AirForce);
                }

            }


        }
    }



    void PlayerFly(bool canfly)
    {

        CanFly = canfly;


    }

}
