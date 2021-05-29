using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    // Start is called before the first frame update
    protected Rigidbody myRig;
    protected float slowTime = 1;
    public ManageGame gm;
    public void Start()
    {
        myRig = this.GetComponent<Rigidbody>();
        if (myRig == null)
        {
            throw new System.Exception(this.name + ": Could not find RigidBody");
        }
        gm = FindObjectOfType<ManageGame>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
