using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nbody : MonoBehaviour {

    public nbodyType type;
    public float mass;
    public Vector3 velocity;

    public GameObject orbitObject;

    public float getMass()
    {
        return mass;
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }
}
