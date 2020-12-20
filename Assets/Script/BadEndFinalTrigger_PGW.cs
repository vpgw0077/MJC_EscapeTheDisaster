using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BadEndFinalTrigger_PGW : MonoBehaviour
{
    public Animator anim;
    CharacterMove_PGW thePlayer;

    private void Start()
    {
        thePlayer = FindObjectOfType<CharacterMove_PGW>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Vaccine")
        {
            StartCoroutine(EndingCredit());
        }
    }

    IEnumerator EndingCredit()
    {
        anim.SetTrigger("FadeOut");
        thePlayer.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("BadEnding");
    }

}

