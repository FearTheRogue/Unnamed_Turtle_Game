using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public Rigidbody throwPrefab;
    public float speed = 5;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Rigidbody o = Instantiate(throwPrefab, transform.position, transform.rotation);
            o.velocity = transform.forward * speed;
            //TurretControl.instance.TurretRecall();
        }
    }
}
