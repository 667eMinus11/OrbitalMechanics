using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingObject : MonoBehaviour
{

    private List<Vector3> Pull;//acting gravitional forces
    public Rigidbody body;//rigid body component
    public Vector3 V0;//initial velocity
    void Start()
    {
        body.velocity = V0;
    }
    List<Vector3> GetForces()
    {
        List<Vector3> Forces = new List<Vector3>();//list of forces
        GameObject[] Plants=GameObject.FindGameObjectsWithTag("Celstial_Object");//array of celstial object
        foreach (GameObject Plant in Plants)
        {
            double Distance = Vector3.Distance(this.transform.position, Plant.transform.position);//Gets distance between this object and the traget object
            Rigidbody PBody = Plant.GetComponent<Rigidbody>();//Get the other plant's riged body component

            Celstial_Object PlantOrbitData = Plant.GetComponent<Celstial_Object>();
            Celstial_Object ThisOrbitData = this.GetComponent<Celstial_Object>();//gets the scale of mass in both tis object and the target object

            double Force = ((body.mass * (Mathf.Pow(10,ThisOrbitData.Scale)) * PBody.mass* (Mathf.Pow(10, PlantOrbitData.Scale))) / (Distance * Distance)) * 6.67384e-11f;//Newton's gravtional eqution
            Vector3 ToOtherBody = Plant.transform.position - this.transform.position;//Gets a vector3 between this object and the target object

            Forces.Add(Vector3.ClampMagnitude(ToOtherBody, (float)(Force)));//clamps the dirction to the magnitod of the force and adds it to forces
        }
        return Forces;
    }
    // Update is called once per frame
    void Update()
    {
        Pull = GetForces();//gets all the forces acting on the object
        foreach(Vector3 Force in Pull)//adds the forces to the object
        {
            body.AddForce(Force); 
        }
    }
}
