using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus_PGW : MonoBehaviour
{
    // 특정 상태에 따라 변할 수 있는 값을 모아둔 클래스

    [Header("JumpValue")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float carryingJumpForce;

    [Space]

    [Header("AccelerationValue")]
    [SerializeField] private float groundAcceleration;
    [SerializeField] private float airAcceleration;

    [Space]

    [Header("MaxSpeedValue")]
    [SerializeField] private float groundMaxSpeed;
    [SerializeField] private float airMaxSpeed;

    public float JumpForce => jumpForce;
    public float CarryingJumpforce => carryingJumpForce;
    public float GroundAcceleration => groundAcceleration;
    public float AirAcceleration => airAcceleration;
    public float GroundMaxSpeed => groundMaxSpeed;
    public float AirMaxSpeed => airMaxSpeed;


}
