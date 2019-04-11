using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CameraControllerthing : MonoBehaviour
{
    public List<GameObject> nbodyObjs;
    private int nCount = 0;
    public List<GameObject> child;

    public GameObject cameraTarget;
    private GameObject cameraTargetStart;
    public float rotateSpeed;
    public float offsetDistance;
    public float smoothing;

    Vector3 offset;
    Vector3 lastPosition;

    private float rotateLeft;
    private float rotateUp;

    private bool horizontal = false;
    private bool verticle = false;
    public int cameraRotation = 0;

    void Awake()
    {
        nbody[] nbodys = GameObject.FindObjectsOfType<nbody>();


        foreach (nbody n in nbodys)
        {
            nbodyObjs.Add(n.gameObject);
        }
        foreach (GameObject n in nbodyObjs)
        {
            child.Add(n.transform.Find("Shape").gameObject);
        }
        //cameraTarget = GameObject.Find("Star").transform.GetChild(0).gameObject;
        //cameraTarget = GameObject.Find("Star").transform.GetChild(0).gameObject;

        lastPosition = new Vector3(cameraTarget.transform.position.x, cameraTarget.transform.position.y, cameraTarget.transform.position.z - offsetDistance);
        offset = lastPosition;

        cameraTargetStart = cameraTarget;
    }

    void Update()
    {
        normalCamera();
        switchCamera();

        if (horizontal)
        {
            offset = Quaternion.AngleAxis(rotateLeft * rotateSpeed, Vector3.up) * offset;
        }
        //else if (verticle)
        //{
        //    offset = Quaternion.AngleAxis(rotateUp * rotateSpeed, Vector3.left) * offset;
        //}
        transform.position = offset;
        transform.position = new Vector3(Mathf.Lerp(lastPosition.x, cameraTarget.transform.position.x + offset.x, smoothing * Time.deltaTime),
            Mathf.Lerp(lastPosition.y, cameraTarget.transform.position.y + offset.y, smoothing * Time.deltaTime),
            Mathf.Lerp(lastPosition.z, cameraTarget.transform.position.z + offset.z, smoothing * Time.deltaTime));
        transform.LookAt(cameraTarget.transform.position);
    }

	void LateUpdate()
	{
		lastPosition = transform.position;
        //linecastCheck();
	}

    void linecastCheck()
    {
        RaycastHit hitinfo;

        if (Physics.Linecast(cameraTarget.transform.position, transform.position, out hitinfo))
        {
            transform.position = hitinfo.point;
        }
    }

    void normalCamera()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotateLeft = 1;
            horizontal = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rotateLeft = -1;
            horizontal = true;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            rotateUp = -1;
            verticle = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rotateUp = 1;
            verticle = true;
        }
        else
        {
            rotateLeft = 0;
            horizontal = false;
            verticle = false;
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            offsetDistance -= 0.1f;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            offsetDistance += 0.1f;
        }
    }

    void switchCamera()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (nCount == nbodyObjs.Count)
            {
                nCount = 0;
            }
            cameraTarget = child[nCount];
            nCount++;
        }
    }
}