using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private AudioClip[] sfx;

    private Rigidbody rb;

    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb.velocity.magnitude >= 2f)
        {
            int index = Random.Range(0, sfx.Length);
            audioPlayer.PlayOneShot(sfx[index]);

        }
    }

}
