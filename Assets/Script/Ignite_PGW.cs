using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ignite_PGW : MonoBehaviour
{
    [Header("AudioComponent")]
    [SerializeField] protected AudioSource theAudioSource = null;
    [SerializeField] protected AudioClip theAudioClip = null;
    [Space]
    [SerializeField] protected ParticleSystem[] fireParticle = null;
    [SerializeField] protected GameObject fireLight = null;

    [SerializeField] protected bool isIgnite = false;

    public virtual void Ignite()
    {
        if (isIgnite) return;

        isIgnite = true;
        theAudioSource.PlayOneShot(theAudioClip);
        for (int i = 0; i < fireParticle.Length; i++)
        {
            var particle = fireParticle[i].main;
            particle.loop = true;
            fireParticle[i].Play();
        }
        fireLight.SetActive(true);


    }
    public virtual void Extinguish()
    {
        if (!isIgnite) return;

        isIgnite = false;
        theAudioSource.Stop();
        for (int i = 0; i < fireParticle.Length; i++)
        {
            var particle = fireParticle[i].main;
            particle.loop = false;
        }
        fireLight.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("SprinklerSensor") && isIgnite)
        {
            Sprinkler_PGW sprinkler = other.GetComponent<Sprinkler_PGW>();
            if (sprinkler != null)
            {
                StartCoroutine(sprinkler.ActivateSprinkler());
            }

        }

        if (isIgnite)
        {
            Ignite_PGW ignitableObject = other.transform.GetComponent<Ignite_PGW>();
            if (ignitableObject != null)
            {
                ignitableObject.Ignite();
            }

        }
    }
}
