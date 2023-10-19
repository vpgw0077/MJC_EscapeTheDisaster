using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear_PGW : MovingObject_PGW
{
    [SerializeField] private bool isOpen;
    [SerializeField] private int dnaCount;
    [SerializeField] private int RequireDna;

    public void IncreaseCount()
    {
        dnaCount++;
        if (RequireDna == dnaCount)
        {
            OpenDoor();
        }

    }
    public void DecreaseCount()
    {
        dnaCount--;
        if (isOpen)
        {
            CloseDoor();
        }

    }

    private void OpenDoor()
    {
        isOpen = true;
        StopAllCoroutines();
        StartCoroutine(TurnOn(CheckTargetPosition()));

    }
    private void CloseDoor()
    {
        isOpen = false;
        StopAllCoroutines();
        StartCoroutine(TurnOn(CheckTargetPosition()));
    }

}
