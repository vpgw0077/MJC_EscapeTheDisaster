using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager_PGW : MonoBehaviour
{
    public GameObject TitleUI;
    public GameObject HowToUI;


    public void GameStart()
    {
        SceneManager.LoadScene("ProtoTypeStage");
    }

    public void GotoTitle()
    {
        SceneManager.LoadScene("Intro");

    }

    public void HowTo()
    {
        TitleUI.SetActive(false);
        HowToUI.SetActive(true);

    }

    public void Back()
    {
        TitleUI.SetActive(true);
        HowToUI.SetActive(false);
    }

}
