using UnityEngine;

public class EnemyEnter : MonoBehaviour
{
    public bool isPlaying = false;

    void OnTriggerEnter(Collider other)
    {
        if (isPlaying)
        {
            return;
        }
        else
        {
            if (other.tag == "Enemy")
            {
                isPlaying = true;

                FindObjectOfType<AudioManager>().Play("Alarm");
            }
            else
            {
                FindObjectOfType<AudioManager>().Stop("Alarm");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            FindObjectOfType<AudioManager>().Stop("Alarm");
            isPlaying = false;
        }
    }

    private void Update()
    {
        //this.transform.childCount
    }

}
