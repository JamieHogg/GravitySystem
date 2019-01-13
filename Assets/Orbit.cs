using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    private int planetCount = 500;
    private int maxRadius = 50;

    public GameObject planetPrefab;
    public GameObject[] planets;
    public Material[] planetMattes;

    void Awake()
    {
        planets = new GameObject[planetCount];
    }
    // Use this for initialization
    void Start ()
    {
        CreatePlanets(planetCount, maxRadius);
	}

    public GameObject[] CreatePlanets(int num, int radius)
    {
        var plan = new GameObject[num];
        var planetCopy = planetPrefab;
        //rb.useGravity = false;

        for (int n = 0; n < num; n++)
        {
            var planetIns = GameObject.Instantiate(planetCopy);
            Rigidbody rb = planetIns.GetComponent<Rigidbody>();

            planetIns.transform.position = this.transform.position +
                new Vector3(Random.Range(-maxRadius, maxRadius), 
                            Random.Range(-maxRadius, maxRadius), 
                            Random.Range(-maxRadius, maxRadius));
            planetIns.transform.localScale *= Random.Range(0.05f, 0.1f);
            rb.mass = planetIns.transform.localScale.x;

            Renderer planetRender = planetIns.GetComponent<Renderer>();
            planetRender.material = planetMattes[Random.Range(0, planetMattes.Length)];

            planets[n] = planetIns;
        }
        GameObject.Destroy(planetCopy);
        return planets;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float gravityConstant = 6.673f / 4;
		foreach (GameObject p in planets)
        {
            Vector3 difference = this.transform.position - p.transform.position;

            float distance = difference.magnitude;
            Vector3 gravityDirection = difference.normalized;
            float gravity = gravityConstant * (this.transform.localScale.x * p.transform.localScale.x * 100)
                / (distance * distance);
            Vector3 gravityVector = (gravityDirection * gravity);
            //p.transform.GetComponent<Rigidbody>().AddForce(p.transform.forward, ForceMode.Acceleration);
            p.transform.GetComponent<Rigidbody>().AddForce(Random.insideUnitCircle.normalized, ForceMode.Acceleration);
            p.transform.GetComponent<Rigidbody>().AddForce(gravityVector, ForceMode.Acceleration);

        }
	}

    void changeGravityConstant()
    {

    }
}
