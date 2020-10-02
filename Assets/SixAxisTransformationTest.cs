using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SixAxisTransformationTest : MonoBehaviour
{
    private float w;//argoument of perigee
    private float om;//right assention of assending node
    private float i;
    private Vector3 Xh;
    private Vector3 Yh;
    private Vector3 Zh;
    // Start is called before the first frame update
    void Start()
    {
        w = 0;
        om = 0;
        i =0;
        CalculateOrbitalPlane();
        SetCordinets();
    }
    void CalculateOrbitalPlane()
    {
        Vector3 Jh = new Vector3(1, 0, 0);
        Vector3 Ih = new Vector3(0, 0, 1);
        Vector3 Kh = new Vector3(0, 1, 0);

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

       /* Zh = SwitchZY(Zh);
        Xh = SwitchZY(Xh);
        Yh = SwitchZY(Yh);*/
        Debug.Log(Xh + "\n" + Yh + "\n" + Zh);
    }
    Vector3 SwitchZY(Vector3 v)
    {
        float tmp = v.z;
        v.z = v.y;
        v.y = tmp;
        return v;
    }
    void SetCordinets()
    {
        float x = this.transform.position.x;
        float z = this.transform.position.z;
        float y = this.transform.position.y;
        this.transform.position = x * Xh + y*Zh + Yh * z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
