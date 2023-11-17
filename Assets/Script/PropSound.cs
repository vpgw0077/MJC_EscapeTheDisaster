using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioPlayer = null;
    [SerializeField] private AudioClip[] sfx = null;

    [Space]
    [SerializeField] private float targetSpeed = 0f;


    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= targetSpeed)
        {
            int index = Random.Range(0, sfx.Length);
            audioPlayer.PlayOneShot(sfx[index]);

        }

    }

}
