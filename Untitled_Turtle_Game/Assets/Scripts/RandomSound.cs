using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{

    public AudioSource audioSource;

    public AudioClip[] audioClipArray;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Start()
    {
        audioSource.clip = audioClipArray[Random.Range(0, audioClipArray.Length)];
        audioSource.PlayOneShot(audioSource.clip);
    }
}
