using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusController_PGW : MonoBehaviour
{
    public enum VirusType
    {
        CommonVirus,
        SuperVirus
    }
    [SerializeField] private GameObject DNA;
    [SerializeField] private VirusType virusType;

    private void OnCollisionEnter(Collision collision)
    {
         if(virusType == VirusType.CommonVirus)
        {
            if(collision.transform.CompareTag("Player") || collision.transform.CompareTag("White"))
            {
                Instantiate(DNA, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
         else if(virusType == VirusType.SuperVirus)
        {
            if(collision.transform.CompareTag("White"))
            {
                Instantiate(DNA, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
