using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AscendingAirControll_PGW : MonoBehaviour
{
    [SerializeField] private Vector3 originSize = Vector3.zero;
    [SerializeField] private float increasePowerValue = 0;

    private BoxCollider[] airCollider;
    [SerializeField]private AscendingAir_PGW[] childAir;
    private void Awake()
    {

        airCollider = GetComponentsInChildren<BoxCollider>();
        childAir = GetComponentsInChildren<AscendingAir_PGW>();
        for (int i = 0; i < airCollider.Length; i++)
        {
            airCollider[i].size = originSize;
        }

    }
    public void IncreseHeight()
    {

        for (int i = 0; i < airCollider.Length; i++)
        {
            childAir[i].theAirComponent.airForce += increasePowerValue;
            airCollider[i].size = new Vector3(3, airCollider[i].size.y + increasePowerValue, 3);
        }
    }

    public void DecreseHeight()
    {
        for (int i = 0; i < airCollider.Length; i++)
        {
            childAir[i].theAirComponent.airForce -= increasePowerValue;
            airCollider[i].size = new Vector3(3, airCollider[i].size.y - increasePowerValue, 3);
        }
    }

}
