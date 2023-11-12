using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachFloor_PGW : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        other.transform.SetParent(gameObject.transform);
    }

    private void OnCollisionExit(Collision other)
    {
        other.transform.SetParent(null);
    }

}
