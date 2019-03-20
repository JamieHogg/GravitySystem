using System.Collections;
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
    public int rotateX;

    [Range(0, 360)]
    public int rotateY;

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

        //maxSpeed = (child.GetComponent<Rigidbody>().mass + orbitTarget.transform.GetChild(0).GetComponent<Rigidbody>().mass);
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

        float distance = Vector3.Distance(child.transform.position, orbitTarget.transform.GetChild(0).transform.position);
        maxSpeed = newtonsLawGravity(child.GetComponent<Rigidbody>().mass, orbitTarget.transform.GetChild(0).GetComponent<Rigidbody>().mass, distance);
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

            Quaternion rotation = Quaternion.Euler(rotateX, rotateY, rotateZ);
            Matrix4x4 m = Matrix4x4.Rotate(rotation);
            points[i] = m.MultiplyPoint3x4(points[i]);
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

    float newtonsLawGravity(float mass1, float mass2, float distance)
    {
        float force = 1 * ((mass1 * mass2) / Mathf.Pow(distance, 2));
        return force;
    }
}