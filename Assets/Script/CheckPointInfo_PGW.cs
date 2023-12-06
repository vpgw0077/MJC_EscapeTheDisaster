using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointInfo_PGW : MonoBehaviour
{
    public static Transform currentCheckPoint = null;

    [SerializeField] private List<Transform> checkPointPosList;
    [SerializeField] private int checkPointIndex = 0;

    [Space]
    [SerializeField] private GameObject UpdateText = null;

    private WaitForSeconds sec = new WaitForSeconds(2.5f);
    private void Awake()
    {
        UpdateText.SetActive(false);
        currentCheckPoint = checkPointPosList[0];
    }
    private IEnumerator ShowUpdateText()
    {
        UpdateText.SetActive(true);
        yield return sec;
        UpdateText.SetActive(false);
    }
    public void CheckPointUpdate()
    {
        StartCoroutine(ShowUpdateText());
        checkPointIndex++;
        currentCheckPoint = checkPointPosList[checkPointIndex];
    }

}
