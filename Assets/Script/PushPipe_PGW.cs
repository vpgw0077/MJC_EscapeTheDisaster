using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPipe_PGW : MonoBehaviour
{
    public bool isBlocked;

    // Start is called before the first frame update

    private void Start()
    {

        //rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }


    private void OnTriggerStay(Collider other)
    {

        if (other.transform.tag == "Player"&& !isBlocked || other.transform.tag == "White" &&!isBlocked || other.transform.tag == "VirusDNA" && !isBlocked
            || other.transform.tag == "Pipe" && !isBlocked || other.transform.tag == "Virus" &&!isBlocked)
        {
            Rigidbody RB = other.GetComponent<Rigidbody>();
            RB.AddForce(transform.up * 1.5f, ForceMode.Impulse);
           // rb.AddForce(transform.up * 100f);

        }
        else if (other.transform.tag == "Rock")
        {

            isBlocked = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Virus" || other.transform.tag == "Rock")
        {

            isBlocked = false;

        }
    }


}
