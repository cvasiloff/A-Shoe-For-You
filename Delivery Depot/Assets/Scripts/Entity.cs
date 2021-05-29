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
        Debug.Log(this.name + " has died");
    }

    public virtual void Move()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Floor")
        {
            if(collision.gameObject.tag != this.gameObject.tag)
                Die();
        }
    }
}
