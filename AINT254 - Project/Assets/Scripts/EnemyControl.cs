using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public Transform target;
    public Transform enemySpawn;
    public float speed = 2f;

    public float stopDist = 0.5f;

    public bool targetBool = false;
    public bool travelToSpawn = false;

    public bool enter = true;
    public bool stay = false;
    public bool exit = false;

    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(exit)
        Destroy(transform.gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        if (stay)
        {
            Debug.Log("Staying");
        }
    }

    void Update()
    {
        if (target != null)
        {
            targetBool = true;
            travelToSpawn = false;

            if (targetBool)
            {
                transform.LookAt(target.position);

                if ((transform.position - target.position).magnitude > stopDist)
                {
                    transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
                    Destroy(target.gameObject, 10f);
                } else
                {
                    stay = true;
                    return;
                }
            }
        }

        if (target == null)
        {
            targetBool = false;
            travelToSpawn = true;

            if (travelToSpawn)
            {
                transform.LookAt(enemySpawn.position);

                transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
                exit = true;
            }
        }
    }

}
