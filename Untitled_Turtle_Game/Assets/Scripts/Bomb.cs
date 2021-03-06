﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Projectile Attributes")]

    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;
    public float damage = 2;
    public float modifier = 0;

    [Header("Explosion Effect")]

    public GameObject explosionEffect;
    //public GameObject bombDecal;

    [Header("Projectile Attributes 2.0")]
    public float distance;
    public float timeInAir;

    public float torque;

    float countdown;
    bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        float turn = Input.GetAxis("Horizontal");
        rb.AddTorque(transform.up * torque * turn);
    }

    void OnCollisionEnter(Collision other)
    {
        Explode();
    }

    void Explode()
    {
        //explosionFX.Play();

        Instantiate(explosionEffect, transform.position, transform.rotation);
       
        //Instantiate(bombDecal, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                //rb.AddExplosionForce(force, transform.position, radius);
                rb.AddExplosionForce(force, transform.position, radius, modifier);

                Debug.Log(rb.name);
            }

            EnemyControl enemy = nearbyObject.GetComponent<EnemyControl>();

            if(enemy != null)
            {
                enemy.Health(damage);
            }
        }

        Destroy(gameObject);

    }
}
