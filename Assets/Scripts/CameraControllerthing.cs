using UnityEngine;
using System.Collections;

public class CameraControllerthing : MonoBehaviour
{
    public GameObject cameraTarget;
    private GameObject cameraTargetStart;
    public float rotateSpeed;
    public float offsetDistance;
    public float offsetHeight;
    public float smoothing;
    public bool rotation90 = false;
    public bool rotated = false;
    Vector3 offset;
    bool following = true;
    Vector3 lastPosition;

    private float rotateLeft;
    private float rotateUp;

    private bool horizontal = false;
    private bool verticle = false;
    public int cameraRotation = 0;

    void Start()
    {
        lastPosition = new Vector3(cameraTarget.transform.position.x, cameraTarget.transform.position.y + offsetHeight, cameraTarget.transform.position.z - offsetDistance);
        offset = lastPosition;

        cameraTargetStart = cameraTarget;
    }

    void Update()
    {
        normalCamera();

        if (horizontal)
        {
            offset = Quaternion.AngleAxis(rotateLeft * rotateSpeed, Vector3.up) * offset;
        }
        else if (verticle)
        {
            offset = Quaternion.AngleAxis(rotateUp * rotateSpeed, Vector3.up) * offset;
        }
        transform.position = offset;
        transform.position = new Vector3(Mathf.Lerp(lastPosition.x, cameraTarget.transform.position.x + offset.x, smoothing * Time.deltaTime),
            Mathf.Lerp(lastPosition.y, cameraTarget.transform.position.y + offset.y, smoothing * Time.deltaTime),
            Mathf.Lerp(lastPosition.z, cameraTarget.transform.position.z + offset.z, smoothing * Time.deltaTime));
        transform.LookAt(cameraTarget.transform.position);
    }

	void LateUpdate()
	{
		lastPosition = transform.position;
        linecastCheck();
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
        if (Input.GetKey(KeyCode.A))
        {
            rotateLeft = 1;
            horizontal = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotateLeft = -1;
            horizontal = true;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            rotateUp = -1;
            verticle = true;
        }
        else if (Input.GetKey(KeyCode.S))
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
    }
}