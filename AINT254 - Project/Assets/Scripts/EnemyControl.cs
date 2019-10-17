using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    public GameObject target;
    public GameObject enemySpawn;
    public float speed = 2f;

    public float stopDist = 0.5f;

    public bool targetBool = false;
    public bool travelToSpawn = false;

    public bool enter = true;
    public bool stay = false;
    public bool exit = false;
    public bool collision = false;

    public bool canDespawnItem = false;

    public int health = 1;

    void Start()
    {
        enemySpawn = GameObject.Find("Enemy Despawn");
        enter = false;
        target = null;
        //target = GameObject.FindGameObjectsWithTag("item");
    }

    void OnTriggerEnter(Collider other)
    {
        if(exit)
        Destroy(transform.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        stay = false;
        canDespawnItem = false;
        collision = false;

           // target.GetComponentInParent<SphereCollider>().enabled = false;
            //Debug.Log("Exiting");
    }

    void OnTriggerStay(Collider other)
    {
        stay = true;

        if (stay)
            canDespawnItem = true;
			//Debug.Log("isStaying");
    }

    void OnCollisionEnter(Collision other)
    {
        collision = true;
        //Debug.Log("Collision with " + other.collider.name);
    }

    void FindClosestItem()
    {
        float distanceToClosestItem = Mathf.Infinity;
        GameObject closestItem = null;
        GameObject[] allItems = GameObject.FindGameObjectsWithTag("item");

        if (allItems != null)
        {
            foreach (GameObject currentItem in allItems)
            {
                float distanceToItem = (currentItem.transform.position - this.transform.position).sqrMagnitude;
                if (distanceToItem < distanceToClosestItem)
                {
                    distanceToClosestItem = distanceToItem;
                    closestItem = currentItem;
                   //Debug.Log("finding item " + closestItem);
                }
            }
            if (closestItem != null)
            {
                Debug.DrawLine(this.transform.position, closestItem.transform.position, Color.red);
                TravelToClosestItem(closestItem);
            }
            else if(closestItem == null)
            {
                closestItem = enemySpawn;
                Debug.DrawLine(this.transform.position, closestItem.transform.position, Color.black);
                DespawnEnemy();
            }
        } 
    }

    void TravelToClosestItem(GameObject currentClosesItem)
    {
        //Debug.Log("the current target is " + currentClosesItem.name);
        target = currentClosesItem.gameObject;

        if (target != null)
        {
            targetBool = true;
            travelToSpawn = false;

            if (targetBool)
            {
                Debug.Log(target.name);
                transform.LookAt(target.transform.position);

                if ((transform.position - target.transform.position).magnitude >= stopDist)
                {
                    transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);

                    if (canDespawnItem && collision)
                    {
                        GameUI.instance.text.text = "An Enemy is taking an " + target.name;
                        Destroy(target.gameObject, 10f);
                    }
                }
            }
        }
    }

    void DespawnEnemy()
    {
        //targetBool = false;
        //travelToSpawn = true;

       //if (travelToSpawn)
        //{
            transform.LookAt(enemySpawn.transform.position);

            transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
            exit = true;
       // }
    }


    void Update()
    {
        FindClosestItem();

        if (exit)
        {
            GameUI.instance.text.text = "GAME OVER, NO EGGS LEFT";
        }
            //FindTarget();

        //if(stay == false)
        //{
        //    GameUI.instance.text.text = "No Enemies taking an Egg";
        //}

        //for (int i = 0; i < target.Length; i++)
        //{                   
        //    if (target[i] != null)
        //    {
        //        targetBool = true;
        //        travelToSpawn = false;

        //        if (targetBool)
        //        {
        //            Debug.Log(target[i].name);
        //            transform.LookAt(target[i].transform.position);

        //            if ((transform.position - target[i].transform.position).magnitude >= stopDist)
        //            {
        //                transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);

        //                if (canDespawnItem && stay)
        //                {
        //                    GameUI.instance.text.text = "An Enemy is taking an " + target[i].name;
        //                    Destroy(target[i].gameObject, 5f);
        //                }
        //            }
        //        }
        //    }
        //}

        //if (target == null)
        //{
        //    targetBool = false;
        //    travelToSpawn = true;

        //    if (travelToSpawn)
        //    {
        //        transform.LookAt(enemySpawn.transform.position);

        //        transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
        //        exit = true;
        //    }
        //}
    }
}
