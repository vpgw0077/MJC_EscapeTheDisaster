using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusController_PGW : MonoBehaviour
{
    public GameObject DNA;
    public enum VirusType
    {
        CommonVirus,
        SuperVirus
    }
    public VirusType virus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
         if(virus == VirusType.CommonVirus)
        {
            if(collision.transform.tag == "Player")
            {
                Instantiate(DNA, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
         else if(virus == VirusType.SuperVirus)
        {
            if(collision.transform.tag == "White")
            {
                Instantiate(DNA, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
