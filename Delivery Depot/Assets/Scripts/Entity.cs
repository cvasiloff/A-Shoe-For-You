using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : PhysicsObject
{
    public bool death;
    
    public void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame

    public void Die()
    {
        death = true;
    }

    public virtual void Move()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Floor")
        {
            Die();
        }
    }
}
