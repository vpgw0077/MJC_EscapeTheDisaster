using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFace_PGW : MonoBehaviour
{
    public float mouseSensitivity;
    public Transform FaceTransform;
    float lookAngle;
    float pivotAngle;
    public float minimumPivot = -35;
    public float maximumPivot = 35;

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
        transform.localRotation = Quaternion.Euler(pivotAngle, 0, 0);
    }
    private void Update()
    {
        float delta = Time.fixedDeltaTime;
        HandleCameraRotation(delta, mouseSensitivity, mouseSensitivity);
    }
}
