using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController_PGW : MonoBehaviour, IState_PGW<CharacterController_PGW.playerState>
{
    [SerializeField] private KeyCode jumpKeyCode = KeyCode.Space;
    [SerializeField] private KeyCode interactKeyCode = KeyCode.E;
    [SerializeField] private KeyCode throwKeyCode = KeyCode.Mouse0;
    public enum playerState
    {
        Controlable,
        OutOfControl
    }

    private playerState PlayerState = playerState.Controlable;

    [Header("WallBlockCheckComponent")]
    [SerializeField] private Transform wallBlockCheckPos = null;
    [SerializeField] private float wallBlockRayLength = 0.7f;
    [SerializeField] private PhysicMaterial frictionZero = null;
    [SerializeField] private PhysicMaterial frictionNormal = null;
    private float h = 0;
    private float v = 0;

    private bool isGrounded = true;
    private bool isFalling = false;

    private float fallingPeriod = 1f;
    private float fallingTimer = 0f;
    private float groundRayLength = 1.5f;

    private Vector3 direction = Vector3.zero;

    private CapsuleCollider theCapsule = null;

    private CharacterMovement_PGW characterMove = null;
    private CharacterAnimation_PGW characterAnimation = null;
    private CharacterSound_PGW characterSound = null;
    private CharacterStatus_PGW characterStatus = null;
    private Interact_PGW interact = null;

    private readonly Vector3 boxCastSize = new Vector3(0.3f, 0.1f, 0.3f);

    private void Awake()
    {
        characterMove = GetComponent<CharacterMovement_PGW>();
        characterAnimation = GetComponent<CharacterAnimation_PGW>();
        characterSound = GetComponent<CharacterSound_PGW>();
        characterStatus = GetComponent<CharacterStatus_PGW>();
        interact = GetComponent<Interact_PGW>();
        theCapsule = GetComponent<CapsuleCollider>();
    }


    void Update()
    {
        CalculateMoveValue();
        UpdateStatusValue();
        UpdateFootStepSound();
        FallingCheck();
        CheckGrounded();
        BlockStuckWall();
        characterAnimation.PlayWalkAnimation(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (isGrounded)
        {
            TryJump();
        }

        if (Input.GetKeyDown(interactKeyCode))
        {
            interact.TryInteractObject();
        }
        else if (Input.GetKeyDown(throwKeyCode))
        {
            interact.TryThrowObject();
        }

    }
    private void FixedUpdate()
    {
        characterMove.MoveCharacter(direction);
        if (isGrounded)
        {
            characterMove.StepUpSteep(direction);

        }
    }

    private void UpdateFootStepSound()
    {
        if (!isGrounded) return;
        if (h == 0 && v == 0) return;

        characterSound.PlayWalkSound();
    }
    private void TryJump()
    {
        if (Input.GetKeyDown(jumpKeyCode) && !characterMove.IsJumping)
        {
            characterMove.IsJumping = true;
        }
    }
    private void CalculateMoveValue()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        direction = (transform.right * h + transform.forward * v).normalized;
    }

    private void UpdateStatusValue()
    {
        characterMove.Acceleration = isGrounded ? characterStatus.GroundAcceleration : characterStatus.AirAcceleration;
        characterMove.JumpForce = interact.Carrying ? characterStatus.CarryingJumpforce : characterStatus.JumpForce;
        characterMove.MaxSpeed = PlayerState == playerState.Controlable ? characterStatus.GroundMaxSpeed : characterStatus.AirMaxSpeed;

    }
    private void FallingCheck()
    {
        if (!isGrounded && !isFalling)
        {
            fallingTimer += Time.deltaTime;
            if (fallingTimer >= fallingPeriod)
            {
                isFalling = true;
                characterAnimation.PlayFallingAnimation(isFalling);

            }
        }
        else if (isGrounded)
        {
            isFalling = false;
            characterAnimation.PlayFallingAnimation(isFalling);
            fallingTimer = 0f;

        }
    }

    private void BlockStuckWall()
    {
        RaycastHit hit;
        if (Physics.Raycast(wallBlockCheckPos.position, direction, out hit, wallBlockRayLength, 1, QueryTriggerInteraction.Ignore))
        {
            theCapsule.material = frictionZero;
        }
        else
        {
            theCapsule.material = frictionNormal;
        }
    }
    private void CheckGrounded()
    {
        RaycastHit hit;
        if (Physics.BoxCast(theCapsule.bounds.center, boxCastSize / 2, Vector3.down, out hit, Quaternion.identity, groundRayLength, -1, QueryTriggerInteraction.Ignore))
        {
            isGrounded = true;

        }
        else
        {
            isGrounded = false;
        }

    }

    public IEnumerator ChangeState(playerState state, float duration)
    {
        yield return new WaitForSeconds(duration);
        PlayerState = state;
    }

}
