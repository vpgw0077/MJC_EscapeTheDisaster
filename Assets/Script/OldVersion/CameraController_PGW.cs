using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_PGW : MonoBehaviour
{
    public float mouseSensitivity = 10;
    public Transform target;
    public Transform CamFollow;
    public float distFromTarget = 2;
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    public float rotationSmoothTime;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    float yaw;
    float pitch;

    public bool NeedCollisionCheck;

    public bool changeTransparency = true;
    public MeshRenderer targetRender;

    public float moveSpeed = 5;
    public float returnSpeed = 9;
    public float wallpush = 0.7f;

    public float closetDistanceToPlayer = 2;
    public float evenCloserDistanceToPlayer = 1;

    public LayerMask collisionMask;

    public bool pitchLock = false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void FixedUpdate()
    {
        CollisionCheck();
        WallCheck();

        if (!pitchLock)
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
            currentRotation = new Vector3(pitch, yaw, 0);
            //currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
            Quaternion localRotation = Quaternion.Euler(pitch, yaw, 0);
            transform.rotation = localRotation;

            Quaternion e = transform.rotation;
            e.x = 0;
            // transform.position = target.position - transform.forward * distFromTarget;
            target.rotation = Quaternion.Euler(0, yaw, 0);
        }
        else
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch = pitchMinMax.y;
            currentRotation = Vector3.Lerp(currentRotation, new Vector3(pitch, yaw), rotationSmoothTime * Time.deltaTime);

        }



    }

    void WallCheck()
    {
        Ray ray = new Ray(target.position, -target.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, 0.5f, out hit, 0.7f, collisionMask))
        {
            pitchLock = true;

        }
        else
        {
            pitchLock = false;
        }
    }

    void CollisionCheck()
    {
        RaycastHit hit;
        if (Physics.Linecast(target.position, target.position - transform.forward * distFromTarget, out hit, collisionMask))
        {

            Vector3 norm = hit.normal * wallpush;
            Vector3 p = hit.point + norm;
            NeedCollisionCheck = true;
            TransparencyCheck();
            if (Vector3.Distance(Vector3.Lerp(transform.position, p, moveSpeed * Time.deltaTime), target.position) <= evenCloserDistanceToPlayer)
            {

            }
            else
            {

                transform.position = Vector3.Lerp(transform.position, p, moveSpeed * Time.deltaTime);
            }


            return;

        }

        FullTransperency();

        transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y + 0.35f, transform.position.z), target.position - transform.forward * distFromTarget, returnSpeed * Time.deltaTime);



        pitchLock = false;
    }

    void TransparencyCheck()
    {
        if (changeTransparency)
        {
            if (Vector3.Distance(transform.position, target.position) <= closetDistanceToPlayer)
            {
                Color temp = targetRender.sharedMaterial.color;
                temp.a = Mathf.Lerp(temp.a, 0.2f, moveSpeed * Time.deltaTime);

                targetRender.sharedMaterial.color = temp;

            }
            else
            {
                if (targetRender.sharedMaterial.color.a <= 0.99f)
                {
                    Color temp = targetRender.sharedMaterial.color;
                    temp.a = Mathf.Lerp(temp.a, 1, moveSpeed * Time.deltaTime);

                    targetRender.sharedMaterial.color = temp;
                }
            }
        }
    }
    void FullTransperency()
    {
        if (changeTransparency)
        {
            if (targetRender.sharedMaterial.color.a <= 0.99f)
            {
                Color temp = targetRender.sharedMaterial.color;
                temp.a = Mathf.Lerp(temp.a, 1, moveSpeed * Time.deltaTime);

                targetRender.sharedMaterial.color = temp;
            }
        }
    }


}
