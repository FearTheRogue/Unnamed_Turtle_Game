using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmControl : MonoBehaviour
{
    public Transform arm;
    public float min;
    public float max;

    public float elevation;

    void Update()
    {
        float v = Input.GetAxis("Vertical");
        elevation = Mathf.Clamp(elevation + v, min, max);
        arm.localRotation = Quaternion.Euler(elevation, -180, 0);
    }
}
