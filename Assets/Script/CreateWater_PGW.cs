using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWater_PGW : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Createwater());
    }

    IEnumerator Createwater()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject t_object = WaterPooling_PGW.instance.GetQueue();
            t_object.transform.position = transform.position;
        }
    }

}
