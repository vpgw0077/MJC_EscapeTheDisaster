using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch_PGW : MonoBehaviour
{
    public Animator anim;
    public bool isPowerOn = true;
    public bool Up;
    public bool ReadyToUp;
    public bool ReadyToDown;
    public bool Down;
    public string LiftSound;
    public string ElevatorSound;

    private void Start()
    {

    }

    private void Update()
    {
        if (!ReadyToDown)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Up"))
            {

                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
                {
                    SoundManager_PGW.instance.StopAllSE();
                    SoundManager_PGW.instance.PlaySE(LiftSound);
                    Up = false;
                    ReadyToUp = false;
                    ReadyToDown = true;
                }
            }

        }
        if (!ReadyToUp)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Down"))
            {
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
                {
                    SoundManager_PGW.instance.StopAllSE();
                    SoundManager_PGW.instance.PlaySE(LiftSound);
                    Down = false;
                    ReadyToUp = true;
                    ReadyToDown = false;
                }
            }

        }


    }
    public void ElevatorState()
    {
        if (Up && ReadyToUp)
        {
            anim.SetBool("Up", true);
            SoundManager_PGW.instance.PlaySE(ElevatorSound);


        }
        if (Down && ReadyToDown)
        {
            anim.SetBool("Up", false);
            SoundManager_PGW.instance.PlaySE(ElevatorSound);

        }
    }

    public void OpenDoor()
    {
        anim.SetBool("OpenDoor", true);
    }


}
