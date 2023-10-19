using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPowerPipe_PGW : MonoBehaviour,ITrigger_PGW
{
    [SerializeField] private bool isPowerOn;

    public bool IsPowerOn
    {
        get
        {
            return isPowerOn;
        }

        private set
        {
            isPowerOn = value;
        }
    }
    public void Trigger()
    {
        IsPowerOn = !IsPowerOn;
    }



}
