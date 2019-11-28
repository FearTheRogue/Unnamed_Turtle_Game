using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryPath : MonoBehaviour
{
    public GameObject TrajectoryPointPrefab;
    public GameObject BombPrefeb;

    private GameObject bomb;
    private bool isPressed, isBombThrown;
    private float power = 25;
    private int numOfTrajectoryPoints = 30;
    private List<GameObject> trajectoryPoints;

    private void Start()
    {
        trajectoryPoints = new List<GameObject>();
        isPressed = isBombThrown = false;

        for (int i = 0; i < numOfTrajectoryPoints; i++)
        {
            GameObject dot = (GameObject)Instantiate(TrajectoryPointPrefab);
            //dot.renderer.enabled = false;
            trajectoryPoints.Insert(i, dot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isBombThrown)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            isPressed = true;
            if (!bomb)
                createBomb();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isPressed = false;
            if (!isBombThrown)
            {
                throwBomb();
            }
        }
        if (isPressed)
        {
            Vector3 vel = GetForceFrom(bomb.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

    }

    void createBomb()
    {
        bomb = (GameObject)Instantiate(BombPrefeb);
        Vector3 pos = transform.position;
        pos.z = 1;
        bomb.transform.position = pos;
        bomb.SetActive(false);  
    }

    void throwBomb()
    {
        bomb.SetActive(true);
        bomb.rigidbody.useGravity = true;
    }

    private Vector2 GetForceFrom(Vector3 fromPos, Vector3 toPos)
    {
        return (new Vector2(toPos.x,toPos.y)) - new Vector2(fromPos.x,fromPos.y))*power;
    }


}
