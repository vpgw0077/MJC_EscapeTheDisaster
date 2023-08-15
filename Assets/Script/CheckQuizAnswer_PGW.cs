using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckQuizAnswer_PGW : DoorAndBridgeManager_PGW
{
    [SerializeField] private string quizAnswer;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AnswerBox"))
        {
            string theAlphabat = other.GetComponent<AlphabatType_PGW>().AlphabatType;
            if (quizAnswer == theAlphabat)
            {
                StopAllCoroutines();
                StartCoroutine("TurnOn");

            }
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AnswerBox"))
        {
            string theAlphabat = other.GetComponent<AlphabatType_PGW>().AlphabatType;
            if (quizAnswer == theAlphabat)
            {
                StopAllCoroutines();
                StartCoroutine("TurnOff");

            }

        }
    }


}
