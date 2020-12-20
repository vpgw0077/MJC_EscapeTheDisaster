using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachWheel_PGW : MonoBehaviour
{
    public GameObject CarWheel;
    public string carSound;

    Interact_PGW theInteract;
    MovingCar_PGW theMovingCar;

    private void Start()
    {
        theInteract = FindObjectOfType<Interact_PGW>();
        theMovingCar = GetComponentInParent<MovingCar_PGW>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wheel")
        {
            if (theInteract.carrying)
            {
                theInteract.AutoDrop();
            }
            SoundManager_PGW.instance.PlaySE(carSound);
            theMovingCar.isReady = true;
            Destroy(other.gameObject);
            CarWheel.SetActive(true);


        }
    }
}
