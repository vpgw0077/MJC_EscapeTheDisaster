using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interact_PGW : MonoBehaviour
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
    [Space]
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI InteractText = null;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Carrying)
            {
                TryDrop();

            }


            else if (!Physics.Raycast(rayStartPos.position, rayStartPos.forward, out hit, interactDistance, layermask, QueryTriggerInteraction.Ignore))
            {
                if (Physics.Raycast(rayStartPos.position, rayStartPos.forward, out hit, interactDistance, -1))
                {
                    TryInteract();

                }

            }
        }

        else if (Input.GetMouseButtonDown(0))
        {
            if (Carrying)
            {
                carryObjectRigidBody.AddForce(rayStartPos.forward * throwForce);
                TryDrop();

            }
        }
        CheckInWall();
        ShowInteractUI();
    }
    private void FixedUpdate()
    {
        if (Carrying)
        {
            AdjustObjectPosition();
        }
    }
    private void ShowInteractUI()
    {

        if (!Physics.Raycast(rayStartPos.position, rayStartPos.forward, out hit, interactDistance, layermask, QueryTriggerInteraction.Ignore))
        {
            if (Physics.Raycast(rayStartPos.position, rayStartPos.forward, out hit, interactDistance, -1) && !Carrying)
            {
                InteractText.gameObject.SetActive(true);
                ObjectInfo_PGW objectInfo = hit.transform.GetComponentInParent<ObjectInfo_PGW>();
                if (objectInfo != null)
                {
                    InteractText.text = objectInfo.ObjectName + "<color=yellow>" + "(E)" + "</color>";
                }

            }
            else
            {
                InteractText.text = "";
                InteractText.gameObject.SetActive(false);

            }

        }
        else
        {
            InteractText.text = "";
            InteractText.gameObject.SetActive(false);

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
        PickUpAbleObject_PGW pickUpObject = hit.collider.GetComponentInParent<PickUpAbleObject_PGW>();
        ITrigger_PGW trigger = hit.collider.GetComponentInParent<ITrigger_PGW>();
        if (pickUpObject != null)
        {

            carryObjectRigidBody = pickUpObject.GetComponentInParent<Rigidbody>();
            carryObjectCollider = pickUpObject.GetComponentInParent<Collider>();
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
        Carrying = false;
        carryObjectRigidBody.useGravity = true;
        carryObjectRigidBody.drag = 0f;
        carryObjectRigidBody.angularDrag = 0.05f;
        carryObjectRigidBody.velocity = Vector3.zero;
        CarriedObject = null;


    }

}

