using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController_PGW : MonoBehaviour
{
    [SerializeField] private Rigidbody targetTransform = null;
    [SerializeField] private Transform cameraTransform = null;
    [SerializeField] private Transform cameraPivotTransform = null;

    [SerializeField] private SkinnedMeshRenderer targetRender = null;

    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float followSpeed = 0.1f;
    [SerializeField] private float minimumPivot = -35;
    [SerializeField] private float maximumPivot = 35;
    [SerializeField] private float cameraSphereRadius = 0.2f;
    [SerializeField] private float cameraCollisionOffset = 0.2f;
    [SerializeField] private float minimumCollisionOffset = 0.2f;

    private Transform myTransform = null;
    private Vector3 cameraTransformPosition;
    private float targetPosition = 0f;
    private float defaultPosition = 0f;
    private float targetLookAngle = 0f;
    private float camLookAngle = 0f;
    private float pivotAngle = 0f;

    private void Awake()
    {
        myTransform = transform;
        defaultPosition = cameraTransform.localPosition.z;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void FixedUpdate()
    {
        if (!MenuManager_PGW.isStop)
        {
            HandleTargetRotation();

        }
    }

    private void LateUpdate()
    {
        if (!MenuManager_PGW.isStop)
        {
            float delta = Time.deltaTime;
            HandleCameraRotation();
            FollowTarget();
            HandleCameraCollision(delta);

        }

    }

    private void HandleTargetRotation()
    {
        targetLookAngle += Input.GetAxis("Mouse X") * mouseSensitivity;
        targetTransform.rotation = Quaternion.Euler(0, camLookAngle, 0);
    }
    private void FollowTarget()
    {

        myTransform.position = Vector3.Lerp(myTransform.position, targetTransform.position, Time.deltaTime * followSpeed);
    }

    private void HandleCameraRotation()
    {
        camLookAngle += Input.GetAxis("Mouse X") * mouseSensitivity;
        pivotAngle -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

        cameraPivotTransform.localRotation = Quaternion.Euler(pivotAngle, camLookAngle, 0);
    }


    private void HandleCameraCollision(float delta)
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

    public void ChangeMouseSensitivity(float value)
    {
        mouseSensitivity = value;
    }

}



