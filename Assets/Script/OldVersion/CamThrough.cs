using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamThrough : MonoBehaviour
{
    public float minDistance = 3.0f;
    public float maxDistance = 4.0f;
    public float smooth = 10f;
    Vector3 dollyDir;
    Vector3 dollyDirAdjust;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        ThroughWall();
    }
    void ThroughWall()
    {
        Vector3 desiredCameraPos = transform.parent.TransformPoint(dollyDir * maxDistance);
        RaycastHit hit;
        if (Physics.Raycast(transform.parent.position, desiredCameraPos, out hit))
        {
          
           distance = Mathf.Clamp((hit.distance*0.9f), minDistance, maxDistance);

        }
        else
        {
            distance = maxDistance;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);


    }
}
