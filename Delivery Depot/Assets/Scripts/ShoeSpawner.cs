using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoeSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject package;
    void Start()
    {
        StartCoroutine(SpawnShoe());
    }

    // Update is called once per frame
    IEnumerator SpawnShoe()
    {
        GameObject shoe = GameObject.Instantiate(package, new Vector3((Random.Range(-13.0f, 13.0f)), this.transform.position.y, this.transform.position.z), Quaternion.Euler(Random.Range(-360, 360f), Random.Range(-360, 360f), Random.Range(-360, 360f)));
        yield return new WaitForSeconds(0.1f);

        StartCoroutine(SpawnShoe());
    }
}
