using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPowerPipe_PGW : MovingObject_PGW, ITrigger_PGW
{

    public void Trigger()
    {       
        StopAllCoroutines();
        StartCoroutine(TurnOn(CheckTargetPosition()));
    }

}
