using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneBooth : MonoBehaviour
{
    [Header ("Set In Inspector")]
    public GameObject CustomerPrefab;
    public bool canSpawn = true;
    private PlayerController player;
    // Start is called before the first frame update

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        if(canSpawn)
            StartCoroutine(SpawnCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnCooldown()
    {
        SpawnCustomer();
        yield return new WaitForSeconds(5);
        if(!player.death)
            StartCoroutine(SpawnCooldown());
    }

    public void SpawnCustomer()
    {
        GameObject customer = GameObject.Instantiate(CustomerPrefab, this.transform.position + this.transform.forward.normalized * 2, this.transform.rotation);
        Physics.IgnoreCollision(customer.GetComponent<Collider>(), this.GetComponent<Collider>());
        customer.GetComponent<Customer>().inMotion = true;
    }
}
