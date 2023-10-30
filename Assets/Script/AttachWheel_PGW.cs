using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachWheel_PGW : MonoBehaviour
{
    [SerializeField] private GameObject carWheel = null;

    private Interact_PGW theInteract;
    private MovingCar_PGW theMovingCar;

    private void Awake()
    {
        theInteract = FindObjectOfType<Interact_PGW>();
        theMovingCar = GetComponentInParent<MovingCar_PGW>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wheel"))
        {
            if (theInteract.Carrying)
            {
                theInteract.TryDrop();
            }
            Destroy(other.gameObject);
            theMovingCar.StartEngine();
            carWheel.SetActive(true);


        }
    }
}
