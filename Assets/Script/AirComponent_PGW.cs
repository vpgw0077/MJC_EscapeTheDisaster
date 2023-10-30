using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AirComponent_PGW 
{
    [HideInInspector] public int layerMask;
    public float airForce;
    public float rayLength;
    public Transform rayStartPosition;
    public List<Rigidbody> objectRigidbody;
    public RaycastHit hit;
}
