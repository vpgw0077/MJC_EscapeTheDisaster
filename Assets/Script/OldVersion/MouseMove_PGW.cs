using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove_PGW : MonoBehaviour
{
    public float mouseSpeed;

    float mouseX;
    float mouseY;
    float minDistance = 3.0f;
    float maxDistance = 4.0f;
    float smooth = 10f;
    Vector3 dollyDir;
    Vector3 dollyDirAdjust;
    float distance;
    

    public Transform player, Target;


    // Start is called before the first frame update
    private void Awake()
    {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
        ThroughWall();
       

    }
    void CameraMove()
    { 
        mouseX += Input.GetAxis("Mouse X") * mouseSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * mouseSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);
        transform.LookAt(Target);

        player.rotation = Quaternion.Euler(0, mouseX, 0);
        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);

    }
    void ThroughWall()
    {
        Vector3 desiredCameraPos = transform.parent.TransformPoint(dollyDir * maxDistance);
        RaycastHit hit;
        if(Physics.Linecast(transform.parent.position, desiredCameraPos, out hit))
        {
            distance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
            
        }
        else
        {
            distance = maxDistance;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);


    }
}
