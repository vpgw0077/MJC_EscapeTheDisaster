using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachFloor_PGW : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player" && other.tag !="PickPosition")
        {
            other.transform.SetParent(gameObject.transform,true);
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        {
            if (other.tag != "Player" && other.tag != "PickPosition")
            {
                other.transform.SetParent(null);
            }
        }
    }
}
