using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPipe_PGW : MonoBehaviour
{

    [SerializeField] private AirComponent_PGW theAirComponent;
    [SerializeField] private List<Rigidbody> lightRockrb;

    [Space]
    [SerializeField] private float knockBackDuration = 0f;
    [SerializeField] private float rockThrowForce = 0f;

    private Vector3 originTriggerPos;


    private void Awake()
    {
        lightRockrb = new List<Rigidbody>(5);
        originTriggerPos = transform.localPosition;
        theAirComponent.rayLength = GetComponent<BoxCollider>().size.x;
    }
    private void Update()
    {
        if (Physics.Raycast(theAirComponent.rayStartPosition.position, transform.right, out theAirComponent.hit, theAirComponent.rayLength, -1, QueryTriggerInteraction.Ignore))
        {
            if (theAirComponent.hit.transform.CompareTag("Rock"))
            {
                transform.localPosition = new Vector3(originTriggerPos.x - (theAirComponent.rayLength - Vector3.Distance(theAirComponent.rayStartPosition.position, theAirComponent.hit.point)),
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
    private void OnTriggerEnter(Collider other)
    {

        if (!other.transform.CompareTag("Rock") && !other.transform.CompareTag("LightRock") && other.GetComponent<Rigidbody>() != null)
        {
            if (other.CompareTag("Player"))
            {
                IState_PGW<CharacterController_PGW.playerState> state = other.GetComponent<IState_PGW<CharacterController_PGW.playerState>>();
                StartCoroutine(state.ChangeState(CharacterController_PGW.playerState.OutOfControl, 0));

            }
            theAirComponent.objectRigidbody.Add(other.GetComponent<Rigidbody>());
        }
        else if (other.transform.CompareTag("LightRock") && other.GetComponent<Rigidbody>() != null)
        {
            lightRockrb.Add(other.GetComponent<Rigidbody>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.transform.CompareTag("Rock") && !other.transform.CompareTag("LightRock") && other.GetComponent<Rigidbody>() != null)
        {
            if (other.CompareTag("Player"))
            {
                IState_PGW<CharacterController_PGW.playerState> state = other.GetComponent<IState_PGW<CharacterController_PGW.playerState>>();
                StartCoroutine(state.ChangeState(CharacterController_PGW.playerState.Controlable, knockBackDuration));

            }
            theAirComponent.objectRigidbody.Remove(other.GetComponent<Rigidbody>());

        }

        else if (other.transform.CompareTag("LightRock") && other.GetComponent<Rigidbody>() != null)
        {
            lightRockrb.Remove(other.GetComponent<Rigidbody>());
        }
    }

}
