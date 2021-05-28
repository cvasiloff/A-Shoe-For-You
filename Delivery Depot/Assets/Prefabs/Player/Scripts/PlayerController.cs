using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    // Start is called before the first frame update

    [Header("Set Variables")]
    public int turnSpeed;
    public int moveSpeed;
    public GameObject PackagePrefab;
    public float resetPackageTime;
    public int brakeTime;
    

    [Header("Don't Set variables")]
    public bool canSpawnPackage = true;
    Camera myCam;
    private float brakeTimer;

    void Start()
    {
        base.Start();
        myCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        MoveCam();
    }

    public void PlayerMove()
    {
        float turn = Input.GetAxisRaw("Horizontal");
        float move = Input.GetAxisRaw("Vertical");

        if(turn != 0)
        {
            myRig.angularVelocity = new Vector3(0, turn, 0).normalized * turnSpeed;
        }

        if(Input.GetAxisRaw("Fire2") != 0)
        {
            slowTime = 0.5f;
            brakeTimer -= Time.deltaTime * 1.5f;

            if(brakeTimer <= 0)
            {
                Debug.Log("Brakes Failed!");
                //Die();
            }
        }
        else
        {
            if(brakeTimer < brakeTime)
            {
                brakeTimer += Time.deltaTime;
            }
            slowTime = 1;
        }

        if(Input.GetAxisRaw("Fire1") != 0)
        {
            if(canSpawnPackage)
            {
                canSpawnPackage = false;
                SpawnPackage();
            }
        }
        
        //Constantly moving forward
        myRig.velocity = this.transform.forward * moveSpeed * slowTime;
    }

    public void SpawnPackage()
    {

        Vector3 mousePos = GetMouseLoc();

        //GameObject package = GameObject.Instantiate(PackagePrefab, new Vector3(mousePos.x, 1, mousePos.z), this.transform.rotation);
        GameObject package = GameObject.Instantiate(PackagePrefab, new Vector3(transform.position.x, 2, transform.position.z), this.transform.rotation);

        //Throw package towards mouse, and adjust with player velocity
        package.GetComponent<Rigidbody>().velocity = (new Vector3(mousePos.x, 2, mousePos.z) -new Vector3(this.transform.position.x, 2, this.transform.position.z) ).normalized
            * 20 + myRig.velocity * slowTime;

        //Cooldown timer
        StartCoroutine(ResetPackage());
    }

    public Vector3 GetMouseLoc()
    {

        Vector3 target = Vector3.zero;

        //Plane is what the raycast is looking for
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        //Getting point where mouse is on the screen
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hit;
        
        if(plane.Raycast(ray, out hit))
        {
            target = ray.GetPoint(hit);
            return target;
        }

        throw new System.Exception("Could not determine raycast!");
    }

    public IEnumerator ResetPackage()
    {
        yield return new WaitForSeconds(resetPackageTime);
        canSpawnPackage = true;
    }

    public void MoveCam()
    {
        myCam.transform.position = new Vector3(this.transform.position.x, myCam.transform.position.y, this.transform.position.z);
    }
}
