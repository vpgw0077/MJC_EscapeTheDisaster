using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnable_PGW : MonoBehaviour
{
    private Quaternion originRotation;
    private Vector3 originPos;
    private Vector3 respawnPos;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originPos = transform.localPosition;
        respawnPos = originPos;
        originRotation = transform.rotation;
        MenuManager_PGW.Respawn += InitializeRespawnPos;
    }
    public void ChangeRespawnPos(Vector3 targetPos)
    {
        respawnPos = targetPos;
    }
    public void InitializeRespawnPos()
    {
        respawnPos = originPos;
        RespawnObject();
    }
    public void RespawnObject()
    {
        rb.velocity = Vector3.zero;
        transform.localPosition = respawnPos;
        transform.rotation = originRotation;
    }
}
