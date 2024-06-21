using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation_PGW : MonoBehaviour
{
    private Animator anim = null;

    private string flagHorizontal = "Horizontal";
    private string flagVertical = "Vertical";
    private string flagFalling = "Falling";

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayFallingAnimation(bool isFalling)
    {
        anim.SetBool(flagFalling, isFalling);
    }
    public void PlayWalkAnimation(float h, float v)
    {
        anim.SetFloat(flagHorizontal, h);
        anim.SetFloat(flagVertical, v);
    }

}
