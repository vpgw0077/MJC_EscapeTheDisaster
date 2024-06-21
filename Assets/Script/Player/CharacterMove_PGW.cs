using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMove_PGW : MonoBehaviour, IState_PGW<CharacterMove_PGW.playerState>
{
    public enum playerState
    {
        Controlable,
        OutOfControl
    }

    [Space]
    [Header("State")]
    [SerializeField] private playerState PlayerState = playerState.Controlable;
    [Space]
    [Header("SteepComponent")]
    [SerializeField] private Transform lowRayStartPos = null;
    [SerializeField] private Transform upperRayStartPos = null;
    [SerializeField] private float steepHeight = 0.4f;
    [SerializeField] private float steepSmooth = 0.15f;


    private bool isGrounded = true;
    private bool isJumping;
    public bool IsJumping
    {
        get => isJumping;
        set { isJumping = value; }

    }
    private bool isFalling;
    public bool IsFalling
    {
        get => isFalling;

        private set { isFalling = value; }

    }

    [SerializeField]private float acceleration;
    public float Acceleration
    {
        get => acceleration;
        set => acceleration = Mathf.Max(0, value);
    }
    private float jumpForce;
    public float JumpForce
    {
        get => jumpForce;
        set => jumpForce = Mathf.Max(0, value);
    }
    private float maxSpeed;
    public float MaxSpeed
    {
        get => maxSpeed;
        set => maxSpeed = Mathf.Max(0, value);
    }
    private float deceleration = 0.93f;
    private float maxSlope = 60f;


    private readonly float maxSpeedY = 80f;

    private Vector3 direction;
    private Vector3 velXZ;
    private Vector3 velY;

    private Rigidbody rb;
    private RaycastHit lowSteepRay;
    private RaycastHit upperSteepRay;
    private RaycastHit slopeRay;
    private RaycastHit WallRay;

    // Start is called before the first frame update
    void Awake()
    {

        rb = GetComponent<Rigidbody>();
        upperRayStartPos.localPosition = new Vector3(upperRayStartPos.localPosition.x, steepHeight, upperRayStartPos.localPosition.z);
        MenuManager_PGW.Respawn += RespawnPlayer;
    }

    public void MoveCharacter(Vector3 dir)
    {
        if (dir != Vector3.zero)
        {
            rb.AddForce(dir * Acceleration, ForceMode.Acceleration);
            if (rb.velocity.magnitude >= MaxSpeed)
            {
                velXZ = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                velY = new Vector3(0, rb.velocity.y, 0);

                velXZ = Vector3.ClampMagnitude(velXZ, MaxSpeed);
                velY = Vector3.ClampMagnitude(velY, maxSpeedY);

                rb.velocity = velXZ + velY;
            }

        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x * deceleration, rb.velocity.y, rb.velocity.z * deceleration); // 감속
        }

    }
    private void FixedUpdate()
    {
        if (IsJumping)
        {
            Jump();
        }
        StepUpSteep();

    }

    private bool IsSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeRay, 1f, -1, QueryTriggerInteraction.Ignore))
        {
            float angle = Vector3.Angle(Vector3.up, slopeRay.normal);
            return angle <= maxSlope && angle != 0;

        }
        return false;
    }
    private void StepUpSteep()
    {
        if (IsSlope() || !isGrounded) return;
        if (Physics.Raycast(lowRayStartPos.position, direction, out lowSteepRay, 0.5f, 1, QueryTriggerInteraction.Ignore))
        {
            
            if (!Physics.Raycast(upperRayStartPos.position, direction, out upperSteepRay, 0.6f, 1, QueryTriggerInteraction.Ignore))
            {
                
                rb.position += new Vector3(0, steepSmooth, 0f);
            }
        }
    }

    void Jump()
    {

        rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        IsJumping = false;

    }

    
    private void RespawnPlayer()
    {
        rb.velocity = Vector3.zero;
        transform.position = CheckPointInfo_PGW.currentCheckPoint.position;
    }
    public IEnumerator ChangeState(playerState state, float duration)
    {
        yield return new WaitForSeconds(duration);
        PlayerState = state;
    }


}
