using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch_PGW : MonoBehaviour
{
    [SerializeField] private GameObject FireEffect = null;

    private bool isOnFire = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("FireZone"))
        {
            FireEffect.SetActive(true);
            isOnFire = true;
        }
    }
}
