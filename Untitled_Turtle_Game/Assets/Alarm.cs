using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Alarm : MonoBehaviour
{
    public AudioSource aSource;

    public bool nothing;

    void Start()
    {
        aSource.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            nothing = false;

            //Debug.Log("Yes, this is true");
            if (aSource.isPlaying)
            {
                Debug.Log("Audio is already playing");
                return;
            }
            else
            {
                aSource.Play();
                Debug.Log("Alarm is playing");
            }
        } 
    }

    void Update()
    {
        if (nothing)
        {
            aSource.Stop();
        }

    }

    void OnTriggerStay(Collider other)
    {
        //nothing = true;
        Debug.Log("nothings in here");
    }
}
