using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AirControll_PGW : MonoBehaviour
{
    public float Height;
    public float AfterHeight;

    public int BlockCount;

   


    BoxCollider[] AirCollider;
    PlayerAir_PGW[] thePlayerAir;
    public Vector3 originsize;
    private void Start()
    {

        AirCollider = gameObject.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < transform.childCount; i++)
        {
            AirCollider[i].size = originsize;
            Height = AirCollider[i].size.y;
        }

    }
    private void Update()
    {

    }
    public void IncreseHeight()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            AirCollider[i].size = new Vector3(1, Height + (30 * BlockCount), 1);
            AfterHeight = AirCollider[i].size.y;
        }
    }

    public void DecreseHeight()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            AirCollider[i].size = new Vector3(1, AfterHeight - 30  , 1);
           // AfterHeight = AirCollider[i].size.y;
        }
    }

}
