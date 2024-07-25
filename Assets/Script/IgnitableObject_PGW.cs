using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnitableObject_PGW : Ignite_PGW
{
    [SerializeField] private GameObject parentObject = null;

    [SerializeField] private MeshRenderer objectMesh = null;

    [SerializeField] private Material burningMaterial = null;
    [SerializeField] private Material igniteMaterial = null;


    private WaitForSeconds burnTime = new WaitForSeconds(7f);
    public override void Ignite()
    {
        base.Ignite();
        objectMesh.material = burningMaterial;
        StartCoroutine(BurnOut());

    }
    public override void Extinguish()
    {
        base.Extinguish();
        objectMesh.material = igniteMaterial;
    }

    private IEnumerator BurnOut()
    {
        yield return burnTime;
        Rigidbody rb = parentObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            parentObject.AddComponent<Rigidbody>();
        }


    }


}
