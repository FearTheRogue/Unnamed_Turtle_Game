using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Original Projectile Script https://www.youtube.com/watch?reload=9&v=03GHtGyEHas

public class Projectile : MonoBehaviour
{

    public Rigidbody bombPrefab;
    public GameObject cursor;
    public Transform firePoint;
    public LayerMask layer;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        LaunchProjectile();
    }

    void LaunchProjectile()
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit, 100f, layer))
        {
            cursor.SetActive(true);
            cursor.transform.position = hit.point + Vector3.up * 0.1f;

            Vector3 Vo = CalculateVelocity(hit.point, firePoint.position, 1.5f);

            // Set rotation of the cannon
            transform.rotation = Quaternion.LookRotation(Vo);

            if (Input.GetMouseButtonDown(0))
            {
                Rigidbody obj = Instantiate(bombPrefab, firePoint.position, Quaternion.identity);
                obj.velocity = Vo;
            }
        }
        else
        {
            cursor.SetActive(false);
        }
    }

    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        //  Define the distance X and Y first
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;

        // Create a float that represents our distance
        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        // Calculate the Velocity
        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        // Return the result
        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }


    //public float speed = 20f;
    //public int preditionStepsPerFrame = 6;
    //public Vector3 bombVelocity;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    bombVelocity = this.transform.forward * speed;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    Vector3 point1 = this.transform.position;
    //    float stepSize = 1.0f / preditionStepsPerFrame;

    //    for (float step = 0; step < 1; step += stepSize)
    //    {
    //        bombVelocity += Physics.gravity * stepSize * Time.deltaTime;
    //        Vector3 point2 = point1 + bombVelocity * stepSize * Time.deltaTime;

    //        Ray ray = new Ray(point1, point2 - point1);
    //        if (Physics.Raycast(ray, (point2 - point1).magnitude))
    //        {
    //            Debug.Log("Hit" + gameObject.name);
    //        }

    //        point1 = point2;
    //    }
    //    this.transform.position = point1;
    //}

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Vector3 startPos = this.transform.position;
    //    Vector3 preditedBombVelocity = bombVelocity;
    //    float stepSize = 0.01f;

    //    for (float step = 0; step < 1; step += stepSize)
    //    {
    //        preditedBombVelocity += Physics.gravity * stepSize;
    //        Vector3 point2 = startPos + preditedBombVelocity * stepSize;
    //        Gizmos.DrawLine(startPos, point2);
    //        startPos = point2;
    //    }
    //}
}
