using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(new Vector3(this.transform.position.x, -1000, Camera.main.transform.position.z+10));
        this.transform.position = new Vector3(this.transform.position.x, 2, this.transform.position.z);
        //this.transform.position = new Vector3(this.transform.position.x, 1, this.transform.position.z);
        //this.transform.eulerAngles = new Vector3(0, this.transform.rotation.y, 0);
    }
}
