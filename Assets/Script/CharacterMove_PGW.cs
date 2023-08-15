using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMove_PGW : MonoBehaviour
{
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private AudioClip[] walkClip;

    private bool isGrounded = true;
    private bool isJumping = false;

    private float h;
    private float v;
    private float animationHorizontal;
    private float animationVertical;
    private float accacceleration = 30f;
    private float groundRayLength = 1.5f;
    private float walkAudioTimer = 0f;
    private float walkAudioPeriod = 0.6f;
    private float jumpForce => theInteract.Carrying ? 5f : 10f;
 

    private readonly float maxSpeed = 8f;
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
        ActOnGround();
        CheckGround();
        CalculateMoveValue();
        PlayWalkSound();

    }

    private void FixedUpdate()
    {
        rb.AddForce(direction * accacceleration * Time.deltaTime, ForceMode.VelocityChange);
        if (rb.velocity.magnitude >= maxSpeed)
        {
            velXZ = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            velY = new Vector3(0, rb.velocity.y, 0);

            velXZ = Vector3.ClampMagnitude(velXZ, maxSpeed);
            velY = Vector3.ClampMagnitude(velY, float.MaxValue);

            rb.velocity = velXZ + velY;
        }


        if (isJumping)
        {
            Jump();
        }

    }

    private void PlayWalkSound()
    {
        if (!isGrounded) return;
        else if (h == 0 && v == 0) return;

        walkAudioTimer -= Time.deltaTime;
        if(walkAudioTimer <= 0)
        {
            int index = Random.Range(0, walkClip.Length);
            audioPlayer.PlayOneShot(walkClip[index]);
            walkAudioTimer = walkAudioPeriod;
        }
        

    }
    private void ActOnGround()
    {
        if (isGrounded)
        {
            rb.drag = 1f;
            accacceleration = 30f;

            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                isJumping = true;
            }
        }

        else
        {
            accacceleration = 20f;
            rb.drag = 0f;
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
}
