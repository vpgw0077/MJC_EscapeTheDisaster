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

    [SerializeField] private AudioSource audioPlayer = null;
    [SerializeField] private AudioClip[] walkClip = null;
    [SerializeField] private playerState PlayerState = playerState.Controlable;
    [SerializeField] private float deceleration = 0f;

    private bool isGrounded = true;
    private bool isJumping = false;

    private float h = 0;
    private float v = 0;
    private float animationHorizontal = 0;
    private float animationVertical = 0;
    private float groundRayLength = 1.5f;
    private float walkAudioTimer = 0f;
    private float walkAudioPeriod = 0.6f;
    private float acceleration => isGrounded ? 30f : 5f;
    private float jumpForce => theInteract.Carrying ? 5f : 10f;
    private float maxSpeed => PlayerState == playerState.Controlable ? 8f : 100f;
    private float maxSpeedY = 80f;

    private readonly Vector3 boxCastSize = new Vector3(0.3f, 0.1f, 0.3f);

    private Vector3 direction;
    private Vector3 velXZ;
    private Vector3 velY;

    private Rigidbody rb;
    private CapsuleCollider theCapsule;
    private Interact_PGW theInteract;
    private RaycastHit hit;
    private Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        theInteract = GetComponent<Interact_PGW>();
        anim = GetComponent<Animator>();
        theCapsule = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        CalculateMoveValue();

        if (isGrounded)
        {
            TryJump();
            PlayWalkSound();

        }

    }

    private void FixedUpdate()
    {
        if (direction != Vector3.zero)
        {
            rb.AddForce(direction * acceleration * Time.fixedDeltaTime, ForceMode.VelocityChange);
            if (rb.velocity.magnitude >= maxSpeed)
            {
                velXZ = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                velY = new Vector3(0, rb.velocity.y, 0);

                velXZ = Vector3.ClampMagnitude(velXZ, maxSpeed);
                velY = Vector3.ClampMagnitude(velY, maxSpeedY);

                rb.velocity = velXZ + velY;
            }

        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x * deceleration, rb.velocity.y, rb.velocity.z * deceleration); // 감속
        }
         
        if (isJumping)
        {
            Jump();
        }

    }

    private void PlayWalkSound()
    {
        if (h == 0 && v == 0) return;

        walkAudioTimer -= Time.deltaTime;

        if (walkAudioTimer <= 0)
        {
            int index = Random.Range(0, walkClip.Length);
            audioPlayer.PlayOneShot(walkClip[index]);
            walkAudioTimer = walkAudioPeriod;
        }


    }
    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
        }

    }
    private void CalculateMoveValue()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        animationHorizontal = Input.GetAxis("Horizontal");
        animationVertical = Input.GetAxis("Vertical");

        anim.SetFloat("Horizontal", animationHorizontal);
        anim.SetFloat("Vertical", animationVertical);

        direction = (transform.right * h + transform.forward * v).normalized;

    }
    void Jump()
    {

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isJumping = false;

    }

    void CheckGround()
    {

        if (Physics.BoxCast(theCapsule.bounds.center, boxCastSize / 2, Vector3.down, out hit, Quaternion.identity, groundRayLength))
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
