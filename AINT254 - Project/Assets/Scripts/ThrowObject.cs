using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public Rigidbody throwPrefab;
    public float speed = 5;
    public bool fireRate = true;

    private void Start()
    {
        fireRate = true;
    }

    void Fire()
    {
        //fireRate = false;
        //Rigidbody obj = Instantiate(throwPrefab, transform.position, transform.rotation);
        //obj.velocity = transform.forward * speed;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && fireRate)
        {
            //Fire();
            StartCoroutine(Firing());
        }
    }

    IEnumerator Firing()
    {
        fireRate = false;
        Rigidbody obj = Instantiate(throwPrefab, transform.position, transform.rotation);
        obj.velocity = transform.forward * speed;
        yield return new WaitForSeconds(.8f);
        fireRate = true;
    }
}
