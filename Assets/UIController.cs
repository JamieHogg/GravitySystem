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

    public Slider sizeSlider;

    public int numberOfPlanets = 0;
    public int numberOfMoons = 0;
    public int total = 0;

    private bool moving;

    void Update()
    {
        if (dropdown.options.Count == 0)
        {
            for (int i = 1; i < mainUI.Length; i++)
            {
                mainUI[i].SetActive(false);
            }
        }
        else
        {
            if (!moving)
            {
                for (int i = 2; i < mainUI.Length; i++)
                {
                    mainUI[i].SetActive(true);
                }

                if (planets[dropdown.value].tag == "Moon")
                {
                    //sizeSlider.maxValue = 1;
                    //sizeSlider.minValue = 0.5f;
                    mainUI[1].SetActive(false);
                    mainUI[2].SetActive(false);
                }
                else if (planets[dropdown.value].tag == "Planet")
                {
                    //sizeSlider.maxValue = 20;
                    //sizeSlider.minValue = 1;
                    mainUI[1].SetActive(true);
                    mainUI[2].SetActive(true);
                }
            }
        }
    }

    public void spawnPlanet()
    {
        GameObject spawned = Instantiate(planet, Vector3.zero, Quaternion.identity);

        numberOfPlanets++;
        total++;

        string target = spawned.GetComponent<Orbiting>().orbitTarget.name;
        spawned.name = "planet " + numberOfPlanets + ": " + target;
        planets.Add(spawned);

        data = new Dropdown.OptionData();
        data.text = spawned.name;
        dropdata.Add(data);

        dropdown.options.Clear();
        foreach (Dropdown.OptionData d in dropdata)
        {
            dropdown.options.Add(d);
        }
        dropdown.value = total;
    }

    public void spawnMoon()
    {
        GameObject spawned = Instantiate(moon, Vector3.zero, Quaternion.identity);

        numberOfMoons++;
        total++;

        spawned.GetComponent<Orbiting>().orbitTarget = planets[dropdown.value];
        string target = spawned.GetComponent<Orbiting>().orbitTarget.name;
        spawned.name = "Moon " + numberOfMoons + ": " + target;
        planets.Add(spawned);

        data = new Dropdown.OptionData();
        data.text = spawned.name;
        dropdata.Add(data);

        dropdown.options.Clear();
        foreach (Dropdown.OptionData d in dropdata)
        {
            dropdown.options.Add(d);
        }
        dropdown.value = total;
    }



    //public void spawnMoon()
    //{
    //    GameObject spawned = Instantiate(moon, Vector3.zero, Quaternion.identity);
    //    spawned.name = "moon " + numberOfMoons;
    //    moons.Add(spawned);
    //    numberOfMoons++;

    //    spawned.GetComponent<Orbiting>().orbitTarget = planets[dropdown.value];

    //    data = new Dropdown.OptionData();
    //    data.text = spawned.name;
    //    dropdata.Add(data);

    //    dropdown.options.Clear();
    //    foreach (Dropdown.OptionData d in dropdata)
    //    {
    //        dropdown.options.Add(d);
    //    }
    //    dropdown.value = numberOfPlanets;
    //}

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
    public void changeProgress(float num)
    {
        planets[dropdown.value].GetComponent<Orbiting>().progress = (int)num;
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

        for (int i = 0; i < mainUI.Length - 1; i++)
        {
            mainUI[i].SetActive(moving);
        }

        moving = !moving;
    }
}