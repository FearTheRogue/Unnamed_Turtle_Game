using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowObject : MonoBehaviour
{
    public Rigidbody throwPrefab;
    public float speed = 5;
    public bool fireRate = true;
    public Text reloadingText;

    private void Start()
    {
        fireRate = true;
        reloadingText.text = "";
    }

    void Fire()
    {
        //fireRate = false;
        //Rigidbody obj = Instantiate(throwPrefab, transform.position, transform.rotation);
        //obj.velocity = transform.forward * speed;
    }

    void Update()
    {

        if (PauseMenu.GameIsPaused || GameManager.instance.isInfoPanelActive)
        {
            return;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && fireRate)
            {
                //Fire();
                StartCoroutine(Firing());
            }
        }
        
    }

    IEnumerator Firing()
    {
        fireRate = false;
        Rigidbody obj = Instantiate(throwPrefab, transform.position, transform.rotation);
        obj.velocity = transform.forward * speed;
        reloadingText.text = "Reloading";

        yield return new WaitForSeconds(.8f);

        reloadingText.text = "Ready To Fire!";
        fireRate = true;
    }
}
