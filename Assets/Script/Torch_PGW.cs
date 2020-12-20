using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch_PGW : MonoBehaviour
{
    public GameObject FireEffect;
    public bool isOnFire = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "FireZone")
        {
            FireEffect.SetActive(true);
            isOnFire = true;
        }
        if(isOnFire && other.tag == "Barrel")
        {
            other.gameObject.GetComponent<Explosion_PGW>().StartCoroutine("BoomWall");
        }
    }
}
