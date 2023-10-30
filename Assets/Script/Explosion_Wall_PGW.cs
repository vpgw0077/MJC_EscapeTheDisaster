using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Wall_PGW : MonoBehaviour
{
    [SerializeField] private GameObject Debric = null;
    [SerializeField] private float Force = 0;

    private Vector3 offset = Vector3.zero;
    public void WallExplosion()
    {
        GameObject clone = Instantiate(Debric, transform.position, Quaternion.identity);
        Rigidbody[] rb = clone.GetComponentsInChildren<Rigidbody>();
        //ollider[] col = clone.GetComponentsInChildren<Collider>();
        for (int i = 0; i < rb.Length; i++)
        {
            rb[i].AddExplosionForce(Force, transform.position + offset, 10f);
            // col[i].enabled = false;
        }
        gameObject.SetActive(false);
    }
}
