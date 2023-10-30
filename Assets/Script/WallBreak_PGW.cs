using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreak_PGW : MonoBehaviour
{
    [SerializeField] private GameObject currentWallObject = null;
    [SerializeField] private GameObject[] wallPreset = null;
    [Space]
    [SerializeField] private GameObject wallDebris = null;
    [SerializeField] private int currentCount = 0;

    [Header("AudioComponent")]
    [SerializeField] private AudioSource audioPlayer = null;
    [SerializeField] private AudioClip crashSound = null;

    private float requireForce = 100f;
    private const int targetCount = 3;

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

            case 2:
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
        if (collision.transform.CompareTag("LightRock") && collision.relativeVelocity.magnitude >= requireForce)
        {
            CrackWall();
        }
    }
}
