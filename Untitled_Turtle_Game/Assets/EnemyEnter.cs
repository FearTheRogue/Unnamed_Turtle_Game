using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnter : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            FindObjectOfType<AudioManager>().Play("Alarm");
        }
        else
        {
            FindObjectOfType<AudioManager>().Stop("Alarm");
        }
    }

    void OnTriggerExit(Collider other)
    {
        FindObjectOfType<AudioManager>().Stop("Alarm");
    }
}
