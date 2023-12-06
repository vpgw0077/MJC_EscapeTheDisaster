using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AirComponent_PGW 
{
    public float airForce;
    public float rayLength;
    public Transform rayStartPosition;
    public List<Rigidbody> objectRigidbody;
    public RaycastHit hit;
}
