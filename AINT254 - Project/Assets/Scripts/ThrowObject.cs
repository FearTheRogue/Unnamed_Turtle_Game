using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public Rigidbody throwPrefab;
    public float speed = 5;

    //private bool holdingItem = true;

    void Start()
    {
        throwPrefab.GetComponent<Rigidbody>().useGravity = false;
    }

    void Update()
    {
       // if (holdingItem)
        //{
            //Rigidbody o = Instantiate(throwPrefab, transform.position, transform.rotation);
            //o.velocity = transform.forward * speed;

            if (Input.GetMouseButtonDown(0))
            {
                //holdingItem = false;
                throwPrefab.GetComponent<Rigidbody>().useGravity = true;
                //Instantiate(throwPrefab, transform.position, transform.rotation);
                //throwPrefab.GetComponent<Rigidbody>().AddForce(throwPrefab.position * speed);

                Rigidbody o = Instantiate(throwPrefab, transform.position, transform.rotation);
                o.velocity = transform.forward * speed;

            }
       // }
    }
}
