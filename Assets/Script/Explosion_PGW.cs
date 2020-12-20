using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_PGW : MonoBehaviour
{
    public string ExplosionSound;
    public string CrashSound;

    public ParticleSystem ExplosionEffect;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public IEnumerator BoomWall()
    {

        yield return new WaitForSeconds(3.0f);

        SoundManager_PGW.instance.PlaySE(ExplosionSound);
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(new Vector3(0,1,1) * 50f, ForceMode.Impulse);
        CrashWall();
    }

    private void CrashWall()
    {
        Collider[] coll = Physics.OverlapSphere(transform.position, 10f);

        foreach (var col in coll)
        {
            Explosion_Wall_PGW thewall = col.GetComponent<Explosion_Wall_PGW>();
            if (thewall != null)
            {
                Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
                thewall.WallExplosion();
                SoundManager_PGW.instance.PlaySE(CrashSound);

            }


        }
    }

    public IEnumerator StartIgnite()
    {

        yield return new WaitForSeconds(3.0f);
        SoundManager_PGW.instance.PlaySE(ExplosionSound);
        FindExplosion();
    }

    public void FindExplosion()
    {
        Collider[] coll = Physics.OverlapSphere(transform.position, 10f);

        foreach (var col in coll)
        {
            if (col.transform.tag == "something")
            {
                Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
                col.GetComponent<Rigidbody>().mass = 0.1f;
                col.GetComponent<Rigidbody>().AddExplosionForce(150f, transform.position, 5f, 1f);

            }


        }
    }

}
