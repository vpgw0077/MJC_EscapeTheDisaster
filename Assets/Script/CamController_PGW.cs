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
    [SerializeField] private float minimumCollisionOffset = 0.2f; // 플레이어와 카메라의 최소거리

    private Transform myTransform = null;
    private Vector3 cameraTransformPosition;
    [SerializeField]private float targetDistance = 0f;
    [SerializeField] private float distance;
    private float defaultDistance = 0f;
    private float camLookAngle = 0f;
    private float pivotAngle = 0f;

    private void Awake()
    {
        myTransform = transform;
        defaultDistance = cameraTransform.localPosition.z;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate() // Update를 쓰면 지터링이 발생하기 때문에 Update를 쓰지 않음
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
        targetDistance = defaultDistance;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivotTransform.position, cameraSphereRadius, direction, out hit, Mathf.Abs(targetDistance)))
        {
            distance = Vector3.Distance(cameraPivotTransform.position, hit.point);
            targetDistance = -(distance - cameraCollisionOffset); // 값이 음수인 이유는 카메라는 플레이어의 뒤쪽 즉 -z값을 가져야하므로
                                                                  // cameraCollisionOffset이 클수록 카메라가 플레이어에 더 가까워짐

        }


        if (Mathf.Abs(targetDistance) < minimumCollisionOffset)
        {
            targetDistance = -minimumCollisionOffset;

        }
        cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetDistance, delta / 0.2f);
        cameraTransform.localPosition = cameraTransformPosition;

    }

    public void ChangeMouseSensitivity(float value)
    {
        mouseSensitivity = value;
    }

}



