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
    public float brakeTimer;

    public float progress;
    private bool tallyScore;


    public bool finishBar;
    public bool brakeBar;

    void Start()
    {
        base.Start();
        myCam = Camera.main;
        brakeTimer = brakeTime;

    }

    // Update is called once per frame
    void Update()
    {
        if(!death && gm.isStarted)
            this.Move();

        
        if(death && !tallyScore)
        {
            tallyScore = true;
            StartCoroutine(EndGame());
            gm.canAdd = false;
            myRig.velocity = new Vector3(0, myRig.velocity.y, 0);
        }

        MoveCam();

        if (brakeBar)
            ToggleBrakeBar();

        if (finishBar)
            ToggleProgressBar();
    }

    public override void Move()
    {
        float turn = Input.GetAxisRaw("Horizontal");
        float move = Input.GetAxisRaw("Vertical");

        if(turn != 0)
        {
            myRig.angularVelocity = new Vector3(0, turn, 0).normalized * turnSpeed;
        }
        else
        {
            myRig.angularVelocity = Vector3.zero;
        }

        if(Input.GetAxisRaw("Fire2") != 0 && !gm.isPaused)
        {
            slowTime = 0.5f;
            brakeTimer -= Time.deltaTime * 1.5f;

            gm.brakeBar.fillAmount += 1.0f / (brakeTime) * Time.deltaTime*1.5f;

            

            if (brakeTimer <= 0)
            {
                Debug.Log("Brakes Failed!");
                Die();
            }
        }
        else
        {
            if(brakeTimer < brakeTime)
            {
                gm.brakeBar.fillAmount -= 1.0f / (brakeTime) * Time.deltaTime;
                brakeTimer += Time.deltaTime;
            }
            slowTime = 1;
        }

        if(Input.GetAxisRaw("Fire1") != 0 && !gm.isPaused)
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
        GameObject package = GameObject.Instantiate(PackagePrefab, new Vector3(transform.position.x, 1, transform.position.z), this.transform.rotation);
        Physics.IgnoreCollision(package.GetComponent<Collider>(), this.GetComponent<Collider>());

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

    void ToggleProgressBar()
    {
        gm.progressBar.fillAmount -= 1 / resetPackageTime * Time.deltaTime;
    }

    void ToggleBrakeBar()
    {
        gm.brakeBar.fillAmount += 1 / resetPackageTime * Time.deltaTime;
    }

    public IEnumerator ResetPackage()
    {

        finishBar = true;
        gm.progressBar.gameObject.SetActive(true);
        yield return new WaitForSeconds(resetPackageTime);
        canSpawnPackage = true;
        finishBar = false;
        gm.progressBar.gameObject.SetActive(false);
        gm.progressBar.fillAmount = 1;

    }

    public IEnumerator EndGame()
    {
        //Incase of pause and death at the EXACT same time :)
        gm.pausePanel.SetActive(false);
        Time.timeScale = 1;

        yield return new WaitForSeconds(1.4f);
        gm.finalScore.text = "Final Score: " + gm.scoreVar.ToString();
        gm.endPanel.SetActive(true);
    }

    public void MoveCam()
    {
        myCam.transform.position = new Vector3(this.transform.position.x, myCam.transform.position.y, this.transform.position.z);
    }

    


}
