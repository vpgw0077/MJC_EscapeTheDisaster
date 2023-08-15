using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCar_PGW : MonoBehaviour

{
    [SerializeField] private float speed;
    [SerializeField] private string crashSound;
    [SerializeField] private string carSound;

    private bool isReady;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReady)
        {
            rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        }
    }
    public void StartEngine()
    {
        isReady = true;
        SoundManager_PGW.instance.PlaySE(carSound);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Renton"))
        {
            isReady = false;
            SoundManager_PGW.instance.StopAllSE();
            SoundManager_PGW.instance.PlaySE(crashSound);
        }
    }
}
