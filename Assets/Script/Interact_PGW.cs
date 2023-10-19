using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_PGW : MonoBehaviour
{
    [SerializeField] private Transform rayStartPos; // 캐릭터의 얼굴, Ray가 나오는 위치
    [SerializeField] private Transform pickPosition; // 오브젝트를 집었을 때 위치

    [SerializeField] private float interactDistance;
    [SerializeField] private float throwForce;
    [SerializeField] private float objectMoveSpeed;
    [SerializeField] private float maxCarryDistance;

    [SerializeField] private LayerMask layermask;


    private Collider carryObjectCollider;
    private Collider playerCollider;
    private RaycastHit hit;
    private Rigidbody carryObjectRigidBody;

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
        playerCollider = GetComponent<Collider>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Carrying)
            {
                TryDrop();

            }

            else if (Physics.Raycast(rayStartPos.position, rayStartPos.forward, out hit, interactDistance, layermask))
            {
                TryInteract();

            }
        }

        else if (Input.GetMouseButtonDown(0))
        {
            if (Carrying)
            {
                carryObjectRigidBody.AddForce(pickPosition.forward * throwForce);
                TryDrop();

            }
        }

        if (Carrying)
        {
            AdjustObjectPosition();
        }

    }


    void AdjustObjectPosition()
    {
        Vector3 moveDirection = (pickPosition.transform.position - CarriedObject.transform.position);
        carryObjectRigidBody.AddForce(moveDirection * objectMoveSpeed);
        if (Vector3.Distance(CarriedObject.transform.position, pickPosition.transform.position) > maxCarryDistance)
        {
            TryDrop();
        }
    }

    void TryInteract()
    {
        PickUpAbleObject_PGW pickUpObject = hit.collider.GetComponent<PickUpAbleObject_PGW>();
        ITrigger_PGW trigger = hit.collider.GetComponent<ITrigger_PGW>();
        if (pickUpObject != null)
        {

            carryObjectRigidBody = pickUpObject.GetComponent<Rigidbody>();
            carryObjectCollider = pickUpObject.GetComponent<Collider>();
            Physics.IgnoreCollision(playerCollider, carryObjectCollider, true);

            Carrying = true;
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

