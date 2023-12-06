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
    [SerializeField] private GameObject DNA = null;
    [SerializeField] private VirusType virusType = VirusType.CommonVirus;

    private Vector3 originVirusPos = Vector3.zero;
    private Respawnable_PGW theRespawnAble = null;
    private void Awake()
    {
        originVirusPos = DNA.transform.localPosition;
        theRespawnAble = DNA.GetComponent<Respawnable_PGW>();

    }
    private void OnCollisionEnter(Collision collision)
    {
         if(virusType == VirusType.CommonVirus)
        {
            if(collision.transform.CompareTag("Player") || collision.transform.CompareTag("White"))
            {
                theRespawnAble.ChangeRespawnPos(transform.localPosition);
                DNA.transform.localPosition = gameObject.transform.localPosition;
                gameObject.transform.localPosition = originVirusPos;
            }
        }
         else if(virusType == VirusType.SuperVirus)
        {
            if(collision.transform.CompareTag("White"))
            {
                theRespawnAble.ChangeRespawnPos(transform.localPosition);
                DNA.transform.localPosition = gameObject.transform.localPosition;
                gameObject.transform.localPosition = originVirusPos;
            }
        }
    }
}
