using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPipe_PGW : AirController_PGW
{

    private float rayLength;
    private RaycastHit hit;

    [SerializeField] private Transform rayStartPosition;
    [SerializeField] private LayerMask layerMask;

    private void Start()
    {
        rayLength = GetComponent<BoxCollider>().size.x;
    }
    private void Update()
    {
        if (Physics.Raycast(rayStartPosition.position, transform.right, out hit, rayLength, layerMask))
        {
            if (hit.transform.CompareTag("Rock"))
            {
                transform.localPosition = new Vector3((rayLength - Vector3.Distance(rayStartPosition.position, hit.point)) * -1,
                                                                            transform.localPosition.y, transform.localPosition.z);

            }
        }

        else
        {
            transform.localPosition = Vector3.zero;
        }
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(rayStartPosition.position, transform.right * rayLength, Color.red);
    }
    protected override void OnTriggerEnter(Collider other)
    {

        if (!other.transform.CompareTag("Rock"))
        {
            rb = other.GetComponent<Rigidbody>();

        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (!other.transform.CompareTag("Rock"))
        {
            rb = null;
        }
    }

    protected override void OnTriggerStay(Collider other)
    {

        if (rb != null)
        {
            rb.AddForce(transform.right * airForce, ForceMode.Force);

        }
    }

}
