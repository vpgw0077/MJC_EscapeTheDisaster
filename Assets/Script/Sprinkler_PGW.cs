using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinkler_PGW : MonoBehaviour
{
    [SerializeField] private AudioSource[] theAudioSources = null;
    [SerializeField] private AudioClip theAudioClip = null;
    [SerializeField] private ParticleSystem[] waterParticles = null;
    [Space]
    [SerializeField] private float radius = 50f;
    private bool isActivate = false;

    public IEnumerator ActivateSprinkler()
    {
        if (isActivate) yield break;

        isActivate = true;
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < waterParticles.Length; i++)
        {
            waterParticles[i].Play();
            theAudioSources[i].PlayOneShot(theAudioClip);

        }

        yield return new WaitForSeconds(12f);
        for (int i = 0; i < waterParticles.Length; i++)
        {
            theAudioSources[i].Stop();

        }
        Collider[] coll = Physics.OverlapSphere(transform.position, radius);
        foreach (var col in coll)
        {
            Ignite_PGW ignitableObject = col.GetComponent<Ignite_PGW>();
            if (ignitableObject != null)
            {
                ignitableObject.Extinguish();
            }


        }
        isActivate = false;
        yield return null;
    }
}
