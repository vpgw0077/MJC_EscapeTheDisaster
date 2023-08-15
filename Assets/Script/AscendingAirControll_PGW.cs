using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AscendingAirControll_PGW : MonoBehaviour
{
    [SerializeField] private Vector3 originSize;
    [SerializeField] private float increasePowerValue;

    private BoxCollider[] airCollider;
    private void Awake()
    {

        airCollider = gameObject.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < transform.childCount; i++)
        {
            airCollider[i].size = originSize;
        }

    }
    public void IncreseHeight()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            airCollider[i].size = new Vector3(1, airCollider[i].size.y + increasePowerValue, 1);
        }
    }

    public void DecreseHeight()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            airCollider[i].size = new Vector3(1, airCollider[i].size.y - increasePowerValue, 1);
        }
    }

}
