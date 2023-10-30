using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear_PGW : MovingObject_PGW
{
    [SerializeField] private bool isOpen = false;
    [SerializeField] private int dnaCount = 0;
    [SerializeField] private int RequireDna = 0;

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
