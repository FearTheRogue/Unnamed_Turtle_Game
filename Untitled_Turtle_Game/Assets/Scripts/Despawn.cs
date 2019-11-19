using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    public float timeToDie = 15000f;

    void Start()
    {
        DestroyObject();
    }

    void DestroyObject()
    {
        Destroy(gameObject, 5);
    }

}
