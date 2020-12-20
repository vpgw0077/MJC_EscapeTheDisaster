using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_PGW : MonoBehaviour
{
    public float rotSpeed;

    public GameObject Player;
    public GameObject MainCam;
    public float camera_width = -10f;
    public float camera_height = 4f;
    public float camera_fix = 3f;

    float camera_dist = 0f;

    Vector3 dir;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        MainCam = GameObject.FindGameObjectWithTag("MainCamera");

        camera_dist = Mathf.Sqrt(camera_width + camera_height * camera_height);
        dir = new Vector3(0, camera_height, camera_width).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 ray_target = transform.up * camera_height + transform.forward * camera_width;

        RaycastHit hit;
        Physics.Raycast(transform.position, ray_target, out hit, camera_dist);
        if(hit.point != Vector3.zero)
        {
            MainCam.transform.position = hit.point;
            MainCam.transform.Translate(dir * -1 * camera_fix);
        }
        else
        {
            MainCam.transform.localPosition = Vector3.zero;
            MainCam.transform.Translate(dir * camera_dist);
            MainCam.transform.Translate(dir * -1 * camera_fix);
        }
    
  
    }
}
