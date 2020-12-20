using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAble_PGW : MonoBehaviour
{

    public Vector3 OriginPos;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        OriginPos = transform.position;
    }

    public void RespawnZone()
    {
        rb.velocity = Vector3.zero;
        transform.position = OriginPos;
    }

  
}
