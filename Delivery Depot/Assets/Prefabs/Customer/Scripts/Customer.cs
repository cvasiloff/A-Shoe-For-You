using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : Entity
{
    public bool inMotion;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(inMotion && !death)
        {
            myRig.velocity = this.transform.forward * 5;
        }
    }

    public void BeginDeath()
    {
        StartCoroutine(RemoveCustomer());
        
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    IEnumerator RemoveCustomer()
    {
        
        yield return new WaitForSeconds(1);
        Destroy(this.transform.GetChild(0).gameObject);
        yield return new WaitForSeconds(1);
        gm.CallPolice();
        if(this != null)
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Floor" && collision.gameObject.tag != "Booth" && collision.gameObject.tag != "Package")
        {
            BeginDeath();
        }
        else if(collision.gameObject.tag == "Booth")
        {
            Destroy(this.gameObject);
        }
    }
}
