using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySystemMKII : MonoBehaviour
{

    public float gravityConstant;

    public List<GameObject> nbodyObjs;
    public List<nbodyType> types;
    public List<float> masses;
    public List<Vector3> velocities;
    public List<GameObject> orbitObjects;
    public Vector3[] gravVectors;

    // Use this for initialization
    void Awake()
    {
        nbody[] nbodys = GameObject.FindObjectsOfType<nbody>();
        gravVectors = new Vector3[nbodys.Length];

        foreach (nbody n in nbodys)
        {
            nbodyObjs.Add(n.gameObject.transform.GetChild(0).gameObject);
            masses.Add(n.mass);
            velocities.Add(n.velocity);
            types.Add(n.type);
            orbitObjects.Add(n.orbitObject);
        }

        int count = 0;
        foreach (GameObject n in nbodyObjs)
        {
            Rigidbody rb = getRb(n);
            rb.mass = masses[count];
            getRb(n).AddForce(velocities[count]);

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
        gravity();
    }

    float newtonsLawGravity(float mass1, float mass2, float distance)
    {
        float force = gravityConstant * ((mass1 * mass2) / Mathf.Pow(distance, 2));
        return force;
    }

    Rigidbody getRb(GameObject obj)
    {
        return obj.GetComponent<Rigidbody>();
    }

    public Vector3 getGravityVector(GameObject obj, GameObject targetObj)
    {
        Rigidbody rb = getRb(obj);
        Rigidbody tRb = getRb(targetObj);

        Vector3 difference = tRb.transform.position - rb.transform.position;

        float distance = difference.magnitude;
        Vector3 gravityDirection = difference.normalized;

        float force = newtonsLawGravity(rb.mass, tRb.mass, distance);
        Vector3 gravityVector = gravityDirection * force;
        return gravityVector;
    }

    Vector3 calculateHooksLaw(GameObject obj)
    {
        Vector3 totalGravityVector = Vector3.zero;
        Vector3 gravityVector = Vector3.zero;

        foreach (GameObject n in nbodyObjs)
        {
            gravityVector = getGravityVector(n, obj);
            totalGravityVector += gravityVector;
        }
        return totalGravityVector;
    }

    void gravity()
    {
        int count = 0;
        foreach (GameObject n in nbodyObjs)
        {
            foreach (GameObject i in nbodyObjs)
            {
                if (n != i)
                {
                    Vector3 gravityVector = getGravityVector(n, i);
                    gravVectors[count] = gravityVector;

                    if ((types[count] == nbodyType.Asteroid || types[count] == nbodyType.Ship))
                    {
                        getRb(n).AddForce(gravityVector);
                    }
                }
            }
            count++;
        }
    }
}