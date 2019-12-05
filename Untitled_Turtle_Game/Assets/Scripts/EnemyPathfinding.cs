using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [Header("Target")]
    public GameObject target;

    public float speed = 2f;

    public float stopDist = 0.5f;

    public bool targetBool = false;
    public bool travelToSpawn = false;

    public bool enter = true;
    public bool stay = false;
    public bool exit = false;
    public bool collision = false;

    public bool canDespawnItem = false;

    EggControl egg;

    private void Awake()
    {
        this.transform.parent = GameObject.Find("Total Enemies Spawned").transform;
    }

    private void Start()
    {
        enter = false;

        egg = target.GetComponent<EggControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (exit && other.gameObject.tag == "Enemy Despawn")
            Destroy(transform.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        stay = true;

        if (stay)
            canDespawnItem = true;
    }

    private void OnTriggerExit(Collider other)
    {
        stay = false;
        canDespawnItem = false;
        collision = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        collision = true;
    }

    private void Update()
    {
        FindClosestItem();

        if (exit)
        {
            this.transform.parent = GameObject.Find("Enemy Despawn").transform;
        }
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
            else if (closestItem == null)
            {
                //GameManager.instance.GameLose();

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

                    if (canDespawnItem && collision)
                    {
                        GameUI.instance.text.text = "An Enemy is taking an " + target.name;

                        //FindObjectOfType<AudioManager>().Play("Alarm");

                        egg.TakingEgg();
                    }
                    else if (!canDespawnItem && !collision)
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

        GameManager.instance.GameLose();
    }

    public void FoundEgg()
    {
        egg = target.GetComponent<EggControl>();
        egg.atEgg = false;
    }
}
