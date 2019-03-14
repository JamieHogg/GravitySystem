﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Orbiting : MonoBehaviour {

    LineRenderer lr;
    GravitySystem gravitySystem;

    public GameObject orbitTarget;
    private GameObject child;

    [Range(0, 20)]
    public float semiMajorAxisA;
    //public float semiMajorAxisB;
    [Range(0, 1)]
    public float eccentricity;
    [Range(0, 100)]
    public int progress;
    [Range(0, 360)]
    public int speed;

    [Range(0, 360)]
    public int rotateZ;

    [Range(0, 5)]
    public float maxSpeed;

    private int length = 100;
    public Vector3[] points;

    // Use this for initialization
    void Awake () {
        lr = GetComponent<LineRenderer>();
        CalculateEllipse();

        this.transform.position = points[progress];
        child = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update ()
    {
        CalculateEllipse();

        Vector3 dirNormalized = (points[progress+1] - child.transform.position).normalized;
        if (Vector3.Distance(child.transform.position, points[progress+1]) < 0.1f)
        {
            progress++;
            if (progress == 100)
            {
                progress = 0;
            }
        }
        child.transform.position = child.transform.position + dirNormalized * maxSpeed * Time.deltaTime;
        //child.transform.position = points[progress];
    }

    void CalculateEllipse()
    {
        float xt = orbitTarget.transform.position.x;
        float yt = orbitTarget.transform.position.y;

        float a = semiMajorAxisA;
        float ae = semiMajorAxisA * eccentricity;

        float f = ae;
        float ab = a + ae;
        float b = (Mathf.Sqrt((a * a) - (f * f)));
        //Debug.Log(b);

        points = new Vector3[length + 1];
        for (int i = 0; i < length; i++)
        {
            float angle = ((float)i / (float)length) * 360 * Mathf.Deg2Rad;
            float x = xt + Mathf.Sin(angle) * (a + ae);
            float y = yt + Mathf.Cos(angle) * b;
            points[i] = new Vector3(x + (ae*2), y, 0f);
        }
        points[length] = points[0];


        lr.positionCount = length + 1;
        lr.SetPositions(points);
    }

    void ActualOrbit()
    {
        progress++;
        if (progress == 100)
        {
            progress = 0;
        }
    }
}