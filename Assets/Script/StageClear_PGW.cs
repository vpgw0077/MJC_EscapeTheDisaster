using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear_PGW : MonoBehaviour
{
    public Animator anim;
    public bool isOpen;
    public int dnaCount;
    public int RequireDna;
    public string DoorSound;

    // Update is called once per frame
    private void Update()
    {


    }
    public void CheckDna()
    {
        if (RequireDna == dnaCount)
        {
            OpenDoor();
        }
    }

    public void CloseDoor()
    {
        isOpen = false;
        SoundManager_PGW.instance.PlaySE(DoorSound);
        anim.SetBool("OpenDoor", false);
    }

    private void OpenDoor()
    {
        isOpen = true;
        SoundManager_PGW.instance.PlaySE(DoorSound);
        anim.SetBool("OpenDoor", true);


    }
}
