using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : Entity
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Destroy(this.gameObject);
    }
}
