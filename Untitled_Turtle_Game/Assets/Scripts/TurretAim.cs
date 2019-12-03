using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAim : MonoBehaviour
{
    public float speed = 5f;

    float pos = 0.0f;

    public GameObject firePoint;
    public GameObject barrelJoint;
    public Vector3 trajectory;
    public float bombSpeed = 20f;
    public int preditionStepsPerFrame = 6;

    // Update is called once per frame
    void Update()
    {
        MouseAim();

       
        //trajectory.x 

    }

    void MouseAim()
    {
        // pos += speed * Input.GetAxis("Mouse X");

        //transform.eulerAngles = new Vector3(0.0f, pos, 0.0f);

        //Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //float midPoint = (-transform.position - Camera.main.transform.position).magnitude * 0.5f;

        //transform.LookAt(mouseRay.origin + mouseRay.direction * midPoint);
        // transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 point1 = firePoint.transform.position;

        trajectory.x = firePoint.transform.position.z;

        Vector3 preditedBombVelocity = trajectory;
        float stepSize = 0.01f;

        for (float step = 0; step < 1; step += stepSize)
        {
            preditedBombVelocity += Physics.gravity * stepSize;
            Vector3 point2 = point1 + preditedBombVelocity * stepSize;
            Gizmos.DrawLine(point1, point2);
            point1 = point2;
        }
    }
}
