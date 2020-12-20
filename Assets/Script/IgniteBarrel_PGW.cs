﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgniteBarrel_PGW : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Barrel")
        {
            other.gameObject.GetComponent<Explosion_PGW>().StartCoroutine("BoomWall");
        }
    }

}
