using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCar_PGW : MonoBehaviour

{
    public float Speed;
    public bool isReady;
    public string CrashSound;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReady)
        {

            rb.MovePosition(transform.position + transform.forward * Speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Renton")
        {
            isReady = false;
            SoundManager_PGW.instance.StopAllSE();
            SoundManager_PGW.instance.PlaySE(CrashSound);
            //rb.constraints = RigidbodyConstraints.FreezePosition;
        }
    }
}
