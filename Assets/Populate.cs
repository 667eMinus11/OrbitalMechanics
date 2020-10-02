using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Populate : MonoBehaviour
{
    public GameObject prefab;
    private int index;
    public int AstroidAmount;
    public bool Spawn_Plants;
    public bool Spawn_Astroids;
    private string[] AstroidDataEntries;
    private string[] PlanetDataEntries;
    private int PlantIndex;
    public bool Spawn_Moons;
    private const double KmToAu = 6.66E-9;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        PlantIndex = 0;
        GetFullFile();
        if (Spawn_Astroids)
        {
            StartCoroutine(SpawnAstroids());
        }
        if (Spawn_Plants)
        {
            SpawnPlants();
        }
        if (Spawn_Plants && Spawn_Moons)
        {
            SpawnMoons();
        }
    }
    private void SpawnMoons()
    {
        string[] Plants = { "Earth", "Mars", "jupiter", "Saturn", "Uranus", "Neptune", "Pluto" };
        foreach(string plant in Plants)
        {
           SpawnSystem(plant);
        }
    }
    private void SpawnSystem(string Plant)
    {
        string fullFile = System.IO.File.ReadAllText("C:/Users/tomer/Desktop/Solar system object data/Moons/" + Plant + ".csv");
        string[] MoonDataEntries = fullFile.Split('\n');
        int num=1;
        while (num < MoonDataEntries.Length)
        {
            NewMoon(MoonDataEntries[num],Plant);
            num++;
        }
        
    }
    private void NewMoon(string MoonData,string Plant)
    {
        GameObject obj = Instantiate(prefab) as GameObject;

        string[] elemets = MoonData.Split(',');
        obj.name = elemets[0];
        float a = (float)(float.Parse(elemets[1])*KmToAu * 500);
        float e = float.Parse(elemets[2]);
        float V0 = float.Parse(elemets[4]);
        float w = float.Parse(elemets[3]);
        float i = float.Parse(elemets[5]);
        float om = float.Parse(elemets[6]);

        obj.GetComponent<SixElement_Orbit>().SetOrbitalPrameters(a, e, i, w, om, V0, true, GameObject.Find(Plant));
        obj.transform.localScale = Vector3.one * (float.Parse(elemets[10]) / 50);
    }
    private void NewAstroid()
    {
        index++;
        GameObject obj = Instantiate(prefab) as GameObject;

        string[] elemets = AstroidDataEntries[index].Split(',');
        obj.name = elemets[0];
        float a = float.Parse(elemets[4])*500;
        float e = float.Parse(elemets[3]);
        float V0 = float.Parse(elemets[9]);
        float w = float.Parse(elemets[8]);
        float i = float.Parse(elemets[6]);
        float om = float.Parse(elemets[7]);

        obj.GetComponent<SixElement_Orbit>().SetOrbitalPrameters(a, e, i, w, om, V0,false, GameObject.Find("Sun"));
        obj.transform.localScale = Vector3.one*(float.Parse(elemets[11])*2 / 50);
        
    }
    private void NewPlant()
    {
        PlantIndex++;
        GameObject obj = Instantiate(prefab) as GameObject;

        string[] elemets = PlanetDataEntries[PlantIndex].Split(',');
        obj.name = elemets[0];
        float a = float.Parse(elemets[1]) * 500;
        float e = float.Parse(elemets[2]);
        float V0 = float.Parse(elemets[4]);
        float w = float.Parse(elemets[5]);
        float i = float.Parse(elemets[3]);
        float om = float.Parse(elemets[6]);

        obj.GetComponent<SixElement_Orbit>().SetOrbitalPrameters(a, e, i, w, om, V0,false, GameObject.Find("Sun"));
        obj.transform.localScale = Vector3.one * (float.Parse(elemets[10]) / 500);

    }
    IEnumerator SpawnAstroids()
    {
        while (index <AstroidAmount)
        {
            yield return new WaitForSeconds(0.0001f);
            NewAstroid();
        }
    }
    private void SpawnPlants()
    {
        while (PlantIndex<9)
        {
            NewPlant();
        }
    }
    private void GetFullFile()
    {
        string fullFile1 = System.IO.File.ReadAllText("C:/Users/tomer/Desktop/Solar system object data/Small Objects/AllKnownObjectsOver10kmV3.csv");
        AstroidDataEntries = fullFile1.Split('\n');
        string fullFile2 = System.IO.File.ReadAllText("C:/Users/tomer/Desktop/Solar system object data/Plants + pluto/PhisicalDataV2.csv");
        PlanetDataEntries = fullFile2.Split('\n');
    }


}
