using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController_PGW : MonoBehaviour
{
    public float mouseSensitivity;
    public Transform targetTransform;
    public Transform cameraTransform;
    public Transform cameraPivotTransform;
    public SkinnedMeshRenderer targetRender;
    public bool changeTransparency = true;
    Transform myTransform;
    Vector3 cameraTransformPosition;
    Vector3 cameraFollowVelocity = Vector3.zero;

    public float lookSpeed = 0.1f;
    public float followSpeed = 0.1f;
    public float pivotSpeed = 0.03f;

    float targetPosition;
    float defaultPosition;
    float lookAngle;
    float pivotAngle;
    public float minimumPivot = -35;
    public float maximumPivot = 35;

    public float cameraSphereRadius = 0.2f;
    public float cameraCollisionOffset = 0.2f;
    public float minimumCollisionOffset = 0.2f;

    private void Awake()
    {
        myTransform = transform;
        defaultPosition = cameraTransform.localPosition.z;
    }

    public void FollowTarget(float delta)
    {
        Vector3 targetPosition = Vector3.Lerp(myTransform.position, targetTransform.position, delta / followSpeed);
        myTransform.position = targetPosition;

        HandleCameraCollision(delta);
    }

    public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
    {
        lookAngle += Input.GetAxis("Mouse X") * mouseXInput;
        pivotAngle -= Input.GetAxis("Mouse Y") * mouseYInput;
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

        /*Vector3 rotation = Vector3.zero;
        rotation.x = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        myTransform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;

        targetRotation = Quaternion.Euler(rotation);*/
        targetTransform.rotation = Quaternion.Euler(0, lookAngle, 0);
         cameraPivotTransform.localRotation = Quaternion.Euler(pivotAngle, lookAngle, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;

        FollowTarget(delta);
        HandleCameraRotation(delta, mouseSensitivity, mouseSensitivity);
    }

    void HandleCameraCollision(float delta)
    {
        targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
        direction.Normalize();


        if (Physics.SphereCast(cameraPivotTransform.position, cameraSphereRadius, direction, out hit, Mathf.Abs(targetPosition)))
        {
            float dis = Vector3.Distance(cameraPivotTransform.position, hit.point);
            targetPosition = -(dis - cameraCollisionOffset);

            Color temp = targetRender.sharedMaterial.color;
            temp.a = Mathf.Lerp(temp.a, 0.2f, 5 * Time.deltaTime);

            targetRender.sharedMaterial.color = temp;



        }
        else
        {
            if (targetRender.sharedMaterial.color.a <= 0.99f)
            {
                Color temp = targetRender.sharedMaterial.color;
                temp.a = Mathf.Lerp(temp.a, 1, 5 * Time.deltaTime);

                targetRender.sharedMaterial.color = temp;
            }

        }


        if (Mathf.Abs(targetPosition) < minimumCollisionOffset)
        {
            targetPosition = -minimumCollisionOffset;
            


        }
        cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, delta / 0.2f);
        cameraTransform.localPosition = cameraTransformPosition;

        }


    }



