﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class LaunchArcMesh : MonoBehaviour
{
    Mesh mesh;
    public float meshWidth;

    [Range(0f,20f)]
    public float velocity;
    [Range(0f,45f)]
    public float angle;
    public int resolution = 10;

    float gravity;
    float radianAngle;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        gravity = Mathf.Abs(Physics.gravity.y);
    }

    void OnValidate()
    {
        if (mesh != null && Application.isPlaying)
        {
            MakeArcMesh(CalcArcArray());
        }
    }

    void Start()
    {
        MakeArcMesh(CalcArcArray());
    }

    void MakeArcMesh(Vector3[] arcVerts)
    {
        mesh.Clear();
        Vector3[] vertices = new Vector3[(resolution + 1) * 2];
        int[] triangles = new int[resolution * 6 * 2];

        for (int i = 0; i <= resolution; i++)
        {
            vertices[i * 2] = new Vector3(meshWidth * 0.5f, arcVerts[i].y, arcVerts[i].x);
            vertices[i * 2 + 1] = new Vector3(meshWidth * -0.5f, arcVerts[i].y, arcVerts[i].x);

            if (i != resolution) 
            {
                triangles[i * 12] = i * 2;
                triangles[i * 12 + 1] = triangles[i * 12 + 4] = i * 2 + 1; 
                triangles[i * 12 + 2] = triangles[i * 12 + 3] = (i + 1) * 2; 
                triangles[i * 12 + 5] = (i + 1) * 2 + 1; 
                
                triangles[i * 12 + 6] = i * 2;
                triangles[i * 12 + 7] = triangles[i * 12 + 10] = (i + 1) * 2; 
                triangles[i * 12 + 8] = triangles[i * 12 + 9] = i * 2 + 1; 
                triangles[i * 12 + 11] = (i + 1) * 2 + 1; 
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
        
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

} // LaunchArcMesh

