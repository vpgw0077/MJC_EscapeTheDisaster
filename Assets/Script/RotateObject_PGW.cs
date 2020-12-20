using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject_PGW : MonoBehaviour
{

    public GameObject Object;
    public float RotSpeed;
    public bool isActivate = false;

    private void Update()
    {
        if (isActivate)
        {
            Object.transform.Rotate(0, 90 *Time.deltaTime* RotSpeed, 0, Space.World);

        }


    }



}
