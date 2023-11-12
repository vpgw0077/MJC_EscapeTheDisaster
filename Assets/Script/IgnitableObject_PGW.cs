using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnitableObject_PGW : Ignite_PGW
{
    [SerializeField] private GameObject parentObject = null;

    [SerializeField] private MeshRenderer objectMesh = null;
    [SerializeField] private Material igniteMaterial = null;

    private bool isBurnOut = false;


    public override void Ignite()
    {
        base.Ignite();
        if (!isBurnOut)
        {
            StartCoroutine(ChangeMaterial());

        }

    }
    public override void Extinguish()
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
        Rigidbody rb = parentObject.GetComponent<Rigidbody>();
        if(rb == null)
        {
            parentObject.AddComponent<Rigidbody>();
        }
    }

    private IEnumerator ChangeMaterial()
    {
        yield return new WaitForSeconds(3f);
        objectMesh.material = igniteMaterial;
        isBurnOut = true;
    }



}
