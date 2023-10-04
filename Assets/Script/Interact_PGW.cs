using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_PGW : MonoBehaviour
{
    [SerializeField] private Transform rayStartPos; // 캐릭터의 얼굴, Ray가 나오는 위치
    [SerializeField] private Transform pickPosition; // 오브젝트를 집었을 때 위치
    [SerializeField] private float interactDistance;
    [SerializeField] private float throwForce;
    [SerializeField] private LayerMask layermask;

    private Rigidbody carryObjectRigidBody;
    private Vector3 objectPos;
    private BoxCollider pickCollision;
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


    // Start is called before the first frame update
    void Awake()
    {
        pickCollision = pickPosition.GetComponent<BoxCollider>();
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PickPosition"), LayerMask.NameToLayer("Item"), true);
    }


    // Update is called once per frame
    void Update()
    {


        if (Carrying)
        {
            carry(CarriedObject);
            TryThrow(); // 던지기
            TryDrop(); // 내려놓기
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Item"), true);

        }
        else
        {
            Interact();
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Item"), false);

        }
    }

    void carry(GameObject carryObject)
    {

        carryObject.transform.position = pickPosition.position;
    }
    void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;


            if (Physics.Raycast(rayStartPos.position, rayStartPos.forward, out hit, interactDistance, layermask))
            {

                PickUpAbleObject_PGW pickUpObject = hit.collider.GetComponent<PickUpAbleObject_PGW>();
                ITrigger_PGW trigger = hit.collider.GetComponent<ITrigger_PGW>();
                if (pickUpObject != null)
                {

                    carryObjectRigidBody = pickUpObject.GetComponent<Rigidbody>();

                    Carrying = true;
                    CarriedObject = pickUpObject.gameObject;
                    carryObjectRigidBody.velocity = Vector3.zero;
                    carryObjectRigidBody.angularVelocity = Vector3.zero;
                    carryObjectRigidBody.useGravity = false;
                    carryObjectRigidBody.constraints = RigidbodyConstraints.FreezeAll;
                    carryObjectRigidBody.transform.SetParent(pickPosition);
                    pickCollision.enabled = true;

                }

                else if(trigger!= null)
                {
                    trigger.Trigger();
                }

            

            }
        }
    }

    void TryThrow()
    {
        if (Input.GetMouseButtonDown(0))
        {

            carryObjectRigidBody.constraints = RigidbodyConstraints.None;
            carryObjectRigidBody.useGravity = true;
            carryObjectRigidBody.AddForce(pickPosition.forward * throwForce);
            CarriedObject.transform.SetParent(null);
            Carrying = false;
            CarriedObject = null;
            pickCollision.enabled = false;
        }
    }


    void TryDrop()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            carryObjectRigidBody.constraints = RigidbodyConstraints.None;
            //objectPos = carriedObject.transform.position;
            CarriedObject.transform.SetParent(null);
            carryObjectRigidBody.useGravity = true;
            carryObjectRigidBody.velocity = Vector3.zero;
            //carriedObject.transform.position = objectPos;
            Carrying = false;
            CarriedObject = null;
            pickCollision.enabled = false;

        }
    }

    public void AutoDrop()
    {
        carryObjectRigidBody.constraints = RigidbodyConstraints.None;
        carryObjectRigidBody.useGravity = true;
        CarriedObject.transform.SetParent(null);
        CarriedObject = null;
        Carrying = false;
        pickCollision.enabled = false;
    }
}

