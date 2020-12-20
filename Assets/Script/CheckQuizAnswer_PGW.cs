using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckQuizAnswer_PGW : MonoBehaviour
{
    public string quizAnswer;
    QuizClear_PGW theQuiz;
    // Start is called before the first frame update
    private void Start()
    {
        theQuiz = GetComponentInParent<QuizClear_PGW>();
    }
    private void OnTriggerEnter(Collider other)
    {
        string theAlphabat = other.GetComponent<AlphabatType_PGW>().AlphabatType;
        if(quizAnswer == theAlphabat && other.transform.tag == "AnswerBox")
        {
            theQuiz.isRightAnswer = true;
            theQuiz.CheckAnswer();
        }

    }
    private void OnTriggerExit(Collider other)
    {
        string theAlphabat = other.GetComponent<AlphabatType_PGW>().AlphabatType;
        if (other.transform.tag == "AnswerBox")
        {
            theQuiz.isRightAnswer = false;
            if (theQuiz.isOpen)
            {
                theQuiz.CloseDoor();
            }
        }
    }
}
