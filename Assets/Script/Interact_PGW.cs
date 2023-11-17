using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_PGW : MonoBehaviour
{
    [SerializeField] private Transform rayStartPos = null;
    [SerializeField] private Transform pickPosition = null;
    [SerializeField] private Transform wallRayPos = null;

    [SerializeField] private float interactDistance = 0;
    [SerializeField] private float throwForce = 0;
    [SerializeField] private float objectMoveSpeed = 0;
    [SerializeField] private float maxCarryDistance = 0;

    [SerializeField] private LayerMask layermask;

    private Collider carryObjectCollider;
    private Collider playerCollider;
    private RaycastHit hit;
    private RaycastHit wallHit;
    private Rigidbody carryObjectRigidBody;

    private Vector3 originPos = new Vector3(0f, 2f, 3f);
    private Vector3 changePos = new Vector3(0f, 4f, 0f);

    private GameObject carriedObject;
    public GameObject CarriedObject
    {
        get
        {
            return carriedObject;
        }

        private set
        {
            carriedObject = value;
        }
    }

    private bool carrying;


    public bool Carrying
    {
        get
        {
            return carrying;
        }

        private set
        {
            carrying = value;
        }
    }

    private void Awake()
    {
        pickPosition.localPosition = originPos;
        playerCollider = GetComponent<Collider>();
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(rayStartPos.position, rayStartPos.forward * interactDistance, Color.red);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Carrying)
            {
                TryDrop();

            }

            else if (Physics.Raycast(rayStartPos.position, rayStartPos.forward, out hit, interactDistance, -1,QueryTriggerInteraction.Ignore))
            {
                TryInteract();

            }
        }

        else if (Input.GetMouseButtonDown(0))
        {
            if (Carrying)
            {
                carryObjectRigidBody.AddForce(rayStartPos.forward* throwForce);
                TryDrop();

            }
        }
        CheckInWall();

    }

    private void FixedUpdate()
    {
        if (Carrying)
        {
            AdjustObjectPosition();
        }
    }
    private void CheckInWall()
    {
        if (Physics.Raycast(wallRayPos.position, wallRayPos.forward, out wallHit, 3f, 1, QueryTriggerInteraction.Ignore))
        {
            pickPosition.localPosition = changePos;
        }
        else
        {
            pickPosition.localPosition = originPos;
        }
    }
    private void AdjustObjectPosition()
    {
        Vector3 moveDirection = (pickPosition.transform.position - CarriedObject.transform.position);
        carryObjectRigidBody.AddForce(moveDirection * objectMoveSpeed);
        if (Vector3.Distance(CarriedObject.transform.position, pickPosition.transform.position) > maxCarryDistance)
        {
            TryDrop();
        }
    }

    private void TryInteract()
    {
        PickUpAbleObject_PGW pickUpObject = hit.collider.GetComponent<PickUpAbleObject_PGW>();
        ITrigger_PGW trigger = hit.collider.GetComponent<ITrigger_PGW>();
        if (pickUpObject != null)
        {

            carryObjectRigidBody = pickUpObject.GetComponent<Rigidbody>();
            carryObjectCollider = pickUpObject.GetComponent<Collider>();
            Physics.IgnoreCollision(playerCollider, carryObjectCollider, true);

            Carrying = true;
            carryObjectRigidBody.constraints = RigidbodyConstraints.None;
            carryObjectRigidBody.transform.position = pickPosition.position;
            carryObjectRigidBody.useGravity = false;
            carryObjectRigidBody.velocity = Vector3.zero;
            carryObjectRigidBody.angularVelocity = Vector3.zero;
            carryObjectRigidBody.drag = 20f;
            carryObjectRigidBody.angularDrag = 1f;
            CarriedObject = pickUpObject.gameObject;

        }

        else if (trigger != null)
        {
            trigger.Trigger();
        }

    }

    public void TryDrop()
    {
        Physics.IgnoreCollision(playerCollider, carryObjectCollider, false);
        carryObjectRigidBody.useGravity = true;
        carryObjectRigidBody.drag = 0f;
        carryObjectRigidBody.angularDrag = 0.05f;
        carryObjectRigidBody.velocity = Vector3.zero;
        Carrying = false;
        CarriedObject = null;


    }

}

