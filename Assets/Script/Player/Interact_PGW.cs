using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_PGW : MonoBehaviour, IInteract_PGW, IPickUpObject_PGW
{
    [Header("Transform")]
    [SerializeField] private Transform rayStartPos = null;
    [SerializeField] private Transform pickPosition = null;
    [SerializeField] private Transform wallRayPos = null;
    [Space]
    [Header("Values")]
    [SerializeField] private float interactDistance = 7f;
    [SerializeField] private float throwForce = 530f;
    [SerializeField] private float objectMoveSpeed = 500f;
    [SerializeField] private float maxCarryDistance = 5f;

    [SerializeField] private LayerMask layermask;

    private Collider carryObjectCollider;
    private Collider playerCollider;
    private RaycastHit hit;
    private RaycastHit wallHit;
    private Rigidbody carryObjectRigidbody;
    private Vector3 originPos = new Vector3(0f, 2f, 3f);
    private Vector3 changePos = new Vector3(0f, 4f, 0f);

    private InteractUIController_PGW interactUI = null;

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
        interactUI = GetComponent<InteractUIController_PGW>();
    }

    public void TryInteractObject()
    {
        if (Carrying)
        {
            DropObject();

        }


        else if (!Physics.Raycast(rayStartPos.position, rayStartPos.forward, out hit, interactDistance, layermask, QueryTriggerInteraction.Ignore))
        {
            if (Physics.Raycast(rayStartPos.position, rayStartPos.forward, out hit, interactDistance, -1))
            {
                InteractObject();
                PickUpObject();

            }

        }
    }
    public void TryThrowObject()
    {
        if (Carrying)
        {
            DropObject();
            carryObjectRigidbody.AddForce(rayStartPos.forward * throwForce);

        }
    }
    private void Update()
    {
        CheckInWall();
        UpdateInteractUI();
    }
    private void FixedUpdate()
    {
        if (Carrying)
        {
            AdjustObjectPosition();
        }
    }
    private void UpdateInteractUI()
    {

        if (!Physics.Raycast(rayStartPos.position, rayStartPos.forward, out hit, interactDistance, layermask, QueryTriggerInteraction.Ignore))
        {
            if (Physics.Raycast(rayStartPos.position, rayStartPos.forward, out hit, interactDistance, -1) && !Carrying)
            {
                ObjectInfo_PGW objectInfo = hit.transform.GetComponentInParent<ObjectInfo_PGW>();
                if (objectInfo != null)
                {
                    interactUI.ShowInteractUI(objectInfo);
                }

            }
            else
            {
                interactUI.HideInteractUI();

            }

        }
        else
        {
            interactUI.HideInteractUI();

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
        carryObjectRigidbody.AddForce(moveDirection * objectMoveSpeed);
        if (Vector3.Distance(CarriedObject.transform.position, pickPosition.transform.position) > maxCarryDistance)
        {
            DropObject();
        }
    }

    public void InteractObject()
    {
        ITrigger_PGW trigger = hit.collider.GetComponentInParent<ITrigger_PGW>();

        if (trigger != null)
        {
            trigger.Trigger();

        }

    }

    public void PickUpObject()
    {
        PickUpAbleObject_PGW pickUpObject = hit.collider.GetComponentInParent<PickUpAbleObject_PGW>();
        if (pickUpObject != null)
        {

            carryObjectRigidbody = pickUpObject.GetComponentInParent<Rigidbody>();
            carryObjectCollider = pickUpObject.GetComponentInParent<Collider>();
            Physics.IgnoreCollision(playerCollider, carryObjectCollider, true);

            Carrying = true;
            carryObjectRigidbody.constraints = RigidbodyConstraints.None;
            carryObjectRigidbody.transform.position = pickPosition.position;
            carryObjectRigidbody.useGravity = false;
            carryObjectRigidbody.velocity = Vector3.zero;
            carryObjectRigidbody.angularVelocity = Vector3.zero;
            carryObjectRigidbody.drag = 20f;
            carryObjectRigidbody.angularDrag = 1f;
            CarriedObject = pickUpObject.gameObject;

        }

    }

    public void DropObject()
    {
        Physics.IgnoreCollision(playerCollider, carryObjectCollider, false);
        Carrying = false;
        carryObjectRigidbody.useGravity = true;
        carryObjectRigidbody.drag = 0f;
        carryObjectRigidbody.angularDrag = 0.05f;
        carryObjectRigidbody.velocity = Vector3.zero;
        CarriedObject = null;

    }
}

