using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySystem : MonoBehaviour
{

    public float gravityConstant;
    public Material trailMatte;

    [HideInInspector] public List<GameObject> nbodyObjs;
    [HideInInspector] public List<nbodyType> types;
    [HideInInspector] public List<float> masses;
    [HideInInspector] public List<Vector3> velocities;
    [HideInInspector] public List<GameObject> orbitObjects;

    public List<Vector3> initialDirections;
    public List<Vector3> initialVectors;

    // Use this for initialization
    void Awake()
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
        int count = 0;
        foreach (GameObject n in nbodyObjs)
        {
            if (orbitObjects[count] != null)
            {
                Vector3 gravityVector = getGravityVector(n, n.transform.position, 
                    orbitObjects[count], orbitObjects[count].transform.position);

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

    Vector3 getGravityVector(GameObject obj, Vector3 objPos, GameObject targetObj, Vector3 targetPos)
    {
        Rigidbody rb = getChildRb(obj);
        Rigidbody tRb = getChildRb(targetObj);

        //Vector3 difference = tRb.transform.position - rb.transform.position;
        Vector3 difference = targetPos - objPos;

        float distance = difference.magnitude;
        Vector3 gravityDirection = difference.normalized;

        float force = newtonsLawGravity(rb.mass, tRb.mass, distance);
        Vector3 gravityVector = gravityDirection * force;
        return gravityVector;
    }
}