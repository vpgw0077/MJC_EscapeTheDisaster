using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreak_PGW : MonoBehaviour
{
    [SerializeField] private GameObject currentWallObject;
    [SerializeField] private GameObject[] wallPreset;
    [Space]
    [SerializeField] private GameObject wallDebris;
    [SerializeField] private int currentCount;

    [Header("AudioComponent")]
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private AudioClip crashSound;

   private float requireForce = 10f;
    private const int targetCount = 4;

    private BoxCollider theBoxCollider;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Awake()
    {
        audioPlayer.clip = crashSound;
        theBoxCollider = GetComponent<BoxCollider>();
        currentWallObject = wallPreset[0];
        currentWallObject.SetActive(true);
    }

    private void CrackWall()
    {
        currentCount++;
        switch (currentCount)
        {
            case 1:
                currentWallObject.SetActive(false);
                currentWallObject = wallPreset[1];
                currentWallObject.SetActive(true);
                break;

            case 3:
                currentWallObject.SetActive(false);
                currentWallObject = wallPreset[2];
                currentWallObject.SetActive(true);
                break;

            case targetCount:
                currentWallObject.SetActive(false);
                theBoxCollider.enabled = false;
                wallDebris.SetActive(true);
                audioPlayer.Play();
                break;
               
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.transform.GetComponent<Rigidbody>();
        if(collision.transform.CompareTag("LightRock")&& rb.velocity.magnitude >= requireForce)
        {
            CrackWall();
        }
    }
}
