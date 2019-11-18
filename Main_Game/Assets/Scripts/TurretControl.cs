using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour
{
    public static TurretControl instance;

    public Transform turret;
    public Transform barrel;
    public float eleMin;
    public float eleMax;
    public float speed = 1f;

    float elevation = 0;
    float v;

    public Vector3 prevPos;
    public Vector3 newPos;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * speed, 0);

        v = Input.GetAxis("Vertical") * speed;
        elevation = Mathf.Clamp(elevation + v, eleMin, eleMax);
        turret.localRotation = Quaternion.Euler(elevation, 0, 0);
    }

    //public void TurretRecall()
    //{
    //    barrel.GetComponent<Transform>();
    //    prevPos = new Vector3(0, 0, barrel.localPosition.z + 1);
        
    //    newPos = prevPos(0, 0, barrel.localPosition.z + 1);
    //}
}
