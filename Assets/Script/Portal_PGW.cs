using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_PGW : MonoBehaviour
{
    public GameObject otherPortal;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "White")
        {
            collision.transform.position = otherPortal.transform.position + otherPortal.transform.forward * 2f;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(otherPortal.transform.forward * 1000f);

        }
    }
}
