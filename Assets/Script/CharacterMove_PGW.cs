using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMove_PGW : MonoBehaviour
{
    public float moveSpeed;
    public float jumpforce;
    float h;
    float v;
    public float HP;
    public LayerMask layer;

    RaycastHit hit;
    Rigidbody rb;
    CapsuleCollider theCapsule;

    Animator anim;
    public bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        theCapsule = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        CheckGround();
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        
        anim.SetFloat("Vertical", v);
        anim.SetFloat("Horizontal", h);



    }
    private void FixedUpdate()
    {

            Move();


    }

    void Move()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");



        Vector3 horizontal = transform.right * h;
        Vector3 vertical = transform.forward * v;

        Vector3 velocity = (horizontal + vertical).normalized * moveSpeed;



        rb.MovePosition(transform.position + velocity * Time.deltaTime);

    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpforce, 0), ForceMode.Impulse);
            //anim.SetTrigger("Jump");
            //Physics.gravity = new Vector3(0, -18, 0);
        }
    }

    void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, theCapsule.bounds.extents.y - 0.8f, layer);


    }
}
