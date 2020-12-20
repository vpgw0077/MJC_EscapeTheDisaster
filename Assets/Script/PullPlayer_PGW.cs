using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullPlayer_PGW : MonoBehaviour
{
    public bool isBlocked;

    // Start is called before the first frame update

    private void Start()
    {


    }


    private void OnTriggerStay(Collider other)
    {


        if (other.transform.tag == "White" && !isBlocked)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddForce(transform.up * 200f);


        }
        else if (other.transform.tag == "Player" && !isBlocked)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddForce(transform.up * 40f);


        }
        else if (other.transform.tag == "Virus" && !isBlocked)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddForce(transform.up * 40f);


        }
        else if (other.transform.tag == "Rock")
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddForce(transform.up * 200f);
            isBlocked = true;


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Rock")
        {
            isBlocked = false;

        }
    }
}
