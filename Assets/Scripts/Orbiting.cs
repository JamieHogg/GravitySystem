using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Orbiting : MonoBehaviour {

    LineRenderer lr;

    public GameObject orbitTarget;
    private GameObject child;

    [Range(1, 20)]
    public float semiMajorAxisA;
    [Range(0, 1)]
    public float eccentricity;
    [Range(0, 100)]
    public int progress;

    public bool progressDirection;

    [Range(0, 360)]
    public float rotateX;

    [Range(0, 360)]
    public float rotateY;

    [Range(0, 360)]
    public float rotateZ;

    public float speed;

    public bool move = false;

    private int length = 100;
    public Vector3[] points;
    private Vector3 dirNormalized;

    GravitySystemMKII mkii;


    // Use this for initialization
    void Awake () {
        lr = GetComponent<LineRenderer>();

        orbitTarget = GameObject.FindGameObjectWithTag("Star");

        CalculateEllipse();

        this.transform.position = points[progress];
        child = this.transform.GetChild(0).gameObject;

        this.transform.parent = orbitTarget.transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateEllipse();
        this.transform.parent = orbitTarget.transform.GetChild(0).transform;


        if (move)
        {
            movement();
        }
        else
        {
            child.transform.position = points[progress];
        }

        //child.transform.position = Vector3.Lerp(child.transform.position, child.transform.position + dirNormalized, speed * Time.deltaTime);

        float distance = Vector3.Distance(child.transform.position, orbitTarget.transform.GetChild(0).transform.position);
        speed = newtonsLawGravity(child.GetComponent<Rigidbody>().mass, orbitTarget.transform.GetChild(0).GetComponent<Rigidbody>().mass, distance);
    }

    void CalculateEllipse()
    {
        float xt = orbitTarget.transform.GetChild(0).transform.position.x;
        float yt = orbitTarget.transform.GetChild(0).transform.position.y;

        float a = semiMajorAxisA;
        float ae = semiMajorAxisA * eccentricity;

        float f = ae;
        float ab = a + ae;
        float b = (Mathf.Sqrt((a * a) - (f * f)));

        points = new Vector3[length + 1];
        for (int i = 0; i < length; i++)
        {
            float angle = ((float)i / (float)length) * 360 * Mathf.Deg2Rad;
            float x = Mathf.Sin(angle) * (a + ae);
            float y = Mathf.Cos(angle) * b;
            points[i] = new Vector3(x + (ae*2), y, 0f);

            Quaternion rotation = Quaternion.Euler(rotateX, rotateY, rotateZ);
            Matrix4x4 m = Matrix4x4.Rotate(rotation);
            points[i] = m.MultiplyPoint3x4(points[i]);
            points[i] = points[i] + orbitTarget.transform.GetChild(0).transform.position;
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

    void movement()
    {
        if (!progressDirection)
        {
            dirNormalized = (points[progress + 1] - child.transform.position).normalized;

            if (Vector3.Distance(child.transform.position, points[progress + 1]) < 0.1f)
            {
                progress++;
                if (progress == 100)
                {
                    progress = 0;
                }
            }
        }
        else if (progressDirection)
        {
            dirNormalized = (points[progress - 1] - child.transform.position).normalized;

            if (Vector3.Distance(child.transform.position, points[progress - 1]) < 0.1f)
            {
                progress--;
                if (progress == 0)
                {
                    progress = 100;
                }
            }
        }
        child.transform.position += dirNormalized * speed * Time.deltaTime;
    }
}