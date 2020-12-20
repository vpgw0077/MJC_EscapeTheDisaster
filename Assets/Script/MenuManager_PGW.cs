using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager_PGW : MonoBehaviour
{
    public bool isStop = false;
    PickUpAble_PGW[] theItem;

    public GameObject OptionUI;
    public GameObject Player;

    Respawn_PGW theRespawn;
    // Start is called before the first frame update
    void Start()
    {
        theRespawn = FindObjectOfType<Respawn_PGW>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isStop)
            {

                Resume();
                CloseOption();

            }
            else
            {
                Pause();


            }
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스를 게임 중앙 좌표에 고정시키고 마우스커서가 안보임
        Cursor.visible = false;
        OptionUI.SetActive(false);
        isStop = false;


    }

    public void Pause()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        OptionUI.SetActive(true);
        isStop = true;


    }
    public void CloseOption()
    {
        OptionUI.SetActive(false);
    }
    public void GameQuit()
    {
        SceneManager.LoadScene("Intro");
    }
    public void LoadCheckPoint()
    {
        Player.transform.position = theRespawn.Respawn[0].position;
        theItem = FindObjectsOfType<PickUpAble_PGW>();
        foreach (PickUpAble_PGW item in theItem)
        {
            item.RespawnZone();
        }
    }
}
