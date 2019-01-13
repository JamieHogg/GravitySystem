using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySystem : MonoBehaviour {

    public float gravityConstant = 6.673f;
    public List<GameObject> nbodyObjs;
    public List<float> masses;
    public List<Vector3> velocities;
    public Material trailMatte;

    // Use this for initialization
    void Awake ()
    {
        nbody[] nbodys = GameObject.FindObjectsOfType<nbody>();

        foreach (nbody n in nbodys)
        {
            nbodyObjs.Add(n.gameObject);
            masses.Add(n.mass);
            velocities.Add(n.velocity);
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

            if (n.name == "Sun")
            {
                rb.isKinematic = true;
            }
            count++;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
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
                    Debug.Log(gravityVector);
                }
            }
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
    }
}
