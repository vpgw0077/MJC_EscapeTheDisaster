using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch_PGW : MonoBehaviour, ITrigger_PGW
{
    [SerializeField] private Animator anim;
    [SerializeField] private string buttonSound;
    [SerializeField] private string doorSound;

    private bool isOpen = false;

    public void Trigger()
    {
        anim.SetTrigger("Generate");
        PlaySound();
        isOpen = true;
    }

    private void PlaySound()
    {
        SoundManager_PGW.instance.PlaySE(buttonSound);

        if (!isOpen)
        {
            SoundManager_PGW.instance.PlaySE(doorSound);
        }
    }
}
