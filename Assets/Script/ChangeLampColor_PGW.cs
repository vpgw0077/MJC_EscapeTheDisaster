using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLampColor_PGW : MonoBehaviour
{
    [SerializeField] private MeshRenderer doorLamp = null;

    public Material redLamp = null;
    public Material greenLamp = null;

    // Start is called before the first frame update
    void Start()
    {
        doorLamp.material = redLamp;
    }

    public void ChangeLampColor(Material color)
    {
        doorLamp.material = color;
    }

}
