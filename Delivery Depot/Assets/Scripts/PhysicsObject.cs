using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    // Start is called before the first frame update
    protected Rigidbody myRig;
    protected float slowTime = 1;
    public void Start()
    {
        myRig = this.GetComponent<Rigidbody>();
        Debug.Log("Hey");
        if (myRig == null)
        {
            throw new System.Exception(this.name + ": Could not find RigidBody");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}