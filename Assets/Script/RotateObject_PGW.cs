using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject_PGW : MonoBehaviour, ITrigger_PGW
{

    [SerializeField] private GameObject targetObject;
    [SerializeField] private float rotSpeed;
    [SerializeField] private string buttonSound;

    private bool isActivate = false;

    private void Update()
    {
        if (isActivate)
        {
            RotateObject();

        }


    }

    private void RotateObject()
    {
        targetObject.transform.Rotate(0, 90 * Time.deltaTime * rotSpeed, 0, Space.World);
    }
    public void Trigger()
    {
        SoundManager_PGW.instance.PlaySE(buttonSound);
        isActivate = !isActivate;
    }

}
