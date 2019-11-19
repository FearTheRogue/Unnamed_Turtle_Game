using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public float jumpForce = 5;

    void Start()
    {

    }
    
    void Update()
    {
        // Moves player forwards
        //if (Input.GetKey(KeyCode.W))
       // {
           // transform.position += Vector3.forward.normalized * Time.deltaTime * speed;
       // }
        // Moves player backwards
        //if (Input.GetKey(KeyCode.S))
        //{
        //    transform.position -= Vector3.forward.normalized * Time.deltaTime * speed;
        //}
        // Moves player left
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= Vector3.right.normalized * Time.deltaTime * speed;
        }
        // Moves player right
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right.normalized * Time.deltaTime * speed;
        }

        // player jumps
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += Vector3.up.normalized * Time.deltaTime * jumpForce;
        }
    }
} // Player
