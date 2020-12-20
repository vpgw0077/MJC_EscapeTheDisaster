using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_PGW : MonoBehaviour
{
    public GameObject Face; // 캐릭터의 얼굴, Ray가 나오는 위치
    public float InteractDistance;
    public float ThrowForce;
    public GameObject PickPosition; // 오브젝트를 집었을 때 위치
    public bool carrying;
    Vector3 objectPos;
    BoxCollider PickCollision;
    public GameObject carriedObject;
    public LayerMask layermask;

    public string ButtonSound;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        PickCollision = PickPosition.GetComponent<BoxCollider>();
    }


    // Update is called once per frame
    void Update()
    {

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PickPosition"), LayerMask.NameToLayer("Item"), true);
        if (carrying)
        {
            carry(carriedObject);
            TryThrow(); // 던지기
            TryDrop(); // 내려놓기
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Item"), true);

        }
        else
        {
            PickUp(); // 집기
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Item"), false);

        }
    }

    void carry(GameObject o)
    {

        o.transform.position = PickPosition.transform.position;
    }
    void PickUp()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;


            Debug.DrawRay(Face.transform.position, Face.transform.forward, Color.red, InteractDistance);
            if (Physics.Raycast(Face.transform.position, Face.transform.forward, out hit, InteractDistance, layermask))
            {

                PickUpAble_PGW p = hit.collider.GetComponent<PickUpAble_PGW>(); // 레이에 맞은 오브젝트가 PickUpAble_PGW 스크립트 컴포넌트가 있으면 집기실행
                if (p != null)
                {

                    carrying = true;
                    carriedObject = p.gameObject;
                    p.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    p.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    p.GetComponent<Rigidbody>().useGravity = false;
                    p.GetComponent<Rigidbody>().detectCollisions = true;
                    p.transform.SetParent(PickPosition.transform);
                    PickCollision.enabled = true;

                }



                else if (hit.transform.tag == "PipeSwitch")
                {
                    SoundManager_PGW.instance.PlaySE(ButtonSound);
                    hit.transform.GetComponent<RotateObject_PGW>().isActivate = !hit.transform.GetComponent<RotateObject_PGW>().isActivate;


                }
                else if (hit.transform.tag == "DoorSwitch")
                {
                    SoundManager_PGW.instance.PlaySE(ButtonSound);
                    hit.transform.GetComponent<DoorSwitch_PGW>().OpenDoor();
                }
                else if (!hit.transform.GetComponent<DoorSwitch_PGW>().Up && !hit.transform.GetComponent<DoorSwitch_PGW>().Down)
                {

                    if (hit.transform.tag == "EleSwitch")
                    {
                        SoundManager_PGW.instance.PlaySE(ButtonSound);
                        if (hit.transform.GetComponent<DoorSwitch_PGW>().isPowerOn)
                        {
                            if (hit.transform.GetComponent<DoorSwitch_PGW>().ReadyToUp)
                            {
                                hit.transform.GetComponent<DoorSwitch_PGW>().Up = true;
                                hit.transform.GetComponent<DoorSwitch_PGW>().ElevatorState();

                            }
                            else if (hit.transform.GetComponent<DoorSwitch_PGW>().ReadyToDown)
                            {
                                hit.transform.GetComponent<DoorSwitch_PGW>().Down = true;
                                hit.transform.GetComponent<DoorSwitch_PGW>().ElevatorState();

                            }
                        }


                    }
                }




            }
        }
    }

    void TryThrow()
    {
        if (Input.GetMouseButtonDown(0))
        {

            carriedObject.GetComponent<Rigidbody>().useGravity = true;
            carriedObject.GetComponent<Rigidbody>().AddForce(PickPosition.transform.forward * ThrowForce);
            carriedObject.transform.SetParent(null);
            carrying = false;
            carriedObject = null;
            PickCollision.enabled = false;
        }
    }


    void TryDrop()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            objectPos = carriedObject.transform.position;
            carriedObject.transform.SetParent(null);
            carriedObject.GetComponent<Rigidbody>().useGravity = true;
            carriedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            carriedObject.transform.position = objectPos;
            carrying = false;
            carriedObject = null;
            PickCollision.enabled = false;

        }
    }

    public void AutoDrop()
    {
        carriedObject.GetComponent<Rigidbody>().useGravity = true;
        carriedObject.transform.SetParent(null);
        carrying = false;
        carriedObject = null;
        PickCollision.enabled = false;
    }
}

