using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AirController_PGW : MonoBehaviour
{

    [SerializeField] protected float airForce;
    [SerializeField] protected bool blocked;
    [SerializeField] protected Rigidbody rb;


    protected abstract void OnTriggerEnter(Collider other);

    protected abstract void OnTriggerStay(Collider other);

    protected abstract void OnTriggerExit(Collider other);



}
