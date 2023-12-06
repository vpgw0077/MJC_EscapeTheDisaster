using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo_PGW : MonoBehaviour
{
    [SerializeField ]private string objectName = null;

    public string ObjectName
    {
        get
        {
            return objectName;
        }
        private set
        {
            objectName = value;
        }
    }


}
