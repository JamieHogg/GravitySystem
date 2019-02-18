using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySystem : MonoBehaviour {

    public float gravityConstant;
    public Material trailMatte;

    [HideInInspector] public List<GameObject> nbodyObjs;
    [HideInInspector] public List<nbodyType> types;
    [HideInInspector] public List<float> masses;
    [HideInInspector] public List<Vector3> velocities;

    [HideInInspector] public List<GameObject> orbitObjects;

    // Use this for initialization
    void Awake ()
    {
        nbody[] nbodys = GameObject.FindObjectsOfType<nbody>();

        foreach (nbody n in nbodys)
        {
            nbodyObjs.Add(n.gameObject);
            masses.Add(n.mass);
            velocities.Add(n.velocity);
            types.Add(n.type);
            orbitObjects.Add(n.orbitObject);
        }

        int count = 0;
        foreach (GameObject n in nbodyObjs)
        {
            GameObject child = n.transform.Find("Shape").gameObject;
            addTrail(child);

            Rigidbody rb = child.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.mass = masses[count];
            rb.AddForce(velocities[count]);

            if (n.name == "Star")
            {
                rb.isKinematic = true;
            }
            count++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        foreach (GameObject n in nbodyObjs)
        {
            Rigidbody nRb = getChildRb(n);
            foreach (GameObject i in nbodyObjs)
            {
                if (n != i)
                {
                    Rigidbody iRb = getChildRb(i);
                    Vector3 difference = iRb.transform.position - nRb.transform.position;

                    float distance = difference.magnitude;
                    Vector3 gravityDirection = difference.normalized;

                    Vector3 gravityVector = gravityDirection * newtonsLawGravity(nRb.mass, iRb.mass, distance);

                    n.transform.Find("Shape").gameObject.transform.GetComponent<Rigidbody>().AddForce(gravityVector);

                    GameObject child = n.transform.Find("Shape").gameObject;
                    //Debug.Log(gravityVector);
                }
            }
        }
        */
        int count = 0;
        foreach (GameObject n in nbodyObjs)
        {
            if (orbitObjects[count] != null)
            {
                Rigidbody nRb = getChildRb(n);
                Rigidbody tRb = getChildRb(orbitObjects[count]);

                Vector3 difference = tRb.transform.position - nRb.transform.position;

                float distance = difference.magnitude;
                Vector3 gravityDirection = difference.normalized;

                Vector3 gravityVector = gravityDirection * newtonsLawGravity(nRb.mass, tRb.mass, distance);

                n.transform.Find("Shape").gameObject.transform.GetComponent<Rigidbody>().AddForce(gravityVector);

                GameObject child = n.transform.Find("Shape").gameObject;
            }
            count++;
        }
    }

    float newtonsLawGravity(float mass1, float mass2, float distance)
    {
        float force = gravityConstant * ((mass1 * mass2) / Mathf.Pow(distance, 2));
            return force;
    }

    Rigidbody getChildRb(GameObject obj)
    {
        GameObject child = obj.transform.Find("Shape").gameObject;
        return child.GetComponent<Rigidbody>();
    }

    void addTrail(GameObject obj)
    {
        TrailRenderer trail = obj.AddComponent<TrailRenderer>();
        trail.material = trailMatte;
        trail.startWidth = 0.1f;
        trail.endWidth = 0f;
        trail.time = 100;
    }
}
