using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPipe_PGW : MonoBehaviour
{

    [SerializeField] private AirComponent_PGW theAirComponent;
    [SerializeField] private List<Rigidbody> lightRockrb;

    [Space]
    [SerializeField] private float rockThrowForce;
    [SerializeField] private Transform rayStartPosition;
    [SerializeField] private LayerMask layerMask;


    private float rayLength;
    private RaycastHit hit;
    private Vector3 originTriggerPos;


    private void Awake()
    {
        lightRockrb = new List<Rigidbody>(5);
        originTriggerPos = transform.localPosition;
        rayLength = GetComponent<BoxCollider>().size.x;
    }
    private void Update()
    {
        if (Physics.Raycast(rayStartPosition.position, transform.right, out hit, rayLength, layerMask))
        {
            if (hit.transform.CompareTag("Rock"))
            {
                transform.localPosition = new Vector3(originTriggerPos.x - (rayLength - Vector3.Distance(rayStartPosition.position, hit.point)),
                                                                            transform.localPosition.y, transform.localPosition.z);
            }
        }
        else
        {
            transform.localPosition = originTriggerPos;
        }
    }

    private void FixedUpdate()
    {
        if (theAirComponent.objectRigidbody.Count != 0)
        {
            foreach (Rigidbody rb in theAirComponent.objectRigidbody)
            {
                rb.AddForce(transform.right * theAirComponent.airForce, ForceMode.Force);
            }
        }
        if (lightRockrb.Count != 0)
        {
            foreach (Rigidbody rb in lightRockrb)
            {
                rb.AddForce(transform.right * rockThrowForce, ForceMode.Impulse);
            }
        }

    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(rayStartPosition.position, transform.right * rayLength, Color.red);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (!other.transform.CompareTag("Rock") && !other.transform.CompareTag("LightRock") && other.GetComponent<Rigidbody>() != null)
        {
            theAirComponent.objectRigidbody.Add(other.GetComponent<Rigidbody>());
        }
        else if(other.transform.CompareTag("LightRock") && other.GetComponent<Rigidbody>() != null)
        {
            lightRockrb.Add(other.GetComponent<Rigidbody>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.transform.CompareTag("Rock") && !other.transform.CompareTag("LightRock") && other.GetComponent<Rigidbody>() != null)
        {
            theAirComponent.objectRigidbody.Remove(other.GetComponent<Rigidbody>());

        }

        else if (other.transform.CompareTag("LightRock") && other.GetComponent<Rigidbody>() != null)
        {
            lightRockrb.Remove(other.GetComponent<Rigidbody>());
        }
    }

}
