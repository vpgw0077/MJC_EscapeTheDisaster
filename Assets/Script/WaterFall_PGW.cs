using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFall_PGW : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -4, 0) * Time.deltaTime); 
    }
    private void OnEnable()
    {
        
        StartCoroutine(DestroyWater());
    }

    IEnumerator DestroyWater()
    {
        
        yield return new WaitForSeconds(5f);
        WaterPooling_PGW.instance.InsertQueue(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject != null)
        {
            Debug.Log("Hit");
        }
    }
}
