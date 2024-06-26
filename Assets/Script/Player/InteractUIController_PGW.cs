using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractUIController_PGW : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI InteractText = null;

    public void ShowInteractUI(ObjectInfo_PGW info)
    {
        InteractText.gameObject.SetActive(true);
        InteractText.text = info.ObjectName + "<color=yellow>" + "(E)" + "</color>";
    }

    public void HideInteractUI()
    {
        InteractText.text = "";
        InteractText.gameObject.SetActive(false);
    }
}
