using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown_PGW : MonoBehaviour
{
   [SerializeField] private float torqueForce = 1000f;

    private Rigidbody rb;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Car"))
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddTorque(transform.right * torqueForce);
        }
    }


}
