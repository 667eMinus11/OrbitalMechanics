using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Rigidbody body;
    public float force;
    public float torque;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            body.AddRelativeForce(new Vector3(0, 0, force));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            body.AddRelativeForce(new Vector3(0, 0, -force));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            body.AddRelativeForce(new Vector3(force, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            body.AddRelativeForce(new Vector3(-force, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            body.AddRelativeForce(new Vector3(0,force, 0));
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            body.AddRelativeForce(new Vector3(0, -force, 0));
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            body.AddTorque(0, torque, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            body.AddTorque(0, -torque, 0);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            body.AddTorque(0, 0,torque);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            body.AddTorque(0, 0, -torque);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            body.AddTorque( torque,0,0);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            body.AddTorque(-torque, 0, 0);
        }

    }
}
