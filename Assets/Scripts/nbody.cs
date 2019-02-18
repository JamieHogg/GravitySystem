using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nbody : MonoBehaviour {

    public nbodyType type;
    public float mass;
    public Vector3 velocity;

    public GameObject orbitObject;

    float getMass()
    {
        return mass;
    }

    void OnDrawGizmosSelected()
    {
        GameObject child = this.transform.Find("Shape").gameObject;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(child.transform.position, velocity);
    }
}
