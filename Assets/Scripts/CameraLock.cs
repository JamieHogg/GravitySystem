using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour {

    public GameObject camTarget;
    private GameObject camTargetStart;
    public float rotateSpeed;
    public float smoothing;

    private float rotateLeft;
   // private bool following = true;
    private Vector3 offset;
    private Vector3 lastPosition;

    private float joystick_deadzone = 0.3f;

    // Use this for initialization
    void Start ()
    {
        lastPosition = new Vector3(camTargetStart.transform.position.x, camTargetStart.transform.position.y, camTargetStart.transform.position.z - 50);
        offset = lastPosition;

        camTargetStart = camTarget;
    }
	
	// Update is called once per frame
	void Update () {

        offset = Quaternion.AngleAxis(rotateLeft * rotateSpeed, Vector3.up) * offset;
        transform.position = offset;
        transform.position = new Vector3(Mathf.Lerp(lastPosition.x, camTarget.transform.position.x + offset.x, smoothing * Time.deltaTime),
            Mathf.Lerp(lastPosition.y, camTarget.transform.position.y + offset.y, smoothing * Time.deltaTime),
            Mathf.Lerp(lastPosition.z, camTarget.transform.position.z + offset.z, smoothing * Time.deltaTime));
        transform.LookAt(camTarget.transform.position);

    }

    void CameraInput()
    {
        if (Input.GetKey(KeyCode.Q) || Input.GetAxisRaw("Rotation") < -joystick_deadzone)
        {
            if (!Input.GetKey(KeyCode.Q))
            {
                rotateLeft = Input.GetAxisRaw("Rotation");
            }
            else
            {
                rotateLeft = -1;
            }
        }

        else if (Input.GetKey(KeyCode.E) || Input.GetAxisRaw("Rotation") > joystick_deadzone)
        {

            if (!Input.GetKey(KeyCode.E))
            {
                rotateLeft = Input.GetAxisRaw("Rotation");
            }
            else
            {
                rotateLeft = 1;
            }
        }
        else
        {
            rotateLeft = 0;
        }
    }
}
