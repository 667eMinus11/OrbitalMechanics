using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SixElement_Orbit : MonoBehaviour
{//inclention and argoument of perge are switche for some gode forsaken resen just go with it
    public GameObject TargetObject;
    public float a;//semi major axis
    public float e;//eccentricety
    public float V0;//true anomly
    public float w;//argoument of perigee
    public float om;//right assention of assending node
    public float i;//inclantion

    public bool moon;
    private bool solved_plane;
    private Vector3 Xh;
    private Vector3 Yh;
    private Vector3 Zh;

    public float TimeScale;


    private float progress;

    private float b;//semi minor axis
    private float c;
    public Vector3[] GetOrbitalPlane()
    {
        if (!solved_plane)
        {
            CalculateOrbitalPlane();
        }
        Vector3[] vectors = new Vector3[3];
        vectors[0] = Xh;
        vectors[1] = Yh;
        vectors[2] = Zh;
        return vectors;
    }
    private void Start()
    {
        progress = 0;
        SetElipse();
        CalculateOrbitalPlane();
    }
    void SetElipse()
    {
        c = e * a;
        b = Mathf.Sqrt(a * a - c * c);
        progress = V0 / 360;
    }
    void CalculateOrbitalPlane()
    {
        Vector3 Jh = new Vector3(1, 0, 0);
        Vector3 Ih = new Vector3(0, 0, 1);
        Vector3 Kh = new Vector3(0, 1, 0);

        if (moon)
        {
            Vector3[] Plane= TargetObject.GetComponent<SixElement_Orbit>().GetOrbitalPlane();
            Jh = -Plane[1];
            Kh = Plane[2];
            Ih = Plane[0];
        }

        float wRad = w * Mathf.Deg2Rad;
        float iRad = i * Mathf.Deg2Rad;
        float omRad = om * Mathf.Deg2Rad;

        float x1 = Mathf.Cos(omRad) * Mathf.Cos(wRad) - Mathf.Sin(omRad) * Mathf.Cos(iRad) * Mathf.Sin(wRad);
        float x2 = Mathf.Sin(omRad) * Mathf.Cos(wRad) + Mathf.Cos(omRad) * Mathf.Cos(iRad) * Mathf.Sin(wRad);
        float x3 = Mathf.Sin(iRad) * Mathf.Sin(wRad);

        float y1 = -Mathf.Cos(omRad) * Mathf.Sin(wRad) - Mathf.Sin(omRad) * Mathf.Cos(iRad) * Mathf.Cos(wRad);
        float y2 = -Mathf.Sin(omRad) * Mathf.Sin(wRad) + Mathf.Cos(omRad) * Mathf.Cos(iRad) * Mathf.Cos(wRad);
        float y3 = Mathf.Sin(iRad) * Mathf.Cos(wRad);

        float z1 = Mathf.Sin(iRad) * Mathf.Sin(omRad);
        float z2 = -Mathf.Sin(iRad) * Mathf.Cos(omRad);
        float z3 = Mathf.Cos(iRad);

        Xh = x1 * Ih + x2 * Jh + x3 * Kh;
        Yh = y1 * Ih + y2 * Jh + y3 * Kh;
        Zh = z1 * Ih + z2 * Jh + z3 * Kh;
        solved_plane = true;

    }

    // Update is called once per frame
    void Update()
    {
        progress += GetProgresStep();//progres the object by step
        this.transform.position = ProgresToVector3();//applaies the progras in world cordinets
    }
    Vector3 ProgresToVector3()//Converts the progras value from 
    {
        float angle = progress * 360 * Mathf.Deg2Rad;//converts progres to degres from center of ellipse
        float x = Mathf.Cos(angle) * a;
        float y = Mathf.Sin(angle) * b;
        float z = 0;
        Vector3 X = x * Xh;
        Vector3 Y = y * Yh;
        Vector3 Z = z * Zh;
        Vector3 pos= X + Z + Y;
        Vector3 Cvec = -c * Xh;

        pos.x += TargetObject.transform.position.x+Cvec.x;
        pos.y += TargetObject.transform.position.y + Cvec.y;
        pos.z += TargetObject.transform.position.z + Cvec.z;
        return pos; 
    }
    float CalculateOrbitalPriod()
    {
        return Mathf.Sqrt(Mathf.Pow(a, 3) / Mathf.Pow(5, 3) * 3 * 3);//claculate orbital priod using given data and keplar's second law
    }
    float GetProgresStep()
    {
        float Distance = Vector3.Distance(this.transform.position, TargetObject.transform.position);//gets distance to the object we are orbiting
        float Step = 0.01f / CalculateOrbitalPriod();//calculates step using the orbital priod and simulation time step
        Step /= Distance;//adds the third law of kepler (the higher you are in your orbit the slower you go);
        return Step*TimeScale;
    }
    public void SetOrbitalPrameters(float a,float e,float i,float w,float om,float V0,bool is_moon,GameObject TargetObject)
    {
        this.a = a;
        this.e = e;
        this.i = i;
        this.w = w;
        this.om = om;
        this.V0 = V0;
        moon = is_moon;
        TimeScale = 1000;
        this.TargetObject = TargetObject;
        SetElipse();
        CalculateOrbitalPlane();
    }

}
