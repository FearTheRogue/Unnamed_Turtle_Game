using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosion;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter(Collision other)
    {
        Instantiate(explosion, transform.position, transform.rotation);

        if (other.transform.tag == "Enemy")
        {
            Destroy(other.gameObject);
                
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
