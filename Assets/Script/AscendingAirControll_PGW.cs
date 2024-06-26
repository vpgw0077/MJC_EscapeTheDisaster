using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AscendingAirControll_PGW : MonoBehaviour , IAirForceControll_PGW
{
    [SerializeField] private Vector3 originSize = Vector3.zero;
    [SerializeField] private float increasePowerValue = 0;

    private BoxCollider[] airCollider;
    private AscendingAir_PGW[] childAir;
    private void Awake()
    {

        airCollider = GetComponentsInChildren<BoxCollider>();
        childAir = GetComponentsInChildren<AscendingAir_PGW>();
        for (int i = 0; i < airCollider.Length; i++)
        {
            airCollider[i].size = originSize;
        }

    }

    public void ChangeAirForce(int coefficient)
    {
        for (int i = 0; i < airCollider.Length; i++)
        {
            childAir[i].theAirComponent.airForce += (increasePowerValue * coefficient);
            airCollider[i].size = new Vector3(3, airCollider[i].size.y + (increasePowerValue * coefficient), 3);
        }
    }
}
