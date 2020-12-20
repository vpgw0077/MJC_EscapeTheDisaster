using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown_PGW : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Car")
        {
            rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            rb.AddTorque(transform.right * 1000f);
        }
    }


}
