using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAsteroids : MonoBehaviour {

    public GameObject asteroid;
    public int size;

    // Use this for initialization
    void Awake ()
    {
		for (int n = 0; n < size; n++)
        {
            int x = Random.Range(-10, 10);
            int y = Random.Range(-10, 10);
            int z = Random.Range(-10, 10);

            GameObject a = Instantiate(asteroid, new Vector3(x, y, z), Quaternion.identity);
        }
	}
}
