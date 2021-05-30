using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneBooth : MonoBehaviour
{
    public GameObject CustomerPrefab;
    public bool canSpawn = true;
    // Start is called before the first frame update

    void Start()
    {
        StartCoroutine(SpawnCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Spawning Customer");
        SpawnCustomer();
        StartCoroutine(SpawnCooldown());
    }

    public void SpawnCustomer()
    {
        GameObject customer = GameObject.Instantiate(CustomerPrefab, this.transform.position + this.transform.forward.normalized * 2, this.transform.rotation);
        Physics.IgnoreCollision(customer.GetComponent<Collider>(), this.GetComponent<Collider>());
        customer.GetComponent<Customer>().inMotion = true;
    }
}
