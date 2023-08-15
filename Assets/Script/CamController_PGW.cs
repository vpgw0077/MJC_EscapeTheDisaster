using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController_PGW : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform cameraPivotTransform;

    [SerializeField] private SkinnedMeshRenderer targetRender;

    [SerializeField] private bool changeTransparency = true;

    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float lookSpeed = 0.1f;
    [SerializeField] private float followSpeed = 0.1f;
    [SerializeField] private float pivotSpeed = 0.03f;
    [SerializeField] private float minimumPivot = -35;
    [SerializeField] private float maximumPivot = 35;
    [SerializeField] private float cameraSphereRadius = 0.2f;
    [SerializeField] private float cameraCollisionOffset = 0.2f;
    [SerializeField] private float minimumCollisionOffset = 0.2f;

    private Transform myTransform;
    private Vector3 cameraTransformPosition;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private float targetPosition;
    private float defaultPosition;
    private float lookAngle;
    private float pivotAngle;

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
    private void FollowTarget(float delta)
    {
        Vector3 targetPosition = Vector3.Lerp(myTransform.position, targetTransform.position, delta / followSpeed);
        myTransform.position = targetPosition;

        HandleCameraCollision(delta);
    }

    private void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
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

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;

        FollowTarget(delta);
        HandleCameraRotation(delta, mouseSensitivity, mouseSensitivity);
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


}



