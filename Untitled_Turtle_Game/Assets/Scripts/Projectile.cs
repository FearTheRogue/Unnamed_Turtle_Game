using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Original Projectile Script https://www.youtube.com/watch?reload=9&v=03GHtGyEHas

public class Projectile : MonoBehaviour
{

    public GameObject bombPrefab;
    public GameObject cursor;
    public Transform firePoint;
    public LayerMask layer;

    public float projDistance;
    public float projTime;
    public int weaponIndex;

    public AudioSource firingFX;

    public GameObject selectedTarget;

    private Camera cam;

    public bool fireRate = true;
    public Text reloadingText;

    Vector3 Vo;

    void Awake()
    {
        projDistance = bombPrefab.GetComponent<Bomb>().distance;
        projTime = bombPrefab.GetComponent<Bomb>().timeInAir;

        firingFX = GetComponent<AudioSource>();
    }

    void Start()
    { 
        cam = Camera.main;

        reloadingText.text = "";
    }

    void Update()
    {
        if (PauseMenu.GameIsPaused || GameManager.instance.isInfoPanelActive)
        {
            return;
        }
        else
        {
            LaunchProjectile();
        }
    }

    void LaunchProjectile()
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit, projDistance, layer))
        {
            cursor.SetActive(true);
            cursor.transform.position = hit.point + Vector3.up * 0.1f;

            Vo = CalculateVelocity(hit.point, firePoint.position, projTime);

            // Set rotation of the cannon
            transform.rotation = Quaternion.LookRotation(Vo);

            if (Input.GetMouseButtonDown(0) && fireRate)
            {
                StartCoroutine(Firing());
            }
        }
        else
        {
            cursor.SetActive(false);
        }
    }

    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        //  Define the distance X and Y first
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;

        // Create a float that represents our distance
        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        // Calculate the Velocity
        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        // Return the result
        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        Debug.DrawLine(firePoint.position,result);

        return result;
    }

    IEnumerator Firing()
    {
        fireRate = false;

        firingFX.PlayOneShot(firingFX.clip);

        Instantiate(selectedTarget, cursor.transform.position, cursor.transform.rotation);

        Rigidbody obj = Instantiate(bombPrefab.GetComponent<Rigidbody>(), firePoint.position, Quaternion.identity);
        obj.velocity = Vo;

        reloadingText.text = "Reloading...";

        yield return new WaitForSeconds(0.8f);

        reloadingText.text = "Ready To Fire!";
        fireRate = true;
    }

    //public float speed = 20f;
    //public int preditionStepsPerFrame = 6;
    //public Vector3 bombVelocity;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    bombVelocity = this.transform.forward * speed;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    Vector3 point1 = this.transform.position;
    //    float stepSize = 1.0f / preditionStepsPerFrame;

    //    for (float step = 0; step < 1; step += stepSize)
    //    {
    //        bombVelocity += Physics.gravity * stepSize * Time.deltaTime;
    //        Vector3 point2 = point1 + bombVelocity * stepSize * Time.deltaTime;

    //        Ray ray = new Ray(point1, point2 - point1);
    //        if (Physics.Raycast(ray, (point2 - point1).magnitude))
    //        {
    //            Debug.Log("Hit" + gameObject.name);
    //        }

    //        point1 = point2;
    //    }
    //    this.transform.position = point1;
    //}

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Vector3 startPos = this.transform.position;
    //    Vector3 preditedBombVelocity = bombVelocity;
    //    float stepSize = 0.01f;

    //    for (float step = 0; step < 1; step += stepSize)
    //    {
    //        preditedBombVelocity += Physics.gravity * stepSize;
    //        Vector3 point2 = startPos + preditedBombVelocity * stepSize;
    //        Gizmos.DrawLine(startPos, point2);
    //        startPos = point2;
    //    }
    //}
}
