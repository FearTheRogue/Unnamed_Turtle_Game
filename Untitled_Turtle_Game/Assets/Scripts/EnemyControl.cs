using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class EnemyControl : MonoBehaviour
{
    [Header("Target")]
    public GameObject target;
    //public GameObject enemySpawn;
    [Header("Death Effect")]
    public GameObject enemyExplosion;

    [Header("Enemy Attributes")]
    public float speed = 2f;
    public Image healthBar;
    public float startHealth = 5;
    public float currentHealth;
    public Canvas enemyCanvas;

    [Header("Pathfinding Attributes")]
    public float stopDist = 0.5f;
    public bool targetBool = false;
    public bool travelToSpawn = false;

    [Header("Booleans")]
    public bool enter = true;
    public bool stay = false;
    public bool exit = false;
    public bool collision = false;

    public bool canDespawnItem = false;

    EggControl egg;

    void Awake()
    {
        this.transform.parent = GameObject.Find("Total Enemies Spawned").transform;
        //this.transform.parent = GameObject.Find("Enemy Spawn").transform;
        //target = GameObject.Find("Enemy Despawn");
    }

    void Start()
    {
        currentHealth = startHealth;

        enter = false;
        //target = null;
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
    }

    void OnTriggerStay(Collider other)
    {
        stay = true;

        if (stay)
            canDespawnItem = true;
    }

    void OnCollisionEnter(Collision other)
    {
        collision = true;
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
                }
            }
            if (closestItem != null)
            {
                Debug.DrawLine(this.transform.position, closestItem.transform.position, Color.red);
                TravelToClosestItem(closestItem);
            }
            else if(closestItem == null)
            {
                GameManager.instance.GameLose();

                Destroy(gameObject);
                closestItem = GameObject.Find("Enemy Despawn");
                Debug.DrawLine(this.transform.position, closestItem.transform.position, Color.black);
                DespawnEnemy(closestItem);                
            }
        } 
    }

    void TravelToClosestItem(GameObject currentClosesItem)
    {
        target = currentClosesItem.gameObject;

        if (target != null)
        {
            targetBool = true;
            travelToSpawn = false;

            if (targetBool)
            {
                transform.LookAt(target.transform.position);

                if ((transform.position - target.transform.position).magnitude >= stopDist)
                {
                    transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);

                    egg = target.GetComponent<EggControl>();

                    if (canDespawnItem)
                    {
                        //GameUI.instance.text.text = "An Enemy is taking an " + target.name;
                        //FindObjectOfType<AudioManager>().Play("Alarm");

                        egg.TakingEgg();

                    }
                    else
                    {
                        egg.atEgg = false;
                        //FindObjectOfType<AudioManager>().Stop("Alarm");
                    }
                }
            }
        }
    }

    void DespawnEnemy(GameObject currentItem)
    {
        target = currentItem;

        transform.LookAt(target.transform.position);

        transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
        exit = true;

        //GameManager.instance.GameLose();
    }


    void Update()
    {
        FindClosestItem();

        if (exit)
        {
            this.transform.parent = GameObject.Find("Enemy Despawn").transform;

            //GameManager.instance.GameLose();

            GameUI.instance.text.text = "GAME OVER, NO EGGS LEFT";
        }

        enemyCanvas.transform.LookAt(Camera.main.transform.position);
    }

    public void PlayAlarm()
    {
        //if()
    }

    public void Health(float amount)
    {
        currentHealth -= amount;

        healthBar.fillAmount = currentHealth / startHealth; 

        if(currentHealth <= 0) 
        {
            egg = target.GetComponent<EggControl>();
            egg.atEgg = false;

            Instantiate(enemyExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
