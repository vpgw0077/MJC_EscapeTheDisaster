using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnable_PGW : MonoBehaviour
{
    private Quaternion originRotation;
    private Vector3 originPos;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originPos = transform.position;
        originRotation = transform.rotation;
    }

    public void RespawnObject()
    {
        rb.velocity = Vector3.zero;
        transform.position = originPos;
        transform.rotation = originRotation;
    }
}
