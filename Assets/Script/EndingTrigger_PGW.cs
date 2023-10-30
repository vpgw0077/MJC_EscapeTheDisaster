using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTrigger_PGW : MonoBehaviour
{
    public enum EndingType
    {
        HappyEnding,
        BadEnding
    }
    [SerializeField] private Animator anim = null;
    [SerializeField] private EndingType endingType = EndingType.BadEnding;

    private CharacterMove_PGW thePlayer;

    private void Awake()
    {
        thePlayer = FindObjectOfType<CharacterMove_PGW>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Vaccine"))
        {
            StartCoroutine(EndingCredit(endingType));
        }
    }

    IEnumerator EndingCredit(EndingType edType)
    {
        anim.SetTrigger("FadeOut");
        thePlayer.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(edType.ToString());
    }

}

