using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager_PGW : MonoBehaviour
{
    [SerializeField] private GameObject optionUI = null;
    [SerializeField] private GameObject player = null;

    private bool isStop = false;
    private Respawnable_PGW[] theItem;
    private PlayerRespawnZone_PGW theRespawn;

    // Start is called before the first frame update
    void Awake()
    {
        theRespawn = FindObjectOfType<PlayerRespawnZone_PGW>();
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        optionUI.SetActive(false);
        isStop = false;


    }
    public void GameQuit()
    {
        SceneManager.LoadScene("Intro");
    }

    public void LoadCheckPoint()
    {
        player.transform.position = theRespawn.respawnZoneList[0].position;
        theItem = FindObjectsOfType<Respawnable_PGW>();
        foreach (Respawnable_PGW item in theItem)
        {
            item.RespawnObject();
        }
    }
    private void CloseOption()
    {
        optionUI.SetActive(false);
    }

    private void Pause()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        optionUI.SetActive(true);
        isStop = true;


    }

}
