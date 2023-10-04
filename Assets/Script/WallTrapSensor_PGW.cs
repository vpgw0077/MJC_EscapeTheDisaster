using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrapSensor_PGW : MonoBehaviour
{
    [SerializeField] private WallTrap_PGW theWallTrap;

    private void OnTriggerStay(Collider other)
    {
        StartCoroutine(theWallTrap.ActivateTrap());
    }

}
