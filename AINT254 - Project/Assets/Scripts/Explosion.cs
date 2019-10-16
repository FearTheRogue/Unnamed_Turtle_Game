using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius = 4;
    public float force = 8;

    // Start is called before the first frame update
    void Start()
    {
        Collider[] affected = Physics.OverlapSphere(transform.position, 5);

        foreach(var col in affected)
        {
            if(col.GetComponent<Rigidbody>() != null)
            {
                col.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius, force * 0.5f, ForceMode.Impulse);
            }
        }
        Destroy(gameObject, 5f);
    }
}
