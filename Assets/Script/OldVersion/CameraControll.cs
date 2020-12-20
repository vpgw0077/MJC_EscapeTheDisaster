using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public float CameraMoveSpeed = 120f;
    public GameObject CameraFollowobj;
    public Transform player;
    Vector3 FollowPos;
    public float clampAngle = 80f;
    public float inputSensitivity = 150f;
    public GameObject Cameraobj;
    public GameObject Playerobj;
    public float camDistanceXToPlayer;
    public float camDistanceYToPlayer;
    public float camDistanceZToPlayer;
    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputZ;
    public float smoothX;
    public float smoothY;
    float rotY = 0f;
    float rotX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollowobj = GameObject.FindGameObjectWithTag("Cam");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        float inputX = Input.GetAxis("RightStickHorizontal");
        float inputZ = Input.GetAxis("RightStickVertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        finalInputX = inputX + mouseX;
        finalInputZ = inputZ + mouseY;

        rotY += finalInputX * inputSensitivity * Time.deltaTime;
        rotX -= finalInputZ * inputSensitivity * Time.deltaTime;

        player.rotation = Quaternion.Euler(0, rotY, 0);
        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = localRotation;
    }

    private void LateUpdate()
    {
        CameraUpdate();
    }
    void CameraUpdate()
    {
        Transform target = CameraFollowobj.transform;

        float step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }


}
