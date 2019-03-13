using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Orbiting : MonoBehaviour {

    LineRenderer lr;

    public GameObject orbitTarget;

    public float semiMajorAxisA;
    //public float semiMajorAxisB;
    [Range(0, 1)]
    public float eccentricity;
    [Range(0, 100)]
    public int progress;

    private int length = 100;
    public Vector3[] points;

    // Use this for initialization
    void Awake () {
        lr = GetComponent<LineRenderer>();
        CalculateEllipse();

        this.transform.position = points[progress];
    }

    // Update is called once per frame
    void Update ()
    {
        //ActualOrbit();
        CalculateEllipse();
        this.transform.position = points[progress];
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
        Debug.Log(b);

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
        int setSpeed = 4;
        float speed = Time.deltaTime * 1000 * setSpeed;

        this.transform.position = Vector3.Lerp(this.transform.position, points[progress + 1], speed);
        progress+=  1;
    }
}
