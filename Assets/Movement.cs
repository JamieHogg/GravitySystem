using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        rb = this.gameObject.transform.GetChild(0).GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        int speed = 3;


        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("forward");
            rb.AddForce(Vector3.forward * speed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("back");
            rb.AddForce(Vector3.back * speed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("left");
            rb.AddForce(Vector3.left * speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("right");
            rb.AddForce(Vector3.right * speed);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log("up");
            rb.AddForce(Vector3.up * speed);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("down");
            rb.AddForce(Vector3.down * speed);
        }

    }
}
