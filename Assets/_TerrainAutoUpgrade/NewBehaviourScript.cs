using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform room1 = null;
    public Transform room2 = null;
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameObject.transform.position = room1.position;
        }

        else if (Input.GetKeyDown(KeyCode.Q))
        {
            gameObject.transform.position = room2.position;
        }
    }
}
