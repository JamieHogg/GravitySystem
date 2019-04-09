using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public List<GameObject> planets;

    public List<GameObject> moons;

    public GameObject planet;
    public GameObject moon;

    public Dropdown dropdown;
    Dropdown.OptionData data;
    List<Dropdown.OptionData> dropdata = new List<Dropdown.OptionData>();

    public GameObject[] mainUI;

    public Material[] mattes;

    public int numberOfPlanets = 0;
    public int numberOfMoons = 0;

    void Update()
    {
        if (dropdown.options.Count == 0)
        {
            foreach (GameObject m in mainUI)
            {
                m.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject m in mainUI)
            {
                m.SetActive(true);
            }
        }
    }

    public void spawnPlanet()
    {
        GameObject spawned = Instantiate(planet, Vector3.zero, Quaternion.identity);
        spawned.name = "planet " + numberOfPlanets;
        planets.Add(spawned);
        numberOfPlanets++;

        data = new Dropdown.OptionData();
        data.text = spawned.name;
        dropdata.Add(data);

        dropdown.options.Clear();
        foreach (Dropdown.OptionData d in dropdata)
        {
            dropdown.options.Add(d);
        }
        dropdown.value = numberOfPlanets;
    }

    public void spawnMoon()
    {
        GameObject spawned = Instantiate(moon, Vector3.zero, Quaternion.identity);
        spawned.name = "moon " + numberOfMoons;
        moons.Add(spawned);
        numberOfMoons++;

        spawned.GetComponent<Orbiting>().orbitTarget = planets[dropdown.value];

        //data = new Dropdown.OptionData();
        //data.text = spawned.name;
        //dropdata.Add(data);

        //dropdown.options.Clear();
        //foreach (Dropdown.OptionData d in dropdata)
        //{
        //    dropdown.options.Add(d);
        //}
        //dropdown.value = numberOfPlanets;
    }

    public void changeSize(float num)
    {
        planets[dropdown.value].GetComponent<Orbiting>().semiMajorAxisA = num;
    }
    public float getSize()
    {
        return planets[dropdown.value].GetComponent<Orbiting>().semiMajorAxisA;
    }
    public void changeEccentricity(float num)
    {
        planets[dropdown.value].GetComponent<Orbiting>().eccentricity = num;
    }

    public void changeRotationX(float num)
    {
        planets[dropdown.value].GetComponent<Orbiting>().rotateX = num;
    }
    public void changeRotationY(float num)
    {
        planets[dropdown.value].GetComponent<Orbiting>().rotateY = num;
    }
    public void changeRotationZ(float num)
    {
        planets[dropdown.value].GetComponent<Orbiting>().rotateZ = num;
    }

    public void changeMatteBlue()
    {
        planets[dropdown.value].transform.GetChild(0).GetComponent<Renderer>().material = mattes[0];
    }
    public void changeMatteRed()
    {
        planets[dropdown.value].transform.GetChild(0).GetComponent<Renderer>().material = mattes[1];
    }
    public void changeMatteGreen()
    {
        planets[dropdown.value].transform.GetChild(0).GetComponent<Renderer>().material = mattes[2];
    }

    public void changeDirection()
    {
        planets[dropdown.value].GetComponent<Orbiting>().progressDirection = !planets[dropdown.value].GetComponent<Orbiting>().progressDirection;
    }

    public void startMovement(bool move)
    {
        foreach (GameObject p in planets)
        {
            p.GetComponent<Orbiting>().move = !p.GetComponent<Orbiting>().move;
        }
        foreach (GameObject m in moons)
        {
            m.GetComponent<Orbiting>().move = !m.GetComponent<Orbiting>().move;
        }
    }
}