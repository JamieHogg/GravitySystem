using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colliding : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        float colMass = col.gameObject.GetComponent<nbody>().mass;
        float parentMass = transform.parent.gameObject.GetComponent<nbody>().mass;

        Debug.Log("hit");
        if (colMass > parentMass)
        {
            colMass += parentMass;
            Destroy(transform.parent);
        }
        else
        {
            parentMass += colMass;
            Destroy(col.gameObject);
        }
    }
}
