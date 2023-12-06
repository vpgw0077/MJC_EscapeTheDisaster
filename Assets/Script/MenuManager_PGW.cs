using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuManager_PGW : MonoBehaviour
{
    public static Action Respawn;
    public static bool isStop = false;
    [SerializeField] private GameObject optionUI = null;
    [SerializeField] private GameObject player = null;


    // Start is called before the first frame update
    void Awake()
    {
        optionUI.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isStop)
            {

                Resume();

            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        optionUI.SetActive(false);
        isStop = false;
        Time.timeScale = 1;


    }
    public void GameQuit()
    {
        Application.Quit();
    }

    public void LoadCheckPoint()
    {
        Respawn();
        Resume();
    }
    private void Pause()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        optionUI.SetActive(true);
        isStop = true;
        Time.timeScale = 0;


    }

}
