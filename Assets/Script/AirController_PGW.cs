using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AirController_PGW : MonoBehaviour
{
    [SerializeField] protected bool somethingDetect;
    [SerializeField] protected float airForce;
    [SerializeField] protected Rigidbody rb;

    protected abstract void FixedUpdate();
    protected abstract void OnTriggerEnter(Collider other);

    protected abstract void OnTriggerExit(Collider other);

}
