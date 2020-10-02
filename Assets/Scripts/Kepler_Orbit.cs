using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kepler_Orbit : MonoBehaviour
{
    public float SemiMajorDistance;//Width of the ellipse
    public float SemiMainorDistance;//Hight of the ellipse
    public GameObject TargetObject;//The object we are orbiting 
    public float Progres=0;//Progras of the orbit ( 1= one orbit, 0.5= half a orbit, 153.3= one handeret fifty three point three orbits) [need to make it reset back to zero evry once in a while] 
    public bool FirstFocus;//Witch of the foci to use
    [Range(0,1)]
    public float TimeStep=0.01f;//simulation time step (basicly how fast is time)
    public float SMajor=5;//Date for kepleres second law works fine but these values are aproximated
    public float T2 = 3;

    [Range(0, 35)]
    public float Angle;//Rotate the orbit aroud its focus(hopfely) has bugs 35 is the limit for stable preformance

    private float OrbitalPriod;//Time that it takes to complate a orbit calculated using kepler's secend law

    private Vector3[] Foci= new Vector3[2];//The two focoses of the ellipse
    Vector3 ProgresToVector3()//Converts the progras value from 
    {
        float Distance = Vector3.Distance(this.transform.position, TargetObject.transform.position);

        float rotZ = Mathf.Sin(( Angle) * Mathf.Deg2Rad) * Distance;
        float rotX = Distance-Mathf.Cos(( Angle) * Mathf.Deg2Rad) * Distance;//no idea does some trigo magic to add a transform for every point on the orbis as to rotat it unstable beyond three dozend degrees

        float angle = Progres * 360 * Mathf.Deg2Rad;//converts progres to degres from center of ellipse
        float x = Mathf.Cos(angle) * SemiMajorDistance + (FirstFocus? Foci[0].x:Foci[1].x) ;//Gets the point like a circle but multiplise it by difrent values for difent axis to get an ellipse 
        float y = Mathf.Sin(angle) * SemiMainorDistance + (FirstFocus ? Foci[0].z : Foci[1].z);//+ offset for centring the orbit arout one of the foci
        
        return new Vector3(x-rotX, 0, y-rotZ);//returns the point in 2d [need to integrate 3d]
    }
    float CalculateOrbitalPriod()
    {
        return Mathf.Sqrt(Mathf.Pow(SemiMainorDistance, 3) / Mathf.Pow(SMajor, 3) * T2 * T2);//claculate orbital priod using given data and keplar's second law
    }
    void CalculateFoci() //finds the foci's postions using (linear eccentricty)c^2= semi major^2- semi minor^2
    {
        float c = Mathf.Sqrt(Mathf.Abs(SemiMainorDistance * SemiMainorDistance - SemiMajorDistance * SemiMajorDistance));
        Foci[0] = new Vector3(TargetObject.transform.position.x - c, TargetObject.transform.position.y, TargetObject.transform.position.z);
        Foci[1] = new Vector3(TargetObject.transform.position.x + c, TargetObject.transform.position.y, TargetObject.transform.position.z);
        return;
    }
    void Awake()
    {
        CalculateFoci();
    }
    void Start()
    {
        OrbitalPriod = CalculateOrbitalPriod();//This needs to run only one time constant value
        CalculateFoci();
    }
    float GetProgresStep()
    {
        float Distance = Vector3.Distance(this.transform.position, TargetObject.transform.position);//gets distance to the object we are orbiting
        float Step = 0.01f / OrbitalPriod;//calculates step using the orbital priod and simulation time step
        Step /= Distance;//adds the third law of kepler (the higher you are in your orbit the slower you go);
        return Step;
    }

    // Update is called once per frame
    void Update()
    {
        Progres += GetProgresStep();//progres the object by step
        CalculateFoci();//acounts for movment of target plant [need to change this so it only runs whwne the orbit is changed]
        this.transform.position = ProgresToVector3();//applaies the progras in world cordinets
        

    }
}
