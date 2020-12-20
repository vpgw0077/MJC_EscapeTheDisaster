using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizClear_PGW : MonoBehaviour
{
    public Animator anim;
    public bool isRightAnswer;
    public string DoorSound;
    public bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CheckAnswer()
    {
        if (isRightAnswer)
        {
            OpenDoor();
        }
    }
    public void CloseDoor()
    {
        if (isOpen)
        {
            isOpen = false;
            SoundManager_PGW.instance.PlaySE(DoorSound);
            anim.SetBool("OpenDoor", false);
        }

    }

    public void OpenDoor()
    {
        isOpen = true;
        SoundManager_PGW.instance.PlaySE(DoorSound);
        anim.SetBool("OpenDoor", true);


    }
}
