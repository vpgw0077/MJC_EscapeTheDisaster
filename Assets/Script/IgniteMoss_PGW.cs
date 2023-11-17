using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgniteMoss_PGW : Ignite_PGW
{
    [SerializeField] private GameObject moss = null;
    public override void Ignite()
    {
        base.Ignite();
        StartCoroutine(BurnMoss());
        
    }
    private IEnumerator BurnMoss()
    {
        yield return new WaitForSeconds(5f);
        Extinguish();
        Destroy(moss);
    }
}
