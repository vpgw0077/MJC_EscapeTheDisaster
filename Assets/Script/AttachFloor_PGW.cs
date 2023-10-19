using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachFloor_PGW : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag != "Player" || other.transform.tag != "PickPosition")
        {
            other.transform.SetParent(gameObject.transform);

        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.transform.tag != "Player" || other.transform.tag != "PickPosition")
        {
            other.transform.SetParent(null);

        }
    }

}
