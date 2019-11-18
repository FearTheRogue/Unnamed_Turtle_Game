using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class LaunchArcRenderer : MonoBehaviour
{
    public static LaunchArcRenderer instance;

    LineRenderer lr;

    [Range(0f, 20f)]
    public float velocity;
    public float angle;
    public int resolution = 10;

    float gravity;
    float radianAngle;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        gravity = Mathf.Abs(Physics.gravity.y);
    }

    void OnValidate()
    {
        if(lr != null && Application.isPlaying)
        {
            RenderArc();
        }
    }

    void Start()
    {
        instance = this;
        RenderArc();
    }

    public void RenderArc()
    {
        if (velocity == 0)
        {
            return;
        }
        else
        {

            lr.positionCount = resolution + 1;
            lr.SetPositions(CalcArcArray());
        }
    }
    Vector3[] CalcArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];
        radianAngle = Mathf.Deg2Rad * angle;
        float maxDist = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / gravity;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalcArcPoint(t, maxDist);
        }
        return arcArray;
    }

    Vector3 CalcArcPoint(float t, float maxDist)
    {
        float x = t * maxDist;
        float y = x * Mathf.Tan(radianAngle) - ((gravity * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));

        return new Vector3(x, y);
    }

} // LaunchArcRenderer
